export default {
  async register({ rootGetters }, payload) {
    const res = await fetch(
      `${rootGetters["getSiteLink"]}Authentication/register-user`,
      {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      }
    );

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to create new user!");
      throw error;
    }
  },

  async getUser({ commit, dispatch, rootGetters }) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `${rootGetters["getSiteLink"]}Authentication/get-user`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load user data!");
      throw error;
    }
    var user = {
      FirstName: data.firstName,
      LastName: data.lastName,
      Email: data.email,
    };

    commit("setCurrentUser", user);
  },

  async editUser({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `${rootGetters["getSiteLink"]}Authentication/edit-user`,
      {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
        body: JSON.stringify(payload),
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to update user data!");
      throw error;
    }
  },

  async login({ commit, rootGetters }, payload) {
    const res = await fetch(
      `${rootGetters["getSiteLink"]}Authentication/login-user`,
      {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      }
    );

    const data = await res.json();

    if (!res.ok) {
      const error = new Error(data || "Failed to login!");
      throw error;
    }
    commit("setUserLoginData", data);
    localStorage.setItem("userId", data.id);
    localStorage.setItem("token", data.tokenValue.token);
    localStorage.setItem("refreshToken", data.tokenValue.refreshToken);
    localStorage.setItem("expiresAt", data.tokenValue.expiresAt);
  },

  async logout(context) {
    context.commit("logout");
  },

  setUserData(context, payload) {
    context.commit("setUserData", payload);
  },

  async checkTokens({ commit, rootGetters, getters }) {
    if (
      getters.userId !== null &&
      new Date(getters.expiresAt).getTime() + 120000 <= Date.now()
    ) {
      const res = await fetch(
        `${rootGetters["getSiteLink"]}authentication/refresh-token`,
        {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(getters.token),
        }
      );

      const data = await res.json();
      if (!res.ok) {
        const error = new Error(data || "Failed to refresh token!");
        throw error;
      }

      commit("setToken", data);
    }
  },
};
