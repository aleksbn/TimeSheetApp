export default {
	setEmployees(state, payload) {
		state.emCount = payload.emCount;
		state.employees = payload.employees;
	},
	setEmployee(state, payload) {
		state.employee = payload;
	},
};
