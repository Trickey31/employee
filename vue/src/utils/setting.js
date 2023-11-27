import { callApi } from "./request";

/**
 * Hàm lấy format
 * Author: VTThanh (09/10/2023)
 */
async function getFormat() {
  return await callApi("/Setting", "GET");
}

/**
 * Hàm cập nhật format
 * Author: VTThanh (09/10/2023)
 */
async function updateFormat(newFormat) {
  console.log(newFormat);
  try {
    await callApi(`/Setting?format=${newFormat}`, "PUT");
  } catch (error) {
    console.log(error);
  }
  // const response = await fetch("https://localhost:8081/api/v1/Setting", {
  //   method: "PUT",
  //   Con
  //   body: JSON.stringify(newFormat),
  // });
  // return response.json();
}

export { getFormat, updateFormat };
