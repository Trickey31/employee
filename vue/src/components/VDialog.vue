<template>
  <div
    class="overlay"
    @click.self="btnCloseDialog"
    @keydown.esc="btnCloseDialog"
  >
    <div class="dialog">
      <div class="dialog__header">
        <div class="dialog__title">{{ title }}</div>
        <div class="dialog__action">
          <span
            class="dialog__close icon icon-close"
            title="Đóng"
            @click="btnCloseDialog"
          ></span>
        </div>
      </div>
      <div class="dialog__body">
        <div class="dialog__icon">
          <VIcon
            :class="{
              'icon-warning': type === 'warning',
              'icon-question': type === 'confirm',
            }"
          ></VIcon>
        </div>
        <div class="dialog__desc">
          <ul v-if="Array.isArray(desc)">
            <li v-for="(el, index) in desc" :key="index">
              {{ el }}
            </li>
          </ul>
          <span v-else>{{ desc }}</span>
        </div>
      </div>
      <div class="dialog__footer">
        <div class="dialog__button-left">
          <VButton
            type="secondary"
            v-if="cancel !== 'hidden'"
            :text="cancelText"
            @click="btnCloseDialog"
          />
        </div>

        <div class="dialog__button-right">
          <VButton
            type="secondary"
            :text="this.$vnbutton.NO"
            v-if="threeButtons"
            @click="btnNoClick"
            ref="btnNo"
          />
          <VButton
            type="primary"
            :text="okText"
            ref="btnOk"
            v-if="ok !== 'hidden'"
            @click="btnOkOnClick"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import VIcon from "./VIcon.vue";
import VButton from "./VButton.vue";
export default {
  name: "VDialog",
  props: [
    "title",
    "desc",
    "icon",
    "ok",
    "cancel",
    "okText",
    "cancelText",
    "type",
    "threeButtons",
  ],
  components: { VIcon, VButton },
  mounted() {
    if (this.threeButtons === true) {
      this.$refs.btnNo.onFocus();
    } else {
      this.$refs.btnOk.onFocus();
    }
  },
  methods: {
    /**
     * Xử lí sự kiện khi bấm vào nút đóng Dialog
     * Author: VTThanh (10/9/2023)
     */
    btnCloseDialog() {
      this.$emit("closeDialog");
      this.$_vcommon.closeDialog();
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Có
     * Author: VTThanh (10/9/2023)
     */
    btnOkOnClick() {
      if (this.type == "warning") {
        this.btnCloseDialog();
      } else {
        this.$emit("onOkDialog");
      }
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Không
     * Author: VTThanh (10/9/2023)
     */
    btnNoClick() {
      if (this.type == "warning") {
        this.btnCloseDialog();
      } else {
        this.$emit("onNoDialog");
      }
    },
  },
};
</script>
<style scoped>
@import url("../css/components/dialog.css");
ul {
  padding-left: 16px;
}
li + li {
  padding-top: 4px;
}
</style>
