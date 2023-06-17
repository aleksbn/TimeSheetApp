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
};

//akcija za preracunavanje koliko WTs ima
