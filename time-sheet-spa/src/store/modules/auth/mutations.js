export default {
	setUser(state, payload) {
		state.userId = payload.Id;
	},

	setUserLoginData(state, payload) {
		state.userId = payload.id;
		state.token = payload.tokenValue.token;
		state.refreshToken = payload.tokenValue.refreshToken;
		state.expiresAt = payload.tokenValue.expiresAt;
	},

	setUserData(state, payload) {
		state.userId = payload.id;
		state.token = payload.token;
		state.refreshToken = payload.refreshToken;
		state.expiresAt = payload.expiresAt;
	},

	setToken(state, payload) {
		state.token = payload.token;
		state.refreshToken = payload.refreshToken;
		state.expiresAt = payload.expiresAt;
	},

	setCurrentUser(state, payload) {
		state.currentUser = payload;
	},

	logout(state) {
		state.userId = null;
		state.token = "";
		state.refreshToken = "";
		state.expiresAt = 0;
		localStorage.removeItem("userId");
		localStorage.removeItem("refreshToken");
		localStorage.removeItem("token");
		localStorage.removeItem("expiresAt");
	},
};
