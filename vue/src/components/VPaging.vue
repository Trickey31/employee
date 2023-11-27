<template>
  <div class="pagination">
    <div class="pagination__left">
      <div class="total-record">
        Tổng số: <b style="font-weight: 700">{{ totalRecord }}</b> bản ghi
      </div>
    </div>
    <div class="pagination__right">
      <div
        class="record-in-page"
        @click.prevent="showDropDown"
        style="user-select: none"
      >
        <span>Số bản ghi/trang: {{ pageSizeSelected }}</span>
        <div class="record-option" v-show="isShowDropdown">
          <div
            class="option-item"
            v-for="(item, index) in pageSizes"
            :key="index"
            @click="setPageSizeSelected($event, index)"
            ref="pageSizeItem"
          >
            {{ item }} bản ghi trên 1 trang
          </div>
        </div>
        <div class="record-in-page__dropdown">
          <VIcon class="icon icon-chevron-down--small"></VIcon>
        </div>
      </div>

      <div>
        <span
          ><b>{{ firstIndex }} - {{ lastIndex }}</b> bản ghi</span
        >
      </div>
      <div
        class="page-prev"
        @click="previousPageOnClick"
        :class="{
          disabled: isPrevButtonsDisabled,
        }"
      >
        <VIcon
          class="icon-chevron-left"
          :class="{
            disabled: isPrevButtonsDisabled,
          }"
        ></VIcon>
      </div>
      <div
        class="page-next"
        @click="nextPageOnclick"
        :class="{
          disabled: isNextButtonDisabled,
        }"
      >
        <VIcon
          class="icon-chevron-right"
          :class="{
            disabled: isNextButtonDisabled,
          }"
        ></VIcon>
      </div>
    </div>
  </div>
</template>
<script>
import VIcon from "./VIcon.vue";
export default {
  name: "VPaging",
  props: ["totalRecord", "firstIndex", "lastIndex"],
  emits: ["previous", "next", "pageSize"],
  components: { VIcon },
  mounted() {
    this.lastElement = this.$refs.pageSizeItem[2];
    this.lastElement.classList.add("selected");
  },
  data() {
    return {
      isShowDropdown: false,
      isPrevButtonsDisabled: true,
      isNextButtonDisabled: false,
      pageSizes: [10, 20, 30, 50, 100],
      pageSizeSelected: 30,
      lastElement: null,
    };
  },
  methods: {
    /**
     * Hiển thị dropdown
     * Author: VTThanh (05/09/2023)
     */
    showDropDown() {
      this.isShowDropdown = !this.isShowDropdown;
    },
    /**
     * Bấm về trang trước
     * Author: VTThanh (05/09/2023)
     */
    previousPageOnClick() {
      this.$emit("previous");
    },
    /**
     * Bấm đến trang sau
     * Author: VTThanh (05/09/2023)
     */
    nextPageOnclick() {
      this.$emit("next");
    },
    /**
     * Đặt disabled cho prev button
     * Author: VTThanh (05/09/2023)
     */
    setDisabledPrevButton(val) {
      this.isPrevButtonsDisabled = val;
    },
    /**
     * Đặt disabled cho next button
     * Author: VTThanh (05/09/2023)
     */
    setDisabledNextButton(val) {
      this.isNextButtonDisabled = val;
    },
    /**
     * Chọn số lượng bản ghi/trang
     * Author: VTThanh (05/09/2023)
     */
    setPageSizeSelected(event, index) {
      if (this.lastElement) {
        this.lastElement.classList.remove("selected");
      }
      event.currentTarget.classList.add("selected");
      this.lastElement = event.currentTarget;
      this.pageSizeSelected = this.pageSizes[index];
      this.$emit("pageSize", this.pageSizeSelected);
    },
  },
};
</script>
<style scoped>
@import url("../css/components/paging.css");
</style>
