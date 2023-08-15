import { AjaxCall } from "@/helpers";

export default {
	async loadCompanies({ commit, dispatch, rootGetters }) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall("company", "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
			const companies = [];
			for (const key in data) {
				const com = {
					ID: data[key].id,
					Name: data[key].name,
					Address: data[key].address,
					City: data[key].city,
					Country: data[key].country,
					Email: data[key].email,
					CompanyManagerId: data[key].companyManagerId,
					StartTime: data[key].startTime,
					EndTime: data[key].endTime,
				};
				companies.push(com);
				commit("setCompanies", companies);
			}
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load companies!");
			throw errorFromAjax;
		}
	},

	async loadCompany({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(`company/${payload.id}`, "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
			const company = {
				ID: data.id,
				Name: data.name,
				Address: data.address,
				City: data.city,
				Country: data.country,
				Email: data.email,
				CompanyManagerId: data.companyManagerId,
				StartTime: data.startTime,
				EndTime: data.endTime,
			};
			commit("setCompany", company);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load companies!");
			throw errorFromAjax;
		}
	},

	async addCompany({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		const com = {
			Name: payload.comName,
			Address: payload.comAddress,
			City: payload.comCity,
			Country: payload.comCountry,
			Email: payload.comEmail,
			StartTime: payload.comStartTime,
			EndTime: payload.comEndTime,
		};
		try {
			await AjaxCall("company", "POST", com, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to add company!");
			throw errorFromAjax;
		}
	},

	async editCompany({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		const com = {
			ID: payload.comId,
			Name: payload.comName,
			Address: payload.comAddress,
			City: payload.comCity,
			Country: payload.comCountry,
			Email: payload.comEmail,
			CompanyManagerId: localStorage.getItem("userId"),
			StartTime: payload.comStartTime,
			EndTime: payload.comEndTime,
		};
		try {
			await AjaxCall("company", "PUT", com, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to edit company!");
			throw errorFromAjax;
		}
	},

	async deleteCompany({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall(
				`company/${payload.id}?targetDepartmentId=${payload.targetDepartmentId}&&deleteEmployees=${payload.deleteEmployees}`,
				"DELETE",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to delete company!");
			throw errorFromAjax;
		}
	},
};
