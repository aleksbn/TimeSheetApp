import { AjaxCall } from "@/helpers";

export default {
	async loadDepartments({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(`department/${payload.comid}`, "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
			const departments = [];
			for (const key in data) {
				const dep = {
					ID: data[key].id,
					Name: data[key].name,
				};
				departments.push(dep);
				commit("setDepartments", departments);
			}
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load departments!");
			throw errorFromAjax;
		}
	},
	async loadDepartment({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(
				`department/${payload.comid}/${payload.depid}`,
				"GET",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
			const department = {
				ID: data.id,
				Name: data.name,
			};
			commit("setDepartment", department);
		} catch (error) {
			const errorFromAjax = new Error(
				error || "Failed to load department data!"
			);
			throw errorFromAjax;
		}
	},

	async addDepartment({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const dep = {
				Name: payload.depName,
				CompanyID: payload.comId,
			};
			await AjaxCall("department", "POST", dep, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to add department!");
			throw errorFromAjax;
		}
	},

	async editDepartment({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const dep = {
				ID: payload.depId,
				Name: payload.depName,
				CompanyID: payload.comId,
			};
			await AjaxCall("department", "PUT", dep, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(
				error || "Failed to edit department data!"
			);
			throw errorFromAjax;
		}
	},

	async deleteDepartment({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall(
				`department/${payload.depId}?deleteEmployees=${payload.deleteEmployees}&&targetDepId=${payload.targetDepId}`,
				"DELETE",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to delete department!");
			throw errorFromAjax;
		}
	},
};
