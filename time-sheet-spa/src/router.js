import { createRouter, createWebHistory } from "vue-router";

import AboutUs from "./pages/AboutUs.vue";
import Companies from "./pages/companies/CompaniesList.vue";
import CompanyDetails from "./pages/companies/CompanyDetails.vue";
import Employees from "./pages/employees/EmployeesList.vue";
import EmployeeDetails from "./pages/employees/EmployeeDetails.vue";
import Departments from "./pages/departments/DepartmentsList.vue";
import DepartmentDetails from "./pages/departments/DepartmentDetails.vue";
import WorkingTimes from "./pages/workingtimes/WorkingTimesList.vue";
import NotFound from "./pages/NotFound.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", redirect: "/about" },
    { path: "/about", component: AboutUs },
    { path: "/companies", component: Companies },
    { path: "/companies/:comid", component: CompanyDetails, props: true },
    { path: "/departments/:comid/", component: Departments, props: true },
    {
      path: "/departments/:comid/:depid",
      component: DepartmentDetails,
      props: true,
    },
    { path: "/employees/:comid", component: Employees, props: true },
    { path: "/employees/:comid/:depid", component: Employees, props: true },
    { path: "/employeedetails/:empid", component: EmployeeDetails, props: true },
    { path: "/workingtimes/:id", component: WorkingTimes, props: true },
    { path: "/:notFOund(.*)", component: NotFound },
  ],
});

export default router;
