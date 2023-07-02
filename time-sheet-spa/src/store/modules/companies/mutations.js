export default {
  setCompanies(state, payload) {
    state.companies = payload;
  },
  setCompany(state, payload) {
    state.company = payload;
  },
  setToken(state, payload) {
    state.token = payload.token;
    state.refreshToken = payload.refreshToken;
  }
};
