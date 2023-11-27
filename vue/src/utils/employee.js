import { callApi } from "./request";

/**
 * Hàm lấy tất cả nhân viên
 * Author: VTThanh (05/09/2023)
 */
async function getEmployees() {
  return await callApi("/Employees", "GET");
}

/**
 * Hàm lọc nhân viên
 * Author: VTThanh (05/09/2023)
 */
async function filterEmployee(pageSize, pageNumber, searchText) {
  try {
    return await callApi(
      `/Employees/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&searchText=${searchText}`,
      "GET"
    );
  } catch (error) {
    console.log(error);
  }
}

/**
 * Hàm lấy mã nhân viên mới
 * Author: VTThanh (05/09/2023)
 */
async function getNewEmployeeCode() {
  try {
    return await callApi("/Employees/NewEmployeeCode", "GET");
  } catch (error) {
    console.log(error);
  }
}

/**
 * Kiểm tra mã đã tồn tại hay chưa
 * Author: VTThanh (05/09/2023)
 */
async function isExistCode(code) {
  return await callApi(`/Employees/IsDuplicateEmployeeCode/${code}`, "GET");
}

/**
 * Xoá nhân viên
 * Author: VTThanh (05/09/2023)
 */
async function deleteEmployee(employeeId) {
  await callApi(`/Employees/${employeeId}`, "DELETE");
}

/**
 * Xoá nhiều nhân viên
 * Author: VTThanh (05/09/2023)
 */
async function deleteManyEmployee(deleteIds) {
  await callApi("/Employees", "DELETE", deleteIds);
}

/**
 * Thêm nhân viên mới
 * Author: VTThanh (05/09/2023)
 */
async function addEmployee(employeeData) {
  await callApi("/Employees", "POST", employeeData);
}

/**
 * Sửa nhân viên
 * Author: VTThanh (05/09/2023)
 */
async function updateEmployee(employeeId, employeeData) {
  await callApi(`/Employees/${employeeId}`, "PUT", employeeData);
}

async function exportExcel() {
  await callApi("/Employees/ExportExcel", "GET", null, {
    responseType: "blob",
  });
}

export {
  getEmployees,
  deleteEmployee,
  getNewEmployeeCode,
  addEmployee,
  updateEmployee,
  isExistCode,
  filterEmployee,
  deleteManyEmployee,
  exportExcel,
};
