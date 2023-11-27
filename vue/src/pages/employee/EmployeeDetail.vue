<template>
  <VForm>
    <template v-slot:popup-header>
      <div class="popup__title">
        {{ this.formTitle }}
      </div>
      <div class="checkbox">
        <div class="checkbox__item">
          <input type="checkbox" name="" id="" /> Là khách hàng
        </div>
        <div class="checkbox__item">
          <input type="checkbox" name="" id="" /> Là nhà cung cấp
        </div>
      </div>
    </template>
    <template v-slot:popup-body>
      <div class="popup__group-1/2 flex-16">
        <div class="form__group">
          <div class="flex-16">
            <div class="form__item" style="width: 35%">
              <VLabel for="employeeCode" :required="true">Mã</VLabel>
              <VInput
                max-length="20"
                :required="true"
                label-text="Mã"
                v-model="employee.EmployeeCode"
                ref="employeeCode"
                my-id="employeeCode"
                :tab="1"
              />
            </div>
            <div class="form__item flex-1">
              <VLabel for="fullName" :required="true">Tên</VLabel>
              <VInput
                :required="true"
                label-text="Tên"
                :max-length="100"
                v-model="employee.FullName"
                ref="fullName"
                my-id="fullName"
                :tab="2"
              />
            </div>
          </div>
          <div class="form__item" style="margin-top: 30px">
            <VLabel for="department" :required="true">Đơn vị</VLabel>
            <VCombobox
              url="https://localhost:8081/api/v1/Departments"
              label-text="Đơn vị"
              propText="DepartmentName"
              propValue="DepartmentId"
              v-model="employee.DepartmentId"
              :required="true"
              :value="employee.DepartmentName"
              @getValue="handleGetValue"
              ref="department"
              my-id="department"
              :tab="3"
            ></VCombobox>
          </div>
          <div class="form__item" style="margin-top: 30px">
            <VLabel for="position">Chức danh</VLabel>
            <VInput v-model="employee.PositionName" my-id="position" :tab="4" />
          </div>
        </div>

        <div class="form__group flex-1">
          <div class="flex-16">
            <div class="form__item">
              <VLabel for="dob">Ngày sinh</VLabel>
              <!-- <VInput
                type="date"
                label-text="Ngày sinh"
                ref="dateOfBirth"
                v-model="employee.DateOfBirth"
                my-id="dob"
                :tab="5"
              /> -->
              <VueDatePicker
                v-model="employee.DateOfBirth"
                locale="vi-vn"
                :auto-apply="true"
                :format="dateFormat"
                :placeholder="dateFormat"
                :enable-time-picker="false"
                :tabindex="5"
                :day-names="customDays"
              ></VueDatePicker>
            </div>
            <div class="input">
              <div class="input__label">Giới tính</div>
              <div class="radio-group">
                <div class="radio-item">
                  <input
                    type="radio"
                    name="gender"
                    :value="this.$_venum.Gender.Male"
                    checked
                    @click="setGenderValue($event)"
                    ref="genderMale"
                    :tabindex="6"
                  />
                  Nam
                </div>
                <div class="radio-item">
                  <input
                    type="radio"
                    name="gender"
                    :value="this.$_venum.Gender.Female"
                    @click="setGenderValue($event)"
                    ref="genderFemale"
                    :tabindex="7"
                  />
                  Nữ
                </div>
                <div class="radio-item">
                  <input
                    type="radio"
                    name="gender"
                    :value="this.$_venum.Gender.Other"
                    @click="setGenderValue($event)"
                    ref="genderOther"
                    :tabindex="8"
                  />
                  Khác
                </div>
              </div>
            </div>
          </div>
          <div class="flex-16" style="margin-top: 30px">
            <div class="form__item" style="width: 100%">
              <VLabel for="identityNumber">Số CMND</VLabel>
              <VInput
                max-length="12"
                title="Số chứng minh nhân dân"
                v-model="employee.IdentityNumber"
                ref="identityNumber"
                my-id="identityNumber"
                :tab="9"
              />
            </div>
            <div class="form__item">
              <VLabel for="identityDate">Ngày cấp</VLabel>
              <!-- <VInput
                type="date"
                ref="identityDate"
                my-id="identityDate"
                v-model="employee.IdentityDate"
                :tab="10"
              /> -->
              <VueDatePicker
                v-model="employee.IdentityDate"
                locale="vi-vn"
                :auto-apply="true"
                :format="dateFormat"
                :placeholder="dateFormat"
                :tabindex="10"
                :day-names="customDays"
              ></VueDatePicker>
            </div>
          </div>
          <div class="form__item" style="margin-top: 30px">
            <VLabel for="identityPlace">Nơi cấp</VLabel>
            <VInput
              width-size="100%"
              v-model="employee.IdentityAddress"
              ref="identityPlace"
              my-id="identityPlace"
              :tab="11"
            />
          </div>
        </div>
      </div>

      <div class="popup__group-full" style="margin-top: 30px">
        <div class="form__group">
          <div class="form__item">
            <VLabel for="address">Địa chỉ</VLabel>
            <VInput
              width-size="100%"
              v-model="employee.Address"
              my-id="address"
              :tab="12"
            />
          </div>
        </div>
        <div class="form__group flex-16" style="margin-top: 30px">
          <div class="form__item">
            <VLabel for="phoneNumber">ĐT di động</VLabel>
            <VInput
              title="Điện thoại di động"
              v-model="employee.PhoneNumber"
              my-id="phoneNumber"
              :tab="13"
            />
          </div>
          <div class="form__item">
            <VLabel for="landlinePhone">ĐT cố định</VLabel>
            <VInput
              title="Điện thoại cố định"
              v-model="employee.LandlinePhone"
              my-id="landlinePhone"
              :tab="14"
            />
          </div>
          <div class="form__item">
            <VLabel for="email">Email</VLabel>
            <VInput
              :max-length="50"
              v-model="employee.Email"
              my-id="email"
              :tab="15"
              label-text="Email"
              ref="email"
            />
          </div>
        </div>
        <div class="form__group flex-16" style="margin-top: 30px">
          <div class="form__item">
            <VLabel for="bankAccount">Tài khoản ngân hàng</VLabel>
            <VInput
              label-text="Tài khoản ngân hàng"
              v-model="employee.BankAccount"
              my-id="bankAccount"
              :tab="16"
            />
          </div>
          <div class="form__item">
            <VLabel for="bankName">Tên ngân hàng</VLabel>
            <VInput
              label-text="Tên ngân hàng"
              v-model="employee.BankName"
              my-id="bankName"
              :tab="17"
            />
          </div>
          <div class="form__item">
            <VLabel for="bankBranch">Chi nhánh</VLabel>
            <VInput
              label-text="Chi nhánh"
              v-model="employee.BankBranch"
              my-id="bankBranch"
              :tab="18"
            />
          </div>
        </div>
      </div>
    </template>
    <template v-slot:popup-footer>
      <div class="popup__button-left">
        <VButton
          type="secondary"
          :text="this.$vnbutton.CANCEL"
          @click="btnCancelOnClick"
          :tabindex="19"
        />
      </div>

      <div class="popup__button-right">
        <VButton
          type="secondary"
          :text="this.$vnbutton.SAVE"
          :tabindex="20"
          @click="
            () => {
              if (this.formMode == this.$_venum.FormMode.Add) {
                btnAddOnClick();
              } else if (this.formMode == this.$_venum.FormMode.Edit) {
                btnUpdateOnClick();
              }
            }
          "
        />
        <VButton
          type="primary"
          :text="this.$vnbutton.SAVE_ADD"
          :tabindex="21"
          @keydown.tab.prevent="focusFirstInput"
          @click="
            () => {
              this.isClickSaveAndAddBtn = true;
              if (this.formMode == this.$_venum.FormMode.Add) {
                btnAddOnClick();
              } else if (this.formMode == this.$_venum.FormMode.Edit) {
                btnUpdateOnClick();
              }
            }
          "
        />
      </div>
    </template>
  </VForm>

  <Teleport to="body">
    <VDialog
      :title="this.$vndialogTitles.CONFIRM_TITLE"
      :desc="this.$vndialogMessages.CHANGE_CONFIRM_MESSAGE"
      :ok-text="this.$vnbutton.YES"
      :cancel-text="this.$vnbutton.CANCEL"
      :no-text="this.$vnbutton.NO"
      :three-buttons="hasThreeButtons"
      v-if="isDataChanged"
      ref="dlgChange"
      type="confirm"
      @closeDialog="btnCancelClick"
      @onOkDialog="btnYesOnClick"
      @onNoDialog="btnNoOnClick"
    ></VDialog>
  </Teleport>
