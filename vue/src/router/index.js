import EmployeeListVue from "@/pages/employee/EmployeeList.vue";
// import OverviewVue from "@/pages/overview/Overview.vue";
import ReportVue from "@/pages/report/Report.vue";
import VDateVue from "@/components/VDate.vue";
import { createRouter, createWebHistory } from "vue-router";

// Định nghĩa router
const routers = [
  {
    path: "/",
    name: "Overview",
    component: EmployeeListVue,
    children: [{ path: "detail", component: ReportVue }],
  },
  { path: "/report", name: "Report", component: ReportVue },
  { path: "/option", name: "Option", component: VDateVue },
  { path: "/bill", name: "Bill", component: ReportVue },
];

const vueRouter = createRouter({
  history: createWebHistory(),
  routes: routers,
});

export default vueRouter;
