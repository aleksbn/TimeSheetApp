import { AjaxCall } from "@/helpers";

export default {
	async loadEmployee({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(`employee/${payload.empid}`, "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
			const employee = {
				ID: data.id,
				FirstName: data.firstName,
				LastName: data.lastName,
				JobTitle: data.jobTitle,
				Degree: data.degree,
				Email: data.email,
				Address: data.address,
				Phone: data.phone,
				DateOfBirth: data.dateOfBirth,
				StartOfEmployment: data.startOfEmployment,
				HourlyRate: data.hourlyRate,
				DepartmentId: data.departmentId,
			};
			commit("setEmployee", employee);
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load employee data!");
			throw errorFromAjax;
		}
	},

	async loadEmployees({ commit, dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const data = await AjaxCall(`employee/${payload.link}`, "GET", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
			const employees = [];
			for (const key in data.toReturn) {
				const employee = {
					ID: data.toReturn[key].id,
					FirstName: data.toReturn[key].firstName,
					LastName: data.toReturn[key].lastName,
					JobTitle: data.toReturn[key].jobTitle,
					Degree: data.toReturn[key].degree,
					Address: data.toReturn[key].address,
					Email: data.toReturn[key].email,
					Phone: data.toReturn[key].phone,
					DateOfBirth: data.toReturn[key].dateOfBirth,
					StartOfEmployment: data.toReturn[key].startOfEmployment,
					HourlyRate: data.toReturn[key].hourlyRate,
					DepartmentId: data.toReturn[key].departmentId,
					Department: data.toReturn[key].department,
				};
				employees.push(employee);
			}

			commit("setEmployees", {
				employees: employees,
				emCount: data.count,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to load employees!");
			throw errorFromAjax;
		}
	},

	async addEmployee({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const emp = {
				FirstName: payload.empFirstName,
				LastName: payload.empLastName,
				Address: payload.empAddress,
				Email: payload.empEmail,
				Phone: payload.empPhone,
				JobTitle: payload.empJobTitle,
				Degree: payload.empDegree,
				DateOfBirth: payload.empDateOfBirth.split(".").reverse().join("/"),
				StartOfEmployment: payload.empStartOfEmployment
					.split(".")
					.reverse()
					.join("/"),
				HourlyRate: payload.empHourlyRate,
				DepartmentId: payload.empDepartmentId,
			};
			await AjaxCall("employee", "POST", emp, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to add employee!");
			throw errorFromAjax;
		}
	},

	async editEmployee({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			const emp = {
				ID: payload.empId,
				DepartmentId: payload.empDepartmentId,
				FirstName: payload.empFirstName,
				LastName: payload.empLastName,
				JobTitle: payload.empJobTitle,
				Degree: payload.empDegree,
				Address: payload.empAddress,
				Email: payload.empEmail,
				Phone: payload.empPhone,
				DateOfBirth: payload.empDateOfBirth.split(".").reverse().join("/"),
				StartOfEmployment: payload.empStartOfEmployment
					.split(".")
					.reverse()
					.join("/"),
				HourlyRate: payload.empHourlyRate,
			};
			await AjaxCall("employee", "PUT", emp, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to edit employee data!");
			throw errorFromAjax;
		}
	},

	async deleteEmployee({ dispatch, rootGetters }, payload) {
		await dispatch("auth/checkTokens", null, { root: true });
		try {
			await AjaxCall(`employee/${payload.empId}`, "DELETE", null, {
				Authorization: `Bearer ${rootGetters["auth/token"].token}`,
			});
		} catch (error) {
			const errorFromAjax = new Error(error || "Failed to delete employee!");
			throw errorFromAjax;
		}
	},
};