</template>
<script>
import {
  getNewEmployeeCode,
  updateEmployee,
  addEmployee,
} from "@/utils/employee";
import VForm from "@/components/VForm.vue";
import VButton from "@/components/VButton.vue";
import VInput from "@/components/VInput.vue";
import VCombobox from "@/components/VCombobox.vue";
import VLabel from "@/components/VLabel.vue";
import VDialog from "@/components/VDialog.vue";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

export default {
  name: "EmployeeDetail",
  /**
   * employeeReceived: Dùng để nhận dữ liệu employee từ EmployeeList
   */
  props: ["employeeReceived", "isCloneEvent", "dateFormat", "refresh"],
  components: {
    VForm,
    VButton,
    VInput,
    VCombobox,
    VLabel,
    VDialog,
    VueDatePicker,
  },
  emits: ["openFormAdd"],
  async created() {
    // Lấy mã nhân viên mới
    if (!this.employeeReceived) {
      this.employee.EmployeeCode = await getNewEmployeeCode();
    }
  },
  async mounted() {
    this.focusFirstInput();

    // Đẩy các input vào một mảng
    this.inputs = [
      this.$refs.employeeCode,
      this.$refs.fullName,
      this.$refs.department,
      this.$refs.email,
    ];

    // Chuyển formMode và gán các giá trị cần thiết
    if (this.employeeReceived) {
      console.log(this.employeeReceived);
      this.employee = this.employeeReceived;
      console.log("employee: ", this.employee);
      this.employeeBackup = { ...this.employeeReceived };
      // Nếu sự kiện là click vào nút Nhân bản thì gán formMode là Add và lấy mã nhân viên mới
      if (this.isCloneEvent) {
        this.formMode = this.$_venum["FormMode"].Add;
        this.employee.EmployeeCode = await getNewEmployeeCode();
      }
      // Nếu sự kiẹn là click vào nút Sửa thì gán formMode là Edit
      else {
        this.formMode = this.$_venum["FormMode"].Edit;
      }

      // checked các radio button
      switch (this.employee.Gender) {
        case this.$_venum.Gender.Male:
          this.$refs.genderMale.checked = true;
          break;
        case this.$_venum.Gender.Female:
          this.$refs.genderFemale.checked = true;
          break;
        default:
          this.$refs.genderOther.checked = true;
          break;
      }

      // gán giá trị cho các thẻ input date
      // this.employee.DateOfBirth = this.$_vformat.formatDate(
      //   this.employee.DateOfBirth,
      //   this.dateFormat
      // );
      // this.employee.IdentityDate = this.$_vformat.formatDate(
      //   this.employee.IdentityDate,
      //   this.dateFormat
      // );
    }
    // Hiển thị tiêu đề form
    this.formTitle = this.getFormTitle();
  },
  data() {
    return {
      employee: {
        EmployeeCode: "",
        FullName: "",
        DateOfBirth: null,
        Gender: this.$_venum["Gender"].Male,
        PhoneNumber: "",
        Email: "",
        Address: "",
        IdentityNumber: "",
        IdentityDate: null,
        IdentityAddress: "",
        LandlinePhone: "",
        BankAccount: "",
        BankName: "",
        BankBranch: "",
        DepartmentId: null,
        DepartmentName: "",
        PositionName: "",
      },
      employeeBackup: {},
      hasThreeButtons: true,
      isDataChanged: false,
      isEmployeeChanged: false,
      inputs: null,
      formMode: this.$_venum["FormMode"].Add,
      formTitle: "",
      isClickSaveAndAddBtn: false,
      customDays: ["Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy", "CN"],
      emailValid: false,
    };
  },
  methods: {
    /**
     * Focus vào input đầu tiên
     */
    focusFirstInput() {
      this.$refs.employeeCode.onFocus();
    },
    /**
     * Lấy dữ liệu đơn vị
     * Author: VTThanh (02/09/2023)
     */
    handleGetValue(value, text) {
      this.employee.DepartmentId = value;
      this.employee.DepartmentName = text;
    },

    /**
     * Xử lí tiêu đề form
     * Author: VTThanh (02/09/2023)
     */
    getFormTitle() {
      if (this.formMode === this.$_venum.FormMode.Add) {
        return this.$vnform.EMPLOYEE_ADD_TITLE;
      } else {
        return this.$vnform.EMPLOYEE_DETAIL_TITLE;
      }
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Thêm
     * Author: VTThanh (10/9/2023)
     */
    async btnAddOnClick() {
      console.log(this.employee);
      let valid = await this.validate();
      if (!valid) {
        return;
      }
      try {
        await addEmployee(this.employee);

        if (this.isClickSaveAndAddBtn) {
          this.openFormAdd();
        } else {
          this.$_vcommon.closePopup();
        }
        this.$_vcommon.showToast(
          this.$_vresource.VN.dialog.messages.ADD_SUCCESS
        );
        this.refresh();
      } catch (error) {
        return error;
      }
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Cất
     * Author: VTThanh (10/9/2023)
     */
    async btnUpdateOnClick() {
      let valid = await this.validate();
      if (!valid) {
        return;
      }

      try {
        await updateEmployee(this.employee.EmployeeId, this.employee);

        if (this.isClickSaveAndAddBtn) {
          this.openFormAdd();
        } else {
          this.$_vcommon.closePopup();
        }

        this.$_vcommon.showToast(
          this.$_vresource.VN.dialog.messages.UPDATE_SUCCESS
        );
        this.refresh();
      } catch (error) {
        return error;
      }
    },
    /**
     * Reset form và mở form thêm mới
     * Author: VTThanh (02/09/2023)
     */
    openFormAdd() {
      this.$emit("openFormAdd");
    },
    /**
     * Kiểm tra dữ liệu có hợp lệ hay không
     * Author: VTThanh (10/9/2023)
     */
    async validate() {
      // lỗi input
      let errors = [];
      let isFirstInput = true;
      for (const input of this.inputs) {
        console.log(input);
        if (input.value == null || input.value == "") {
          input.checkValid();
          errors.push(input.errorText);
          if (isFirstInput) {
            input.onFocus();
            isFirstInput = false;
          }
        }
      }
      if (errors.length) {
        // this.$_vcommon.showDialog(this.$vndialogTitles.CONFIRM_TITLE, errors);
        return false;
      }
      return true;
    },
    /**
     * Đặt giá trị cho employee.gender
     * Author: VTThanh (05/09/2023)
     */
    setGenderValue(e) {
      this.employee.Gender = parseInt(e.target.value);
    },

    btnCancelClick() {
      this.isDataChanged = false;
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Huỷ
     * Author: VTThanh (10/9/2023)
     */
    btnCancelOnClick() {
      if (
        this.formMode === this.$_venum["FormMode"].Add &&
        this.employee !== null
      ) {
        console.log("Add");
        this.isDataChanged = true;
      } else if (
        this.formMode === this.$_venum["FormMode"].Edit &&
        JSON.stringify(this.employeeBackup) !== JSON.stringify(this.employee)
      ) {
        console.log("Edit");
        this.isDataChanged = true;
      } else {
        console.log("close");
        this.$_vcommon.closePopup();
      }
    },

    // Xử lý khi click vào Có
    async btnYesOnClick() {
      if (this.formMode === this.$_venum.FormMode.Add) {
        await this.btnAddOnClick();
        this.btnCancelClick();
      } else if (this.formMode === this.$_venum.FormMode.Edit) {
        await this.btnUpdateOnClick();
        this.btnCancelClick();
      }
    },

    // Xử lý khi click vào Không
    btnNoOnClick() {
      this.$_vcommon.closePopup();
    },
  },
};
</script>
<style>
@import url("../../css/components/popup.css");
</style>
