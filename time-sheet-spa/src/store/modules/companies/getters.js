export default {
  companies(state) {
    return state.companies;
  },
  hasCompanies(state) {
    return !!state.companies && state.companies.length > 0;
  },
};
