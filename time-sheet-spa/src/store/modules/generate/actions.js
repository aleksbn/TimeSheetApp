export default {
  async generateData({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      rootGetters["getSiteLink"] +
        `randomgenerate/${payload.comid}/${payload.numberOfEmployees}/${payload.numberOfDays}`,
      {
        method: "POST",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to generate data!");
      throw error;
    }
  },
};
