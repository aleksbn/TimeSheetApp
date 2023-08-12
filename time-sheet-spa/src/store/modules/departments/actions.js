export default {
  async loadDepartments({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `${rootGetters["getSiteLink"]}department/${payload.comid}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load departments!");
      throw error;
    }

    const departments = [];
    for (const key in data) {
      const dep = {
        ID: data[key].id,
        Name: data[key].name,
      };
      departments.push(dep);
    }

    commit("setDepartments", departments);
  },
  async loadDepartment({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `${rootGetters["getSiteLink"]}department/${payload.comid}/${payload.depid}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load department data!");
      throw error;
    }

    var department = {
      ID: data.id,
      Name: data.name,
    };

    commit("setDepartment", department);
  },

  async addDepartment({ dispatch, rootGetters }, payload) {
    const dep = {
      Name: payload.depName,
      CompanyID: payload.comId,
    };

    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(`${rootGetters["getSiteLink"]}department`, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dep),
    });

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to add department!");
      throw error;
    }
  },

  async editDepartment({ dispatch, rootGetters }, payload) {
    const dep = {
      ID: payload.depId,
      Name: payload.depName,
      CompanyID: payload.comId,
    };

    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(`${rootGetters["getSiteLink"]}department`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dep),
    });

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to edit department!");
      throw error;
    }
  },

  async deleteDepartment({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `${rootGetters["getSiteLink"]}department/${payload.depId}?deleteEmployees=${payload.deleteEmployees}&&targetDepId=${payload.targetDepId}`,
      {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    );

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to delete department!");
      throw error;
    }
  },
};
