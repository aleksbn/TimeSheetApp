import { AjaxCall } from "@/helpers";

export default {
	async generateData({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall(
				`randomgenerate/${payload.comid}/${payload.numberOfEmployees}/${payload.numberOfDays}`,
				"POST",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to generate random data!");
			throw errorFromAjax;
		}
	},
};
