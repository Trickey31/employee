<template>
  <div class="combobox">
    <input
      type="text"
      class="input combobox__input"
      :class="{ 'input--error': invalid }"
      v-model="textInput"
      @input="inputOnChange"
      @keydown="selecItemUpDown"
      @change="$emit('update:modelValue', $event.target.value)"
      @blur="checkValid"
      :tabindex="tab"
      :id="myId"
      ref="myInput"
    />
    <button
      type="button"
      class="button combobox__button"
      @click.stop="btnSelectDataOnClick"
      @keydown="selecItemUpDown"
      tabindex="-1"
    >
      <VIcon class="icon-chevron-down"></VIcon>
    </button>
    <div
      v-if="isShowListData"
      class="combobox__data"
      ref="combobox__data"
      v-clickoutside="hideListData"
    >
      <a
        class="combobox__item"
        v-for="(item, index) in dataFilter"
        :class="{
          'combobox__item--focus': index == indexItemFocus,
          'combobox__item--selected': index == indexItemSelected,
        }"
        :key="item[this.propValue]"
        :ref="'toFocus_' + index"
        :value="item[this.propValue]"
        @click="itemOnSelect(item, index)"
        @focus="saveItemFocus(index)"
        @keydown="selecItemUpDown(index)"
        @keyup="selecItemUpDown(index)"
        tabindex="1"
      >
        <span>{{ item[this.propText] }}</span>
        <div class="combobox__item-icon">
          <VIcon class="icon-tick" v-show="index == indexItemSelected" />
        </div>
      </a>
    </div>
    <div class="input__error-msg" v-if="invalid">
      {{ errorText }}
    </div>
  </div>
</template>
<script>
import VIcon from "./VIcon.vue";
/* eslint-disable */
/**
 * Gán sự kiện nhấn click chuột ra ngoài combobox data (ẩn data list đi)
 * NVMANH (31/07/2022)
 */
const clickoutside = {
  mounted(el, binding, vnode, prevVnode) {
    el.clickOutsideEvent = (event) => {
      // Nếu element hiện tại không phải là element đang click vào
      // Hoặc element đang click vào không phải là button trong combobox hiện tại thì ẩn đi.
      if (
        !(
          (
            el === event.target || // click phạm vi của combobox__data
            el.contains(event.target) || //click vào element con của combobox__data
            el.previousElementSibling.contains(event.target)
          ) //click vào element button trước combobox data (nhấn vào button thì hiển thị)
        )
      ) {
        // Thực hiện sự kiện tùy chỉnh:
        binding.value(event, el);
        // vnode.context[binding.expression](event); // vue 2
      }
    };
    document.body.addEventListener("click", el.clickOutsideEvent);
  },
  beforeUnmount: (el) => {
    document.body.removeEventListener("click", el.clickOutsideEvent);
  },
};

function removeVietnameseTones(str) {
  str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
  str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
  str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
  str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
  str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
  str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
  str = str.replace(/đ/g, "d");
  str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
  str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
  str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
  str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
  str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
  str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
  str = str.replace(/Đ/g, "D");
  // Some system encode vietnamese combining accent as individual utf-8 characters
  // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
  str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
  str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
  // Remove extra spaces
  // Bỏ các khoảng trắng liền nhau
  str = str.replace(/ + /g, " ");
  str = str.trim();
  // Remove punctuations
  // Bỏ dấu câu, kí tự đặc biệt
  str = str.replace(
    /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
    " "
  );
  return str;
}

const keyCode = {
  Enter: 13,
  ArrowUp: 38,
  ArrowDown: 40,
  ESC: 27,
};

