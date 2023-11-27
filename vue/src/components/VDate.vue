<template>
  <div>
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
    <div
      style="cursor: pointer; padding: 10px 20px; border: 1px solid #e6e6e6"
      @click="updateData"
    >
      Xác nhận
    </div>
  </div>
</template>
<script>
import VIcon from "./VIcon.vue";
import { getFormat, updateFormat } from "@/utils/setting";
import emitter from "tiny-emitter/instance";
export default {
  name: "VDate",
  components: { VIcon },
  created() {
    if (!localStorage.getItem("dateFormat")) {
      this.loadData();
    } else {
      this.restoreFormatDate();
    }
    console.log("created");
    window.addEventListener("click", this.handleOutsideClick);
  },
  beforeUnmount() {
    // this.updateData();
    window.removeEventListener("click", this.handleOutsideClick);
  },
  data() {
    return {
      options: ["dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd"],
      optionSelected: "",
      initialSelected: "",
      isShowDropdown: false,
      lastElement: null,
    };
  },

  methods: {
    handleOutsideClick(event) {
      const dateElement = this.$el; // Phần tử gốc của component
      if (!dateElement.contains(event.target)) {
        this.isShowDropdown = false; // Đóng dropdown nếu click bên ngoài
      }
    },
    /**
     * Gán dữ liệu
     * Author: VTThanh (09/10/2023)
     */
    bindingData(response) {
      if (response) {
        this.initialSelected = response;
        this.optionSelected = this.initialSelected;
        this.$nextTick(() => {
          const dropdownItems = this.$refs.dateItem;
          const selectedElement = dropdownItems.find(
            (item) => item.innerText === this.initialSelected
          );
          if (selectedElement) {
            selectedElement.classList.add("selected");
            this.lastElement = selectedElement;
          }
        });
        emitter.emit("dateFormat", this.initialSelected);
      } else {
        this.lastElement = this.$refs.dateItem[0];
        this.lastElement.classList.add("selected");
      }
    },

    /**
     * Load dữ liệu từ csdl
     * Author: VTThanh (09/10/2023)
     */
    async loadData() {
      try {
        const response = await getFormat();
        this.bindingData(response);
      } catch (error) {
        return error;
      }
    },

    /**
     * Cập nhật format vào csdl
     * Author: VTThanh (09/10/2023)
     */
    async updateData() {
      this.optionSelected = localStorage.getItem("dateFormat");
      console.log("option: ", this.optionSelected);
      console.log("initial: ", this.initialSelected);
      if (this.optionSelected && this.optionSelected !== this.initialSelected) {
        await updateFormat(this.optionSelected);
        console.log("success");
      }
    },
    /**
     * Hiển thị dropdown
     * Author: VTThanh (05/09/2023)
     */
    showDropDown() {
      this.isShowDropdown = !this.isShowDropdown;
    },
    /**
     * Chọn date format
     * Author: VTThanh (5/10/2023)
     */
    async setDateSelected(event, index) {
      console.log(this.initialSelected);
      const dropdownItems = this.$refs.dateItem;
      const selectedElement = event.currentTarget;

      // Loại bỏ "selected" khỏi mục đã chọn trước đó
      const previousSelectedElement = dropdownItems.find((item) =>
        item.classList.contains("selected")
      );
      if (previousSelectedElement) {
        previousSelectedElement.classList.remove("selected");
      }

      selectedElement.classList.add("selected");
      this.optionSelected = this.options[index];
      console.log("options: ", this.options[index]);
      console.log("optionSelected: ", this.optionSelected);
      localStorage.setItem("dateFormat", this.optionSelected);
      emitter.emit("dateFormat", this.optionSelected);
    },

    restoreFormatDate() {
      const response = localStorage.getItem("dateFormat");
      this.bindingData(response);
    },
  },
};
</script>
<style scoped>
@import url("../css/components/date.css");
.date-option {
  background-color: var(--color-white);
  border: 1px solid #e6e6e6;
  border-radius: 4px;
  position: absolute;
  top: 40px;
  right: 0;
  left: 0;
  z-index: 99999999999999999999999999999;
}
.date {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0 10px;
}
.selected {
  color: var(--color-primary);
  background-color: rgba(80, 184, 60, 0.1);
}
.selected::after {
  position: absolute;
  content: "";
  display: inline-block;
  background: url("@/assets/img/Sprites.64af8f61.svg") no-repeat -1225px -363px;
  width: 14px;
  height: 11px;
  right: 4px;
  top: 11px;
}
</style>
