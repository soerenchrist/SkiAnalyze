<template>
<div>
  <div class="text-h4">{{track.name}}</div>
  <div class="text-subtitle-1">
    {{formatDate(track.date)}} @ {{formatTime(track.start)}} in {{track.skiArea.name}}
  </div>
</div>
</template>

<script>
export default {
  props: {
    track: Object,
  },
  methods: {
    formatDate(date) {
      return new Date(date).toLocaleDateString('en-US');
    },
    formatTime(date) {
      return new Date(date).toTimeString().split(' ')[0];
    },
  },
};
</script>
