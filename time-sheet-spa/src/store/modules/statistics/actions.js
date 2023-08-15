import { AjaxCall } from "@/helpers";

export default {
	async loadStatistics({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(
				`calculation/${payload.comid}?year=${payload.year}&month=${payload.month}`,
				"GET",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
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
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load statistics from company!");
			throw errorFromAjax;
		}
	},
};
