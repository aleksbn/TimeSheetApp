export default {
  async loadWorkingTimes(context, payload) {
    const link = payload.link;
    const res = await fetch(
      link +
        payload.id +
        "?PageNumber=" +
        payload.pageNumber +
        "&PageSize=" +
        payload.pageSize
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
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
    
    context.commit("setWorkingTimes", {
      wts: workingTimes,
      wtcount: data.count,
    });
  },

  async loadWorkingTime(context, payload) {
    const res = await fetch(`https://localhost:7059/api/workingtime/GetWorkingTime/${payload}`);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }
    const wt = {
      ID: data.id,
      Employee: data.employee,
      WtDate: data.date,
      StartTime: data.startTime,
      EndTime: data.endTime
    };
    context.commit("setWorkingTime", wt);
  },

  async addWorkingTime(_1, payload) {
    const wtData = {
      EmployeeId: payload.wtEmployeeId,
      Date: payload.wtDate.split('.').reverse().join('/'),
      StartTime: payload.wtStartTime,
      EndTime: payload.wtEndTime
    }
    console.log(payload);
    const res = await fetch(`https://localhost:7059/api/workingtime/create/`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(wtData)
    })
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to add data!");
      throw error;
    }
  },

  async deleteWorkingTime(_1, payload) {
    const res = await fetch(`https://localhost:7059/api/workingtime/deleteWorkingTime/${payload.id}`, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to delete data!");
      throw error;
    }
  },

  async editWorkingTime(_1, payload) {
    const wt = {
      ID: payload.wtId,
      Date: payload.wtDate.split('.').reverse().join('/'),
      StartTime: payload.wtStartTime,
      EndTime: payload.wtEndTime,
      EmployeeId: payload.wtEmployeeId
    };

    const res = await fetch("https://localhost:7059/api/workingtime/editWorkingTime", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(wt)
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to edit data!");
      throw error;
    }
  }
};
