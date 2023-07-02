export default {
  async register(_1, payload) {
    const res = await fetch(
      "https://localhost:7059/api/Authentication/register-user",
      {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to create new user!");
      throw error;
    }
  },

  async editUser(_1, payload) {
    const res = await fetch(
      "https://localhost:7059/api/Authentication/edit-user",
      {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to update user data!");
      throw error;
    }
  },

  setUserData(context, payload) {
    context.commit('setUserData', payload);
  },

  async login(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/Authentication/login-user",
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
      const error = new Error(res.message || "Failed to update user data!");
      throw error;
    }
    context.commit('setUserLoginData', data);
    localStorage.setItem("userId", data.id);
    localStorage.setItem("token", data.tokenValue.token);
    localStorage.setItem("refreshToken", data.tokenValue.refreshToken);
    localStorage.setItem("expiresAt", data.tokenValue.expiresAt);
  },

  async logout(context) {
    context.commit("logout");
  },

  async checkTokens({ commit, getters}) {
    if (getters.userId !== null && (new Date(getters.expiresAt).getTime() + 120000) <= Date.now()) {
      const res = await fetch(
        "https://localhost:7059/api/authentication/refresh-token",
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
        const error = new Error(data.message || "Failed to refresh token!");
        throw error;
      }
      
      commit("setToken", data);
    }
  },
};
