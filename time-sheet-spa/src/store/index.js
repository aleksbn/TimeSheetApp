import { createStore } from "vuex";

import companiesModule from "./modules/companies/index.js";
import departmentsModule from "./modules/departments/index.js";
import employeesModule from "./modules/employees/index.js";
import workingTimesModule from "./modules/workingtimes/index.js";
import authModule from "./modules/auth/index.js";
import statisticsModule from "./modules/statistics/index.js";
import generateModule from "./modules/generate/index.js";

const store = createStore({
  getters: {
    getSiteLink() {
      return "https://timesheet-backend.azurewebsites.net/api/";
      //return "https://localhost:7059/api/";
    },
  },
  modules: {
    companies: companiesModule,
    departments: departmentsModule,
    employees: employeesModule,
    workingTimes: workingTimesModule,
    auth: authModule,
    statistics: statisticsModule,
    generate: generateModule,
  },
});

export default store;
