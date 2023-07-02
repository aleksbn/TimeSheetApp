export default {
  async loadWorkingTimes({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const link = payload.link;
    const res = await fetch(
      link +
        payload.id +
        "?PageNumber=" +
        payload.pageNumber +
        "&PageSize=" +
        payload.pageSize,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load working times!");
      throw error;
    }

    const workingTimes = [];
    for (const key in data.toReturn) {
      const wt = {
        ID: data.toReturn[key].id,
        Date: data.toReturn[key].date,
        StartTime: data.toReturn[key].startTime,
        EndTime: data.toReturn[key].endTime,
        EmployeeID: data.toReturn[key].employeeID,
        Employee: data.toReturn[key].employee,
      };
      workingTimes.push(wt);
    }

    commit("setWorkingTimes", {
      wts: workingTimes,
      wtcount: data.count,
    });
  },

  async loadWorkingTime({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `https://localhost:7059/api/workingtime/GetWorkingTime/${payload}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(
        data.message || "Failed to load specific working time!"
      );
      throw error;
    }
    const wt = {
      ID: data.id,
      Employee: data.employee,
      WtDate: data.date,
      StartTime: data.startTime,
      EndTime: data.endTime,
    };
    commit("setWorkingTime", wt);
  },

  async addWorkingTime({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const wtData = {
      EmployeeId: payload.wtEmployeeId,
      Date: payload.wtDate.split(".").reverse().join("/"),
      StartTime: payload.wtStartTime,
      EndTime: payload.wtEndTime,
    };

    const res = await fetch(`https://localhost:7059/api/workingtime/create/`, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(wtData),
    });
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to add working time!");
      throw error;
    }
  },

  async deleteWorkingTime({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `https://localhost:7059/api/workingtime/deleteWorkingTime/${payload.id}`,
      {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to delete working time!");
      throw error;
    }
  },

  async editWorkingTime({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const wt = {
      ID: payload.wtId,
      Date: payload.wtDate.split(".").reverse().join("/"),
      StartTime: payload.wtStartTime,
      EndTime: payload.wtEndTime,
      EmployeeId: payload.wtEmployeeId,
    };

    const res = await fetch(
      "https://localhost:7059/api/workingtime/editWorkingTime",
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(wt),
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to edit working time!");
      throw error;
    }
  },
};
