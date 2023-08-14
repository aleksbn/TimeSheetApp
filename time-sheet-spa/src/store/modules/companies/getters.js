export default {
	companies(state) {
		return state.companies;
	},
	hasCompanies(state) {
		return !!state.companies && state.companies.length > 0;
	},
	company(state) {
		return state.company;
	},
	hasCompany(state) {
		return !!state.company;
	},
};
