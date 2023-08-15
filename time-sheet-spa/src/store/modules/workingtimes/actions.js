import { AjaxCall } from "@/helpers";

export default {
	async loadWorkingTimes({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const link = payload.link;
			const data = await AjaxCall(
				`${link}${payload.id}?PageNumber=${payload.pageNumber}&PageSize=${payload.pageSize}`,
				"GET",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
			const workingTimes = [];
			for (const key in data.toReturn) {
				const wt = {
					ID: data.toReturn[key].id,
					Date: data.toReturn[key].date,
					StartTime: data.toReturn[key].startTime,
					EndTime: data.toReturn[key].endTime,
					EmployeeID: data.toReturn[key].employeeID,
					Employee: data.toReturn[key].employee,
				};
				workingTimes.push(wt);
			}
			commit("setWorkingTimes", {
				wts: workingTimes,
				wtcount: data.count,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load working times!");
			throw errorFromAjax;
		}
	},

	async loadWorkingTime({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(
				`workingtime/GetWorkingTime/${payload}`,
				"GET",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
			const wt = {
				ID: data.id,
				Employee: data.employee,
				WtDate: data.date,
				StartTime: data.startTime,
				EndTime: data.endTime,
			};
			commit("setWorkingTime", wt);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load working time data!");
			throw errorFromAjax;
		}
	},

	async addWorkingTimes({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const wtData = payload.employees
				.filter((e) => e.didWork)
				.map((e) => {
					return {
						EmployeeId: e.id,
						Date: e.date.split(".").reverse().join("/"),
						StartTime: e.startTime,
						EndTime: e.endTime,
					};
				});
			await AjaxCall(`workingtime/createmultiple/`, "POST", wtData, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			let errorFromAjax = error
				.replace("That", "Some of those")
				.replace("employee", "employees")
				.replace("has", "have");
			errorFromAjax = new Error(error || "Failed to add multiple working times!");
			throw errorFromAjax;
		}
	},

	async addWorkingTime({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const wtData = {
				EmployeeId: payload.wtEmployeeId,
				Date: payload.wtDate.split(".").reverse().join("/"),
				StartTime: payload.wtStartTime,
				EndTime: payload.wtEndTime,
			};
			await AjaxCall(`workingtime/create`, "POST", wtData, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to add workingtime!");
			throw errorFromAjax;
		}
	},

	async deleteWorkingTime({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall(
				`workingtime/deleteWorkingTime/${payload.id}`,
				"DELETE",
				null,
				{
					Authorization: `Bearer ${rootGetters["auth/token"].token}`,
				}
			);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to delete working time!");
			throw errorFromAjax;
		}
	},

	async editWorkingTime({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const wt = {
				ID: payload.wtId,
				Date: payload.wtDate.split(".").reverse().join("/"),
				StartTime: payload.wtStartTime,
				EndTime: payload.wtEndTime,
				EmployeeId: payload.wtEmployeeId,
			};
			await AjaxCall(`workingtime/editWorkingTime`, "PUT", wt, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to edit working time data!");
			throw errorFromAjax;
		}
	},
};
