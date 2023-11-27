<template>
  <div class="content">
    <router-view></router-view>
  </div>

  <Teleport to="body">
    <VToast :msg="toastMessage" v-if="isShowToast"></VToast>
    <VDialog
      :title="titleDialog"
      :desc="descDialog"
      ok-text="Xác nhận"
      cancel="hidden"
      type="warning"
      v-if="isShowDialog"
    ></VDialog>

    <VLoading v-if="isShowLoading"></VLoading>
  </Teleport>
</template>
<script>
import VDialog from "@/components/VDialog.vue";
import VLoading from "@/components/VLoading.vue";
import VToast from "@/components/VToast.vue";
export default {
  name: "TheContent",
  components: { VDialog, VLoading, VToast },
  created() {
    this.$_emitter.on("onShowToast", this.onShowToast);
    this.$_emitter.on("onCloseToast", this.onCloseToast);

    this.$_emitter.on("onShowDialog", this.onShowDialog);
    this.$_emitter.on("onCloseDialog", this.onCloseDialog);

    this.$_emitter.on("onShowLoading", this.onShowLoading);
    this.$_emitter.on("onCloseLoading", this.onCloseLoading);
  },

  unmounted() {
    this.$_emitter.off("onShowToast");
    this.$_emitter.off("onCloseToast");

    this.$_emitter.off("onShowDialog");
    this.$_emitter.off("onCloseDialog");

    this.$_emitter.off("onShowLoading");
    this.$_emitter.off("onCloseLoading");
  },
  data() {
    return {
      isShowToast: false,
      isShowDialog: false,
      isShowLoading: false,
      toastMessage: "",
      titleDialog: "",
      descDialog: "",
      employee: null,
    };
  },
  methods: {
    /**
     * Xử lí sự kiện showToast
     * Author: VTThanh (10/9/2023)
     */
    onShowToast(msg) {
      this.isShowToast = true;
      this.toastMessage = msg;
      setTimeout(() => {
        this.isShowToast = false;
      }, 3000);
    },
    /**
     * Xử lí sự kiện closeToast
     * Author: VTThanh (10/9/2023)
     */
    onCloseToast() {
      this.isShowToast = false;
    },

    /**
     * Xử lí sự kiện showDialog
     * Author: VTThanh (10/9/2023)
     */
    onShowDialog(title, desc) {
      this.isShowDialog = true;
      this.titleDialog = title;
      this.descDialog = desc;
    },

    /**
     * Xử lí sự kiện closeDialog
     * Author: VTThanh (10/9/2023)
     */
    onCloseDialog() {
      this.isShowDialog = false;
    },

    /**
     * Xử lí sự kiện showLoading
     * Author: VTThanh (10/9/2023)
     */
    onShowLoading() {
      this.isShowLoading = true;
    },
    /**
     * Xử lí sự kiện closeLoading
     * Author: VTThanh (10/9/2023)
     */
    onCloseLoading() {
      setTimeout(() => {
        this.isShowLoading = false;
      }, 200);
    },
  },
};
</script>
<style>
@import url("../css/layout/content.css");

.content {
  height: calc(100vh - 56px);
  background-color: rgb(225, 218, 218);
  box-sizing: border-box;
  padding: 0 24px 0 24px;
}
</style>
