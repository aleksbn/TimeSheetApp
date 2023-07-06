import { createRouter, createWebHistory } from "vue-router";

import store from "./store/index.js";

import AboutUs from "./pages/instructions/AboutUs.vue";
import UserManual from "./pages/instructions/UserManual.vue";
import InstallationManual from "./pages/instructions/InstallationManual.vue";
import AddCompany from "./pages/companies/AddCompany.vue";
import AddDepartment from "./pages/departments/AddDepartment.vue";
import AddEmployee from "./pages/employees/AddEmployee.vue";
import AddUser from "./pages/auth/AddUser.vue";
import AuthPage from "./pages/auth/AuthPage.vue";
import Companies from "./pages/companies/CompaniesList.vue";
import CompanyDetails from "./pages/companies/CompanyDetails.vue";
import GenerateDataPage from "./pages/generate/GenerateDataPage.vue";
import EditUserPage from "./pages/auth/EditUser.vue";
import Employees from "./pages/employees/EmployeesList.vue";
import EmployeeDetails from "./pages/employees/EmployeeDetails.vue";
import Departments from "./pages/departments/DepartmentsList.vue";
import DepartmentDetails from "./pages/departments/DepartmentDetails.vue";
import LoginPage from "./pages/auth/LoginPage.vue";
import StatisticsPage from "./pages/statistics/StatisticsPage.vue";
import WorkingTimes from "./pages/workingtimes/WorkingTimesList.vue";
import NotFound from "./pages/NotFound.vue";
import LogoutPage from "./pages/auth/LogoutPage.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", redirect: "/about" },
    {
      path: "/addcompany",
      component: AddCompany,
      meta: { requiresAuth: true },
    },
    {
      path: "/installationmanual",
      component: InstallationManual,
    },
    {
      path: "/usermanual",
      component: UserManual,
    },
    {
      path: "/adddepartment",
      component: AddDepartment,
      meta: { requiresAuth: true },
    },
    {
      path: "/addemployee",
      component: AddEmployee,
      meta: { requiresAuth: true },
    },
    { path: "/about", component: AboutUs },
    { path: "/auth", component: AuthPage, meta: { requiresUnauth: true } },
    {
      path: "/calculations/:comid",
      component: StatisticsPage,
      props: true,
      meta: { requiresAuth: true },
    },
    { path: "/companies", component: Companies, meta: { requiresAuth: true } },
    {
      path: "/companies/:comid",
      component: CompanyDetails,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/departments/:comid/",
      component: Departments,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/departments/:comid/:depid",
      component: DepartmentDetails,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/employees/:comid",
      component: Employees,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/employees/:comid/:depid",
      component: Employees,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/employeedetails/:empid",
      component: EmployeeDetails,
      props: true,
      meta: { requiresAuth: true },
    },
    {
      path: "/generate/:comid",
      component: GenerateDataPage,
      props: true,
      meta: { requiresAuth: true },
    },
    { path: "/login", component: LoginPage, meta: { requiresUnauth: true } },
    {
      path: "/editprofile",
      component: EditUserPage,
      meta: { requiresAuth: true },
    },
    { path: "/logout", component: LogoutPage, meta: { requiresAuth: true } },
    { path: "/register/", component: AddUser, meta: { requiresUnauth: true } },
    {
      path: "/workingtimes/:id",
      component: WorkingTimes,
      props: true,
      meta: { requiresAuth: true },
    },
    { path: "/:notFOund(.*)", component: NotFound },
  ],
});

router.beforeEach(function (to, from, next) {
  if (to.meta.requiresAuth && !store.getters["auth/isAuthenticated"]) {
    next("/auth");
  } else if (to.meta.requiresUnauth && store.getters["auth/isAuthenticated"]) {
    next("/logout");
  } else {
    next();
  }
});

export default router;
