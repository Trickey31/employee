<template>
  <div class="date" @click.prevent="showDropDown" style="user-select: none">
    <div>{{ optionSelected }}</div>
    <div class="date-option" v-show="isShowDropdown">
      <div
        class="option-item"
        v-for="(item, index) in options"
        :key="index"
        @click="setDateSelected($event, index)"
        ref="dateItem"
      >
        {{ item }}
      </div>
    </div>
    <div class="date-icon">
      <VIcon class="icon icon-chevron-down--small"></VIcon>
    </div>
  </div>
</template>
<script>
export default {};
</script>
<style>
.date {
  padding-top: 30px;
  background-color: #fff;
}
</style>