export default {
  name: "VCombobox",
  beforeUpdate() {
    this.checkValid();
    // this.checkValidFromBackend();
  },
  directives: {
    clickoutside,
  },
  components: { VIcon },
  props: {
    value: String,
    url: String,
    propValue: String,
    propText: String,
    isLoadData: {
      type: Boolean,
      default: true,
    },
    tab: null,
    labelText: String,
    required: Boolean,
    myId: String,
  },
  methods: {
    checkValid() {
      if (this.required && this.textInput === "") {
        this.errorText = `${this.labelText} không được để trống.`;
        this.invalid = true;
      } else {
        this.errorText = null;
        this.invalid = false;
      }
    },

    // checkValidFromBackend() {
    //   if (this.errors?.length > 0 && this.required) {
    //     this.invalid = true;
    //   } else {
    //     this.invalid = false;
    //   }
    // },

    onFocus() {
      this.$refs.myInput.focus();
    },

    /**
     * Lưu lại index của item đã focus
     * NVMANH (31/07/2022)
     */
    saveItemFocus(index) {
      this.indexItemFocus = index;
    },

    /**
     * Ẩn danh sách item
     * NVMANH (31/07/2022)
     */
    hideListData() {
      this.isShowListData = false;
    },

    /**
     * Nhấn vào button thì hiển thị hoặc ẩn List Item
     * NVMANH (31/07/2022)
     */
    btnSelectDataOnClick() {
      this.dataFilter = this.data;
      this.isShowListData = !this.showListData;
    },

    /**
     * Click chọn item trong danh sách
     * NVMANH (31/07/2022)
     */
    itemOnSelect(item, index) {
      const text = item[this.propText];
      const value = item[this.propValue];
      this.textInput = text; // Hiển thị text lên input.
      this.indexItemSelected = index;
      this.isShowListData = false;
      // this.$emit("getValue", value, text, item);
      this.$emit("getValue", value, text);
    },

    /**
     * Nhập text thì thực hiện filter dữ liệu và hiển thị
     * NVMANH (31/07/2022)
     */
    inputOnChange() {
      var me = this;
      // Thực hiện lọc các phần tử phù hợp trong data:
      this.dataFilter = this.data.filter((e) => {
        let text = removeVietnameseTones(me.textInput)
          .toLowerCase()
          .replace(" ", "");
        let textOfItem = removeVietnameseTones(e[me.propText])
          .toLowerCase()
          .replace(" ", "");
        return textOfItem.includes(text);
      });
      this.isShowListData = true;
    },

    /**
     * Lựa chọn item bằng cách nhấn các phím lên, xuống trên bàn phím
     * NVMANH (31/07/2022)
     */
    selecItemUpDown(event) {
      var me = this;
      var keyCodePress = event.keyCode;
      var elToFocus = null;
      switch (keyCodePress) {
        case keyCode.ESC:
          this.isShowListData = false;
          break;
        case keyCode.ArrowDown:
          this.isShowListData = true;
          elToFocus = this.$refs[`toFocus_${me.indexItemFocus + 1}`];
          if (
            this.indexItemFocus == null ||
            !elToFocus ||
            elToFocus.length == 0
          ) {
            this.indexItemFocus = 0;
          } else {
            this.indexItemFocus += 1;
          }
          break;
        case keyCode.ArrowUp:
          this.isShowListData = true;
          elToFocus = this.$refs[`toFocus_${me.indexItemFocus - 1}`];
          if (
            this.indexItemFocus == null ||
            !elToFocus ||
            elToFocus.length == 0
          ) {
            this.indexItemFocus = this.dataFilter.length - 1;
          } else {
            this.indexItemFocus -= 1;
          }
          break;
        case keyCode.Enter:
          event.preventDefault();
          elToFocus = this.$refs[`toFocus_${me.indexItemFocus}`];
          if (elToFocus && elToFocus.length > 0) {
            elToFocus[0].click();
            this.isShowListData = false;
          }
          break;
        default:
          break;
      }
    },
  },

  created() {
    // Thực hiện lấy dữ liệu từ api:
    if (this.url) {
      fetch(this.url)
        .then((res) => res.json())
        .then((res) => {
          this.data = res;
          this.dataFilter = res;
        })
        .catch((res) => {
          console.log(res);
        });
    }
    this.textInput = this.value || "";
  },
  data() {
    return {
      data: [], // data gốc
      textInput: "",
      dataFilter: [], // data đã được filter
      isShowListData: false, // Hiển thị list data hay không
      indexItemFocus: null, // Index của item focus hiện tại
      indexItemSelected: null, // Index của item được selected
      errorText: "",
      invalid: false,
    };
  },
  watch: {
    value() {
      this.textInput = this.value;
    },
  },
};
</script>
<style scoped>
@import url("../css/components/combobox.css");
</style>
