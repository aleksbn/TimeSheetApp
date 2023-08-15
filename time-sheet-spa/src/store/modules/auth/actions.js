import { AjaxCall } from "@/helpers";

export default {
	async register(payload) {
		try {
			await AjaxCall("Authentication/register-user", "POST", payload, {});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to create new user!");
			throw errorFromAjax;
		}
	},

	async getUser({ commit, dispatch, rootGetters }) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall("Authentication/get-user", "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});

			const user = {
				FirstName: data.firstName,
				LastName: data.lastName,
				Email: data.email,
			};
			commit("setCurrentUser", user);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load user data!");
			throw errorFromAjax;
		}
	},

	async editUser({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall("Authentication/edit-user", "PUT", payload, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to update user data!");
			throw errorFromAjax;
		}
	},

	async login({ commit }, payload) {
		try {
			const data = await AjaxCall(
				"Authentication/login-user",
				"POST",
				payload,
				{}
			);
			commit("setUserLoginData", data);
			localStorage.setItem("userId", data.id);
			localStorage.setItem("token", data.tokenValue.token);
			localStorage.setItem("refreshToken", data.tokenValue.refreshToken);
			localStorage.setItem("expiresAt", data.tokenValue.expiresAt);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to login!");
			throw errorFromAjax;
		}
	},

	async logout(context) {
		context.commit("logout");
	},

	setUserData(context, payload) {
		context.commit("setUserData", payload);
	},

	async checkTokens({ commit, getters }) {
		if (
			getters.userId !== null &&
			new Date(getters.expiresAt).getTime() + 120000 <= Date.now()
		) {
			try {
				const data = await AjaxCall(
					"authentication/refresh-token",
					"POST",
					getters.token,
					{}
				);
				commit("setToken", data);
			} catch (error) {
				const errorFromAjax = new Error(error || "Failed to refresh token!");
				throw errorFromAjax;
			}
		}
	},
};
