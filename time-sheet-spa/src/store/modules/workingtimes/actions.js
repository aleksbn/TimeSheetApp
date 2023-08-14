import { API_URL } from "@/config";
import { TIMEOUT_SECONDS } from "@/config";
import { timeout } from "@/helpers";

export default {
  async loadWorkingTimes({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const link = payload.link;
    const request = await fetch(
      `${API_URL}${link}${payload.id}?PageNumber=${payload.pageNumber}&PageSize=${payload.pageSize}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load working times!");
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
    const request = await fetch(
      `${API_URL}workingtime/GetWorkingTime/${payload}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load specific working time!");
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

  async addWorkingTimes({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const wtData = payload.employees.filter(e => e.didWork).map(e => {
      return {
        EmployeeId: e.id,
        Date: e.date.split('.').reverse().join('/'),
        StartTime: e.startTime,
        EndTime: e.endTime,
      };
    });
    console.log(wtData);

    const request = await fetch(`${API_URL}workingtime/createmultiple/`,
    {
      method: "POST",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(wtData)
    });
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    let data = await res.json();
    if (!res.ok) {
      data = data.replace("That", "Some of those").replace("employee", "employees").replace("has", "have");
      const error = new Error(data || "Failed to add working time!");
      throw error;
    }
  },

  async addWorkingTime({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const wtData = {
      EmployeeId: payload.wtEmployeeId,
      Date: payload.wtDate.split(".").reverse().join("/"),
      StartTime: payload.wtStartTime,
      EndTime: payload.wtEndTime,
    };

    const request = await fetch(
      `${API_URL}workingtime/create/`,
      {
        method: "POST",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(wtData),
      }
    );
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to add working time!");
      throw error;
    }
  },

  async deleteWorkingTime({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const request = await fetch(
      `${API_URL}workingtime/deleteWorkingTime/${payload.id}`,
      {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    );
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to delete working time!");
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

    const request = await fetch(
      `${API_URL}workingtime/editWorkingTime`,
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
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to edit working time!");
      throw error;
    }
  },
};
