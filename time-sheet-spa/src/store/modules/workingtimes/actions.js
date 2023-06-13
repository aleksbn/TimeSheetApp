export default {
  async loadWorkingTimes(context, payload) {
    const link = payload.link;
    const res = await fetch(
      link + payload.id
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }
    const workingTimes = [];
    for (const key in data) {
      const wt = {
        ID: data[key].id,
        Date: data[key].date,
        StartTime: data[key].startTime,
        EndTime: data[key].endTime,
        EmployeeID: data[key].employeeID,
        Employee: data[key].employee
      };
      workingTimes.push(wt);
    }
    
    context.commit("setWorkingTimes", workingTimes);
  },
};
