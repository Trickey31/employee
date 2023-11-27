<template>
  <table class="table">
    <thead>
      <slot name="table__thead"></slot>
    </thead>
    <tbody>
      <slot name="table__tbody"></slot>
    </tbody>
  </table>
</template>
<script>
export default {
  name: "VTable",
  components: {},
  methods: {},
  data() {
    return {};
  },
};
</script>
<style>
@import url("../css/components/table.css");
</style>
