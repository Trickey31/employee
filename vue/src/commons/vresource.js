const resource = {
  VN: {
    SearchInputHintText: "Tìm theo mã, tên nhân viên",
    dialog: {
      titles: {
        SUCCESS: "Thành công",
        WARNING: "Cảnh báo",
        ERROR: "Lỗi",

        CONFIRM_TITLE: "Thông báo",
        SAVE_CONFIRM_TITLE: "Lưu thay đổi?",
        UPDATE_CONFIRM_TITLE: "Cập nhật thông tin?",
      },
      messages: {
        REFRESH_SUCCESS: "Làm mới thành công!",
        ADD_SUCCESS: "Thêm mới thành công!",
        DELETE_SUCCESS: "Xóa thành công",
        UPDATE_SUCCESS: "Cập nhật thành công",

        SAVE_CONFIRM_MESSAGE: "Bạn có chắc chắn muốn lưu không?",
        UPDATE_CONFIRM_MESSAGE: "Bạn có chắc chắn sửa thông tin này không?",
        DELETE_CONFIRM_MESSAGE: (employeeCode) =>
          `Bạn có thực sự muốn xoá nhân viên <${employeeCode}> không?`,
        DELETE_MANY_CONFIRM_MESSAGE: (employeeCount) =>
          `Bạn có thực sự muốn xoá ${employeeCount} nhân viên đã chọn không?`,
        CHANGE_CONFIRM_MESSAGE:
          "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",

        EMPLOYEE_CODE_EXIST: (employeeCode) =>
          `Mã nhân viên <${employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
        EXCEL_DATA_INVALID: "Dữ liệu file Excel không khả dụng.",
      },
    },
    buttons: {
      AGREE: "Đồng ý",
      CANCEL: "Hủy",
      SAVE_ADD: "Cất và Thêm",
      SAVE: "Cất",
      DELETE: "Xóa",
      YES: "Có",
      NO: "Không",
    },
    form: {
      EMPLOYEE_ADD_TITLE: "Thêm mới nhân viên",
      EMPLOYEE_DETAIL_TITLE: "Thông tin nhân viên",
      CODE_NOT_EMPTY: "Mã không được để trống",
      CODE_EXISTED: "Mã đã tồn tại",
      NAME_NOT_EMPTY: "Tên không được để trống",
      UNIT_NOT_EMPTY: "Đơn vị không được để trống",
      EMAIL_INVALID: "Email không đúng định dạng",
    },
  },
  EN: {},
};
export default resource;
