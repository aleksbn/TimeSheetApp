export default {
  departments(state) {
    return state.departments;
  },
  hasDepartments(state) {
    return !!state.departments && state.departments.length > 0;
  },
  department(state) {
    return state.department;
  },
  hasDepartment(state) {
    return !!state.department;
  }
};
