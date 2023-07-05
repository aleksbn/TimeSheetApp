export default {
  async generateData({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `https://localhost:7059/api/randomgenerate/${payload.comid}/${payload.numberOfEmployees}/${payload.numberOfDays}`,
      {
        method: "POST",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to generate data!");
      throw error;
    }
  },
};
