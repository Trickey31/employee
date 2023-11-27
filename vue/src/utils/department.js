import { callApi } from "./request";

async function getDepartments() {
  return await callApi("/Departments", "GET");
}

export { getDepartments };
