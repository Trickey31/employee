<template>
  <button
    class="btn"
    :class="{
      'btn--primary': type == 'primary',
      'btn--secondary': type == 'secondary',
      'btn--disabled': type == 'disabled',
    }"
    ref="myButton"
    :tabindex="tab"
  >
    <span class="btn__text">{{ text }}</span>
  </button>
</template>
<script>
export default {
  name: "VButton",
  /**
   * type = ['primary', 'secondary', 'disabled']
   */
  props: ["text", "type", "tab"],
  methods: {
    onFocus() {
      this.$nextTick(() => {
        this.$refs.myButton.focus();
      });
    },
  },
};
</script>
<style>
@import url("../css/components/button.css");
</style>
