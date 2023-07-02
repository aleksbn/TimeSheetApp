export default {
  isAuthenticated(state) {
    return !!state.userId && !!state.token;
  },
  token(state) {
    return {
        token: state.token,
        refreshToken: state.refreshToken
    };
  },
  expiresAt(state) {
    return state.expiresAt;
  }
};
