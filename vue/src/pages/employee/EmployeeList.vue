<template>
  <div class="container-header">
    <div class="container-header__title">Nhân viên</div>
    <div class="container-header__button">
      <VButton
        type="primary"
        text="Thêm mới nhân viên"
        @click="btnAddOnClick"
      />
    </div>
  </div>
  <div class="container-toolbar">
    <div class="toolbar--left">
      <div
        class="toolbar__action"
        @click.prevent="toggleToolbarAction"
        v-if="getSelectedRecordCount() > 1"
        style="user-select: none"
      >
        <span class="text"
          >Đã chọn <strong>{{ getSelectedRecordCount() }}</strong></span
        >
        <span class="text" @click="uncheckedAll">Bỏ chọn</span>
        <!-- <span class="text" v-if="getSelectedRecordCount() == this.pageSize"
          >Chọn tất cả các trang</span
        > -->
        <div
          class="toolbar__action-item"
          @click="btnToggleDialogDeleteManyOnClick"
        >
          <VIcon class="icon-trash"></VIcon>
          <span class="item-text">Xoá</span>
        </div>
      </div>
    </div>
    <div class="toolbar--right">
      <!-- <div class="toolbar__date">
        <VDate></VDate>
      </div> -->
      <div class="toolbar__search">
        <VInput
          class="input-action"
          :hint-text="this.$_vresource['VN'].SearchInputHintText"
          icon="icon-search"
          width-size="300px"
          v-model="searchText"
          @input="searchEmployee"
        />
      </div>
      <div class="toolbar__refresh" @click="refreshData" title="Làm mới">
        <VIcon class="icon-refresh"></VIcon>
      </div>
      <div
        class="toolbar__excel"
        @click="exportToExcel"
        title="Xuất dữ liệu toàn bộ nhân viên ra Excel"
      >
        <VIcon class="icon-excel"></VIcon>
      </div>
    </div>
  </div>
  <div class="container-grid" ref="containerGrid">
    <VTable>
      <template v-slot:table__thead>
        <tr>
          <th>
            <input type="checkbox" @click="checkedAll" ref="checkAll" />
          </th>
          <th @click="sortTable('EmployeeCode')">Mã nhân viên</th>
          <th @click="sortTable('FullName')">Tên nhân viên</th>
          <th>Giới tính</th>
          <th>Ngày sinh</th>
          <th title="Số chứng minh nhân dân">Số CMND</th>
          <th>Chức danh</th>
          <th>Tên đơn vị</th>
          <th>Số tài khoản</th>
          <th>Tên ngân hàng</th>
          <th title="Chi nhánh tài khoản ngân hàng">Chi nhánh TK ngân hàng</th>
        </tr>
      </template>
      <template v-slot:table__tbody>
        <tr
          v-for="(employee, index) in employees"
          :key="employee.EmployeeId"
          @mouseover="showActions(index)"
          @mouseleave="hideActions"
          :class="{ 'tr--checked': employee.isChecked }"
        >
          <td
            class="text-center"
            :class="{ 'tr--checked': employee.isChecked }"
          >
            <input
              type="checkbox"
              @click="checkboxHandle($event, index)"
              ref="checkbox"
              :checked="isSelected(employee)"
            />
          </td>
          <td class="text-left" style="min-width: 100px">
            {{ employee.EmployeeCode }}
          </td>
          <td class="text-left" style="min-width: 180px">
            {{ employee.FullName }}
          </td>
          <td class="text-center">
            {{ this.$_vformat.formatGender(employee.Gender) }}
          </td>
          <td class="text-center" style="min-width: 107px">
            {{
              this.$_vformat.formatDate(employee.DateOfBirth, this.formatDate)
            }}
          </td>
          <td class="text-left">{{ employee.IdentityNumber }}</td>
          <td class="text-left" style="min-width: 200px">
            {{ employee.PositionName }}
          </td>
          <td class="text-left" style="min-width: 250px">
            {{ employee.DepartmentName }}
          </td>
          <td class="text-center">{{ employee.BankAccount }}</td>
          <td class="text-left" style="min-width: 300px">
            {{ employee.BankName }}
          </td>
          <td class="text-left" style="min-width: 300px">
            {{ employee.BankBranch }}
          </td>

          <div
            class="table__action-wrapper"
            v-if="showActionIndex === index"
            ref="action"
          >
            <div class="table__action">
              <div
                class="table__action-edit"
                @click="btnEditEmployeeOnClick(index)"
                title="Chỉnh sửa"
              >
                <VIcon class="icon-pen"></VIcon>
              </div>
              <div
                class="table__action-clone"
                @click="btnCloneOnClick(index)"
                v-if="isShowActionMore"
                title="Nhân bản"
              >
                <VIcon class="icon-clone"></VIcon>
              </div>
              <div
                class="table__action-delete"
                @click="btnToggleDialogOnClick(index)"
                v-if="isShowActionMore"
                title="Xoá"
              >
                <VIcon class="icon-delete"></VIcon>
              </div>
              <div
                class="table__action-more"
                @click="showActionMore"
                title="Chức năng khác"
                v-if="!isShowActionMore"
              >
                <VIcon class="icon-more"></VIcon>
              </div>
            </div>
          </div>
        </tr>
      </template>
    </VTable>
  </div>
  <VPaging
    :totalRecord="this.totalRecord"
    @previous="previousPage"
    @next="nextPage"
    @pageSize="setPageSize"
    ref="pagination"
    :first-index="firstIndex"
    :last-index="lastIndex"
  />

  <EmployeeDetail
    ref="employeeDetail"
    v-if="isShowPopup"
    :employeeReceived="employee"
    :key="formKey"
    @openFormAdd="onOpenFormAdd"
    :isCloneEvent="isCloneEvent"
    :date-format="formatDate"
    :refresh="refreshData"
  />

  <Teleport to="body">
    <VDialog
      :title="this.$vndialogTitles.CONFIRM_TITLE"
      :desc="dlgDeleteDesc"
      :ok-text="this.$vnbutton.DELETE"
      :cancel-text="this.$vnbutton.CANCEL"
      v-if="isShowDlgDelete"
      ref="dlgDelete"
      type="confirm"
      @closeDialog="btnToggleDialogOnClick"
      @onOkDialog="deleteEmployee"
    ></VDialog>

    <VDialog
      :title="this.$vndialogTitles.CONFIRM_TITLE"
      :desc="
        this.$vndialogMessages.DELETE_MANY_CONFIRM_MESSAGE(
          getSelectedRecordCount()
        )
      "
      :ok-text="this.$vnbutton.DELETE"
      :cancel-text="this.$vnbutton.CANCEL"
      v-if="isShowDlgDeleteMany"
      type="confirm"
      @closeDialog="btnToggleDialogDeleteManyOnClick"
      @onOkDialog="deleteManyEmployee"
    ></VDialog>
  </Teleport>
