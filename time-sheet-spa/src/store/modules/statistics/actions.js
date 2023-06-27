export default {
  async loadStatistics(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/calculation/" +
        payload.comid +
        "?year=" +
        payload.year +
        "&month=" +
        payload.month
    );

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }

    const statistics = [];
    for (const key in data) {
      const stat = {
        ID: data[key].id,
        FirstName: data[key].firstName,
        LastName: data[key].lastName,
        Department: data[key].department,
        HourlyRate: data[key].hourlyRate,
        WorkingDays: data[key].workingDays,
        RegularWorkingHours: data[key].regularWorkingHours,
        OvertimeHours: data[key].overtimeHours,
        Earnings: data[key].earnings,
      };
      statistics.push(stat);
    }

    context.commit("setStatistics", statistics);
  },
};
