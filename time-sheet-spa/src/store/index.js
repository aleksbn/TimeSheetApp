import { createStore } from "vuex";

import companiesModule from "./modules/companies/index.js";
import departmentsModule from "./modules/departments/index.js";
import employeesModule from "./modules/employees/index.js";
import workingTimesModule from "./modules/workingtimes/index.js";

const store = createStore({
  modules: {
    companies: companiesModule,
    departments: departmentsModule,
    employees: employeesModule,
    workingTimes: workingTimesModule,
  },
});

export default store;