</template>
<script>
import {
  deleteEmployee,
  deleteManyEmployee,
  filterEmployee,
  exportExcel,
} from "@/utils/employee";
import { getFormat } from "@/utils/setting";
import VPaging from "@/components/VPaging.vue";
import VButton from "@/components/VButton.vue";
import VIcon from "@/components/VIcon.vue";
import VDialog from "@/components/VDialog.vue";
import VTable from "@/components/VTable.vue";
import VInput from "@/components/VInput.vue";
import EmployeeDetail from "./EmployeeDetail.vue";
// import VDate from "@/components/VDate.vue";
export default {
  name: "EmployeeList",
  emits: ["editEmployee", "cloneEmployee"],
  components: {
    VPaging,
    VButton,
    VIcon,
    VDialog,
    VTable,
    VInput,
    EmployeeDetail,
  },
  created() {
    this.loadData();
    this.restoreFormatDate();
    this.$_emitter.on("dateFormat", this.setFormatDate);
    this.$_emitter.on("onClosePopup", this.onClosePopup);
  },
  async mounted() {
    const format = await getFormat();
    console.log(format);
    this.formatDate = format;
  },
  props: ["refresh"],
  data() {
    return {
      employeesFromDatabase: [],
      employees: [],
      checkedEmployees: [],
      employee: null,
      showActionIndex: null,
      isShowDlgDelete: false,
      isShowDlgDeleteMany: false,
      isShowActionMore: false,
      indexToDelete: null,
      dlgDeleteDesc: "",
      dlgDeleteManyDesc: "",
      searchText: "",
      pageSize: 30,
      pageNumber: 1,
      deleteIds: [],
      checkedIds: [],
      saveIds: [],
      totalRecord: 0,
      debounceTimeout: null,
      firstIndex: 1,
      lastIndex: 1,
      tempIndex: 0,
      isCloneEvent: false,
      isShowPopup: false,
      formKey: 0,
      toggleSort: false,
      count: 0,
      formatDate: "",
    };
  },
  methods: {
    /**
     * Xử lí sự kiện sau khi bấm vào nút Thêm mới nhân viên
     * Author: VTThanh (30/8/2023)
     */
    btnAddOnClick() {
      this.uncheckedAll();
      this.onShowPopup();
    },
    binding() {
      this.employees = this.employeesFromDatabase.map((employee) => ({
        ...employee,
        isChecked: false,
      }));
    },
    /**
     * Load dữ liệu vào bảng
     * Author: VTThanh (30/08/2023)
     */
    async loadData() {
      try {
        const result = await filterEmployee(
          this.pageSize,
          this.pageNumber,
          this.searchText
        );
        this.employeesFromDatabase = result.Results;

        this.binding();

        this.employees.forEach((employee) => {
          employee.isChecked = this.checkedEmployees.some(
            (emp) => emp.EmployeeId === employee.EmployeeId
          );
          if (employee.isChecked) {
            this.count++;
          }
        });

        // lấy totalRecord
        this.totalRecord = result.TotalCount;

        if (this.totalRecord < this.lastIndex) {
          this.lastIndex = this.totalRecord;
        }

        // Thứ tự bản ghi
        if (this.pageNumber === 1) {
          this.firstIndex = 1;
          this.lastIndex = this.pageSize;

          const pagination = this.$refs.pagination;
          pagination.setDisabledPrevButton(true);
        }

        if (this.lastIndex === this.totalRecord) {
          const pagination = this.$refs.pagination;
          pagination.setDisabledNextButton(true);
        }
      } catch (error) {
        return error;
      }
    },

    /**
     * Refresh dữ liệu trong bảng
     * Author: VTThanh (30/8/2023)
     */
    async refreshData() {
      console.log(this.pageSize);
      this.setPageSize(this.pageSize);
      this.searchText = "";
      // this.paginationKey += 1;
      await this.loadData();
      this.uncheckedAll();
    },

    /**
     * Hiển thị action menu
     * Author: VTThanh (05/09/2023)
     */
    showActions(index) {
      this.showActionIndex = index;
    },
    /**
     * Hiển thị thêm các action (nhân bản, xoá)
     * Author: VTThanh (05/09/2023)
     */
    showActionMore() {
      this.isShowActionMore = true;
    },
    /**
     * Ẩn action menu
     * Author: VTThanh (05/09/2023)
     */
    hideActions() {
      this.showActionIndex = null;
      this.isShowActionMore = false;
    },

    /**
     * Thực hiện việc xoá một nhân viên
     * Author: VTThanh (10/9/2023)
     */
    async deleteEmployee() {
      let employeeId =
        this.employeesFromDatabase[this.indexToDelete].EmployeeId;
      await deleteEmployee(employeeId);

      this.btnToggleDialogOnClick(null);
      this.$_vcommon.showToast(this.$vndialogMessages.DELETE_SUCCESS);

      await this.loadData();
    },
    /**
     * Xử lí sự kiện sau khi bấm vào nút Delete
     * Author: VTThanh (10/9/2023)
     */
    btnToggleDialogOnClick(index) {
      //1. Mở Dialog xác nhận xoá
      this.isShowDlgDelete = !this.isShowDlgDelete;
      //2. Lưu index của nhân viên cần xoá vào biến indexToDelete
      this.indexToDelete = index;
    },

    /**
     * Xử lí sự kiện editEmployee
     * Author: VTThanh (10/9/2023)
     */
    onEditEmployee(employee) {
      // this.employee = Object.assign({}, employee);
      this.employee = {
        ...employee,
      };
      this.onShowPopup();
    },

    /**
     * Xử lí sự kiện sau khi bấm vào nút Edit
     * Author: VTThanh (10/09/2023)
     */
    btnEditEmployeeOnClick(index) {
      this.uncheckedAll();
      this.onEditEmployee(this.employeesFromDatabase[index]);
    },

    /**
     * Xử lí sự kiện cloneEmployee
     * Author: VTThanh (10/9/2023)
     */
    onCloneEmployee(employee) {
      this.employee = Object.assign({}, employee);
      this.onShowPopup();
      this.isCloneEvent = true;
    },

    /**
     * Xử lí sự kiện sau khi bấm vào nút Clone
     * Author: VTThanh (09/09/2023)
     */
    btnCloneOnClick(index) {
      this.uncheckedAll();
      this.onCloneEmployee(this.employeesFromDatabase[index]);
    },

    /**
     * Xử lí sự kiện showPopup
     * Author: VTThanh (10/9/2023)
     */
    onShowPopup() {
      this.isShowPopup = true;
    },
    /**
     * Xử lí sự kiện closePopup
     * Author: VTThanh (10/9/2023)
     */
    onClosePopup() {
      this.isShowPopup = false;
      this.employee = null;
      this.isCloneEvent = false;
    },
    /**
     * Xử lí sự kiện khi bấm vào nút Cất và thêm
     * Author: VTThanh (02/09/2023)
     */
    onOpenFormAdd() {
      this.onClosePopup();
      this.onShowPopup();
      this.formKey += 1;
    },

    /**
     * Xử lí khi click vào ô checkbox
     * Author: VTThanh (09/09/2023)
     */
    checkboxHandle(event, index) {
      event.stopPropagation();

      const employeeId = this.employeesFromDatabase[index].EmployeeId;
      const employee = this.employees.find(
        (emp) => emp.EmployeeId === employeeId
      );
      const isChecked = event.target.checked;
      if (isChecked) {
        if (employee) {
          employee.isChecked = true;
          this.checkedEmployees.push(employee);
          console.log(this.checkedEmployees);
          this.count++;
        }
        if (this.count === this.pageSize) {
          this.$refs.checkAll.checked = true;
        }
      } else {
        if (employee) {
          employee.isChecked = false;
          this.count--;
        }
        const checkedIndex = this.checkedEmployees.findIndex(
          (emp) => emp.EmployeeId === employeeId
        );
        if (checkedIndex > -1) {
          this.checkedEmployees.splice(checkedIndex, 1);
        }
        if (this.count !== this.pageSize) {
          this.$refs.checkAll.checked = false;
        }
      }
    },
    isSelected(employee) {
      return this.checkedEmployees.some(
        (emp) => emp.EmployeeId === employee.EmployeeId
      );
    },
    /**
     * Chuyển về trang trước
     * Author: VTThanh (05/09/2023)
     */
    async previousPage() {
      const pagination = this.$refs.pagination;
      pagination.setDisabledNextButton(false);
      if (this.pageNumber <= 1) {
        pagination.setDisabledPrevButton(true);
        return;
      } else {
        this.pageNumber--;
        this.count = 0;
        await this.loadData();
        this.binding();
        this.employees.forEach((employee) => {
          employee.isChecked = this.checkedEmployees.some(
            (emp) => emp.EmployeeId === employee.EmployeeId
          );
          if (employee.isChecked) {
            this.count++;
          }
        });

        const allEmployeesChecked = this.employees.every(
          (employee) => employee.isChecked
        );

        this.$refs.checkAll.checked = allEmployeesChecked;
      }
      // Giảm thứ tự bản ghi
      if (this.lastIndex === this.totalRecord) {
        this.firstIndex -= this.pageSize;
        this.lastIndex -= this.tempIndex;
      } else if (this.pageNumber === 1) {
        this.firstIndex = 1;
        this.lastIndex = this.pageSize;
      } else {
        this.firstIndex -= this.pageSize;
        this.lastIndex -= this.pageSize;
      }
    },
    /**
     * Chuyển sang trang sau
     * Author: VTThanh (05/09/2023)
     */
    async nextPage() {
      const pagination = this.$refs.pagination;
      pagination.setDisabledPrevButton(false);
      if (this.employeesFromDatabase.length < this.pageSize) {
        return;
      } else {
        this.count = 0;
        this.pageNumber++;
        await this.loadData();
        this.binding();
        this.employees.forEach((employee) => {
          employee.isChecked = this.checkedEmployees.some(
            (emp) => emp.EmployeeId === employee.EmployeeId
          );
          if (employee.isChecked) {
            this.count++;
          }
        });

        const allEmployeesChecked = this.employees.every(
          (employee) => employee.isChecked
        );

        this.$refs.checkAll.checked = allEmployeesChecked;
      }

      // Tăng thứ tự bản ghi
      this.firstIndex += this.pageSize;
      this.lastIndex += this.pageSize;

      if (this.lastIndex >= this.totalRecord) {
        this.lastIndex = this.totalRecord;
        this.tempIndex = this.lastIndex - this.firstIndex + 1;
        pagination.setDisabledNextButton(true);
      }
    },
    /**
     * Tìm kiếm nhân viên
     * Author: VTThanh (05/09/2023)
     */
    async searchEmployee() {
      // Hủy bất kỳ hẹn giờ debounce trước đó nếu có
      if (this.debounceTimeout) {
        clearTimeout(this.debounceTimeout);
      }

      // Đặt một hẹn giờ debounce mới
      this.debounceTimeout = setTimeout(async () => {
        // this.pageSize = 10;
        this.pageNumber = 1;
        await this.loadData();
        const pagination = this.$refs.pagination;
        pagination.setDisabledPrevButton(true);
        if (this.employees.length < this.pageSize) {
          pagination.setDisabledNextButton(true);
        }
        this.uncheckedAll();
      }, 500); // Thời gian chờ debounce
    },

    /**
     * Chọn số lượng bản ghi/trang
     * Author: VTThanh (05/09/2023)
     */
    setPageSize(pageSize) {
      this.pageSize = pageSize;
      this.pageNumber = 1;
      this.loadData();
      const pagination = this.$refs.pagination;
      pagination.setDisabledPrevButton(true);
      this.uncheckedAll();
    },
    /**
     * Xoá nhiều bản ghi
     * Author: VTThanh (05/09/2023)
     */
    async deleteManyEmployee() {
      this.deleteIds = this.checkedEmployees.map(
        (employee) => employee.EmployeeId
      );
      console.log(this.deleteIds);
      await deleteManyEmployee(this.deleteIds);
      this.checkedEmployees = [];
      this.btnToggleDialogDeleteManyOnClick();
      this.$_vcommon.showToast(this.$vndialogMessages.DELETE_SUCCESS);
      await this.loadData();
      this.uncheckedAll();
    },

    /**
     * Nút chọn tất cả bản ghi
     * Author: VTThanh (05/09/2023)
     */
    checkedAll() {
      if (this.$refs.checkAll.checked) {
        this.employees = this.employees.map((employee) => ({
          ...employee,
          isChecked: true,
        }));
        this.count = this.employees.length;
        this.checkedEmployees = [...this.checkedEmployees, ...this.employees];
      } else {
        this.uncheckedAll();
      }
    },
    /**
     * Nút bỏ chọn tất cả bản ghi
     * Author: VTThanh (05/09/2023)
     */
    uncheckedAll() {
      this.$refs.checkAll.checked = false;
      this.employees = this.employees.map((employee) => ({
        ...employee,
        isChecked: false,
      }));
      this.checkedEmployees = [];
    },
    /**
     * Lấy số lượng bản ghi được chọn
     * Author: VTThanh (05/09/2023)
     */
    getSelectedRecordCount() {
      return this.checkedEmployees.length;
    },
    /**
     * Ẩn hiện dialog xoá nhiều
     * Author: VTThanh (05/09/2023)
     */
    btnToggleDialogDeleteManyOnClick() {
      this.isShowDlgDeleteMany = !this.isShowDlgDeleteMany;
    },

    /**
     * sort table
     * Author: VTThanh (26/9/2023)
     */
    sortTable(column) {
      this.toggleSort = !this.toggleSort;
      const sortColumn = (a, b) => {
        if (a[column] < b[column]) {
          return this.toggleSort ? -1 : 1; // Sắp xếp tăng dần hoặc giảm dần
        }
        if (a[column] > b[column]) {
          return this.toggleSort ? 1 : -1; // Sắp xếp tăng dần hoặc giảm dần
        }
        return 0;
      };

      // Kiểm tra cột để sắp xếp
      if (column === "EmployeeCode") {
        this.employeesFromDatabase.sort(sortColumn);
      }
    },

    /**
     * Format Date
     * Author: VTThanh (5/10/2023)
     */
    setFormatDate(value) {
      this.formatDate = value;
    },

    restoreFormatDate() {
      const savedFormatDate = localStorage.getItem("dateFormat");
      if (savedFormatDate) {
        this.formatDate = savedFormatDate;
      }
    },

    /**
     * Xuất excel
     * Author: VTThanh (06/09/2023)
     */
    async exportToExcel() {
      try {
        const response = await exportExcel();
        console.log(response);
        if (response) {
          const url = window.URL.createObjectURL(new Blob(response));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        } else {
          console.log("Dữ liệu file Excel không khả dụng.");
          this.$_vcommon.showDialog(
            this.$vndialogTitles.CONFIRM_TITLE,
            this.$vndialogMessages.EXCEL_DATA_INVALID
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    indexToDelete: function (newIndex) {
      const employeeCode = this.employeesFromDatabase[newIndex]?.EmployeeCode;
      this.dlgDeleteDesc =
        this.$vndialogMessages.DELETE_CONFIRM_MESSAGE(employeeCode);
    },
  },
};
</script>
<style scoped>
@import url("../../css/components/table.css");
@import url("../../css/components/toolbar.css");
.container-header {
  width: 100%;
  height: 62px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px 0;
}

.container-header__title {
  font-size: 24px;
  font-weight: 700;
  color: #1f1f1f;
}
</style>
