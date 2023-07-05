export default {
  employees(state) {
    return state.employees;
  },
  hasEmployees(state) {
    return !!state.employees && state.employees.length > 0;
  },
  employee(state) {
    return state.employee;
  },
  hasEmployee(state) {
    return !!state.employee;
  },
  numberOfEmployees(state) {
    return state.emCount;
  }
};
