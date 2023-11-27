import axios from "axios";
import common from "@/commons/vcommon";
import resource from "@/commons/vresource";

const API_BASE_URL = "https://localhost:8081/api/v1";

/**
 * Tạo instance để dừng việc màn hình báo lỗi đổ khi có lỗi từ server
 * Author: VTThanh (2/10/2023)
 */
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
});

axiosInstance.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    if (error.response) {
      handleApiError(error);
    }
    return Promise.reject(error);
  }
);

/**
 * Phương thức chung dùng để gọi api
 * @param {String} endpoint đường dẫn
 * @param {String} method phương thức ["GET", "POST", "PUT", "DELETE"]
 * @param {Object} data dữ liệu đầu vào
 * @returns {Promise} dữ liệu
 * @author VTThanh (2/10/2023)
 */
async function callApi(endpoint, method, data) {
  try {
    common.showLoading();
    const response = await axiosInstance({
      url: endpoint,
      method,
      data,
    });
    common.closeLoading();
    return response.data;
  } catch (error) {
    common.closeLoading();
    throw error;
  }
}

/**
 * Phương thức xử lý lỗi chung do api trả về
 * @param {Object} res Phản hồi api trả về
 * @author VTThanh (2/10/2023)
 */
function handleApiError(res) {
  const response = res.response;
  const statusCode = response.status;
  let msg = response.data?.UserMessage;
  const errors = response.data.Errors?.map((element) => element.ErrorMessage);
  switch (statusCode) {
    case 500:
      common.showDialog(resource["VN"].dialog.titles.CONFIRM_TITLE, msg);
      break;
    case 400:
      common.showDialog(resource["VN"].dialog.titles.CONFIRM_TITLE, errors);
      break;
    default:
      common.showDialog(resource["VN"].dialog.titles.CONFIRM_TITLE, msg);
      break;
  }
}

export { callApi };
