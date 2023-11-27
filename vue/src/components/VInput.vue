<template>
  <div
    class="input"
    :class="{
      'input--required': required,
    }"
    :style="{ width: widthSize }"
  >
    <div class="input__content">
      <input
        :class="{
          'input--error': invalid,
        }"
        :type="type == null ? 'text' : type"
        name="input"
        :placeholder="hintText"
        :maxlength="maxLength"
        v-model="value"
        @input="onInput"
        @blur="checkValid"
        ref="myInput"
        :id="myId"
        :tabindex="tab"
      />
      <VIcon class="input__icon" :class="icon"></VIcon>
    </div>
    <div class="input__error-msg" v-if="invalid">
      {{ errorText }}
    </div>
  </div>
</template>
<script>
import VIcon from "./VIcon.vue";
export default {
  name: "VInput",
  components: { VIcon },
  emits: ["update:modelValue"],
  /**
   * type = ['text', 'number', 'date'] (type của thẻ input trong html)
   * icon = tên class của ms-icon
   * hintText: là giá trị của thuộc tính placeholder của thẻ input trong html
   * widthSize: độ rộng của thẻ input
   * labelText: set text cho label
   * title: là tooltip
   */
  props: [
    "type",
    "icon",
    "hintText",
    "widthSize",
    "labelText",
    "title",
    "modelValue",
    "required",
    "maxLength",
    "myId",
    "tab",
  ],

  created() {},
  beforeUpdate() {
    this.checkValid();
  },
  data() {
    return {
      value: "",
      errorText: "",
      invalid: false,
      emailValid: false,
    };
  },
  methods: {
    checkValid() {
      if (!this.modelValue && this.required) {
        this.errorText = `${this.labelText} không được trống`;
        this.invalid = true;
      } else if (
        this.modelValue &&
        !this.required &&
        this.labelText === "Email" &&
        !this.$_visvalid.isEmail(this.modelValue)
      ) {
        this.errorText = `${this.labelText} sai định dạng`;
        this.invalid = true;
      } else {
        this.errorText = null;
        this.invalid = false;
      }
    },
    /**
     * Cập nhật giá trị modelValue ra bên ngoài
     * Author: VTThanh (10/9/2023)
     */
    onInput() {
      this.$emit("update:modelValue", this.value);
    },
    /**
     * Thực hiện focus vào input
     * Author: VTThanh (10/9/2023)
     */
    onFocus() {
      this.$refs.myInput.focus();
    },
  },
  watch: {
    /**
     * Theo dõi xem nếu modelValue thay đổi giá trị thì gán value = modelValue
     * Author: VTThanh (10/9/2023)
     */
    modelValue: function (newModelValue) {
      this.value = newModelValue;
      if (this.type == "date" && this.value == "") {
        this.value = null;
        this.$emit("update:modelValue", this.value);
      }
    },
  },
};
</script>
<style>
@import url("../css/components/input.css");
</style>
