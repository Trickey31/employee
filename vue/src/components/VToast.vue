<template>
  <!-- Toast message -->
  <div class="toast-block">
    <div class="toast toast--success">
      <div class="toast__icon">
        <VIcon class="icon-success"></VIcon>
        <span class="text">Thành công!</span>
      </div>
      <div class="toast__msg">{{ msg }}</div>
      <div class="toast__close">
        <span class="text">Hoàn tác</span>
        <i
          class="icon icon-close"
          style="transform: scale(0.5)"
          aria-hidden="true"
          @click="btnCloseToast"
        ></i>
      </div>
    </div>
  </div>
</template>
<script>
import VIcon from "@/components/VIcon.vue";
export default {
  name: "VToast",
  props: ["msg"],
  components: { VIcon },
  methods: {
    /**
     * Xử lí sự kiện khi bấm vào nút đóng Toast
     * Author: VTThanh (10/9/2023)
     */
    btnCloseToast() {
      this.$_common.closeToast();
    },
  },
};
</script>
<style scoped>
@import url("../css/components/toast.css");
</style>
