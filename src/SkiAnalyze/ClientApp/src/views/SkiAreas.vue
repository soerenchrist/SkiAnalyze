<template>
<div>
  <v-row>
    <v-col>
      <v-card>
        <v-card-title>
          Search
        </v-card-title>
        <v-card-text>
          <v-text-field
            prepend-inner-icon="mdi-magnify"
            label="Search..."
            clearable
            hide-details
            v-model="searchText"
            outlined />
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col>
      <ski-area-map
        ref="map"
        :areas="areas"
        :details="details"
        @boundsChanged="onBoundsChanged"
        @showDetails="onShowDetails" />
    </v-col>
    <v-col>
      <v-card>
        <v-card-title>
          List
        </v-card-title>
        <v-card-text>
          <v-virtual-scroll
            :items="areas"
            :item-height="60"
            height="600">
            <template v-slot:default="{ item }">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>
                    {{item.name}}
                  </v-list-item-title>
                </v-list-item-content>
                <v-list-item-action>
                  <v-btn icon @click="onShowDetails(item)">
                    <v-icon>mdi-information</v-icon>
                  </v-btn>
                </v-list-item-action>
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</div>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import SkiAreaMap from '../components/map/SkiAreaMap.vue';
import DataService from '../services/DataService';

export default {
  components: { SkiAreaMap },
  data() {
    return {
      searchText: '',
      bounds: null,
      zoom: null,
      areas: [],
      totalCount: 0,
      details: [],
      page: 1,
      pageSize: 30,
    };
  },
  watch: {
    bounds() {
      if (!this.searchText) {
        this.fetchSkiAreas(this.bounds);
      }
    },
    searchText() {
      this.fetchSkiAreas(this.bounds);
    },
  },
  methods: {
    onBoundsChanged(bounds, zoom) {
      this.zoom = zoom;
      this.bounds = bounds;
    },
    async fetchDetails() {
      const promises = [];
      this.areas.forEach((area) => {
        promises.push(DataService.getSkiArea(area.id));
      });

      this.details = await Promise.all(promises);
    },
    async fetchSkiAreas(mapBounds) {
      let bounds;
      if (!this.searchText) {
        bounds = {
          northEast: {
            latitude: mapBounds._northEast.lat,
            longitude: mapBounds._northEast.lng,
          },
          southWest: {
            latitude: mapBounds._southWest.lat,
            longitude: mapBounds._southWest.lng,
          },
        };
      }
      const response = await DataService.getSkiAreas(
        this.page,
        this.pageSize,
        bounds,
        this.searchText,
      );

      this.totalCount = response.count;
      this.areas = response.data;
      if (this.searchText) {
        this.fitResults(this.areas);
      }
      if (this.zoom >= 12 && this.areas) {
        await this.fetchDetails();
      } else {
        this.details = [];
      }
    },
    fitResults(areas) {
      if (areas.length === 0) return;
      const latLngs = areas.map((x) => latLng(x.centerLatitude, x.centerLongitude));
      const bounds = latLngBounds(latLngs);
      let padding = [100, 100];
      console.log(areas.length);
      if (areas.length === 1) {
        padding = [300, 300];
        this.fetchDetails();
      }
      this.$refs.map.fitBounds(bounds, { padding });
    },
    onShowDetails(area) {
      this.$router.push({ name: 'SkiArea', params: { skiAreaId: area.id } });
    },
  },
};
</script>
