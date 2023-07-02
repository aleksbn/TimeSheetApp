export default {
  async loadStatistics({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      "https://localhost:7059/api/calculation/" +
        payload.comid +
        "?year=" +
        payload.year +
        "&month=" +
        payload.month,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load statistic data!");
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

    commit("setStatistics", statistics);
  },
};
