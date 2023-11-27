<template>
  <label :class="[className, { 'label--required': required }]" :for="forId">
    <slot></slot>
  </label>
</template>
<script>
export default {
  name: "VLabel",
  props: ["className", "forId", "required"],
  methods: {},
};
</script>
<style>
.label--required::after {
  content: " *";
  padding-left: 1px;
  color: var(--color-error);
}
</style>
