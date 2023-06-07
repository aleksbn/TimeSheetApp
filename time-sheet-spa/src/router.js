import { createRouter, createWebHistory } from "vue-router";

import AboutUs from "./pages/AboutUs.vue";
import Companies from "./pages/companies/CompaniesList.vue";
import CompanyDetails from "./pages/companies/CompanyDetails.vue";
import Employees from "./pages/employees/EmployeesList.vue";
import EmployeeDetails from "./pages/employees/EmployeeDetails.vue";
import Departments from "./pages/departments/DepartmentsList.vue";
import DepartmentDetails from "./pages/departments/DepartmentDetails.vue";
import WorkingTimes from "./pages/workingtimes/WorkingTimesList.vue";
import WorkingTimeDetails from "./pages/workingtimes/WorkingTimeDetails.vue";
import NotFound from "./pages/NotFound.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", redirect: "/about" },
    { path: "/about", component: AboutUs },
    { path: "/companies", component: Companies },
    {
      path: "/companies/:comid",
      component: CompanyDetails,
      props: true,
      children: [
        { path: "departments", component: Departments },
        {
          path: "departments/:depid",
          component: DepartmentDetails,
          props: true,
          children: [
            { path: "employees", component: Employees },
            { path: "employees/:emid", component: EmployeeDetails },
            { path: "workingtimes", component: WorkingTimes },
            { path: "workingtimes/:wtid", component: WorkingTimeDetails },
          ],
        },
        { path: "employees", component: Employees },
        {
          path: "employees/:emid",
          component: EmployeeDetails,
          props: true,
          children: [
            { path: "workingtimes", component: WorkingTimes },
            { path: "workingtimes/:wtid", component: WorkingTimeDetails },
          ],
        },
        { path: "workingtimes", component: WorkingTimes },
        {
          path: "workingtimes/:wtid",
          component: WorkingTimeDetails,
          props: true,
        },
      ],
    },
    { path: "/:notFOund(.*)", component: NotFound },
  ],
});

export default router;
