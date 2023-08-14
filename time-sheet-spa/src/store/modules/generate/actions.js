import { API_URL } from "@/config";
import { TIMEOUT_SECONDS } from "@/config";
import { timeout } from "@/helpers";

export default {
	async generateData({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		const request = await fetch(
			`${API_URL}randomgenerate/${payload.comid}/${payload.numberOfEmployees}/${payload.numberOfDays}`,
			{
				method: "POST",
				headers: {
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				},
			}
		);
		const res = await Promise.race([request, timeout(TIMEOUT_SECONDS)]);
		const data = await res.json();
		if (!res.ok) {
			const error = new Error(data || "Failed to generate data!");
			throw error;
		}
	},
};
