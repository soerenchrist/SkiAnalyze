<template>
  <div>
    <div class="text-h4">{{skiArea.name}}</div>
    <div class="text-subtitle-1" v-if="skiArea.alternativeName">
      {{skiArea.alternativeName}}
    </div>
  </div>
</template>

<script>
export default {
  props: {
    skiArea: {
      type: Object,
      required: true,
    },
  },
};
</script>
