export default {
  async loadEmployee({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      "https://localhost:7059/api/employee/" + payload.empid,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load employee data!");
      throw error;
    }

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
  },

  async loadEmployeesFromCompany({ commit, dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      "https://localhost:7059/api/employee/" + payload.comid,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(
        data.message || "Failed to load employees from company!"
      );
      throw error;
    }
    const employees = [];
    for (const key in data) {
      const employee = {
        ID: data[key].id,
        FirstName: data[key].firstName,
        LastName: data[key].lastName,
        JobTitle: data[key].jobTitle,
        Degree: data[key].degree,
        Address: data[key].address,
        Email: data[key].email,
        Phone: data[key].phone,
        DateOfBirth: data[key].dateOfBirth,
        StartOfEmployment: data[key].startOfEmployment,
        HourlyRate: data[key].hourlyRate,
        DepartmentId: data[key].departmentId,
        Department: data[key].department,
      };
      employees.push(employee);
    }
    commit("setEmployees", employees);
  },

  async loadEmployeesFromDepartment(
    { commit, dispatch, rootGetters },
    payload
  ) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      "https://localhost:7059/api/employee/" +
        payload.comid +
        "/" +
        payload.depid,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(
        data.message || "Failed to load employees from department!"
      );
      throw error;
    }

    const employees = [];
    for (const key in data) {
      const employee = {
        ID: data[key].id,
        FirstName: data[key].firstName,
        LastName: data[key].lastName,
        JobTitle: data[key].jobTitle,
        Degree: data[key].degree,
        Address: data[key].address,
        Phone: data[key].phone,
        DateOfBirth: data[key].dateOfBirth,
        StartOfEmployment: data[key].startOfEmployment,
        HourlyRate: data[key].hourlyRate,
        DepartmentId: data[key].departmentId,
        Department: data[key].department,
      };
      employees.push(employee);
    }

    commit("setEmployees", employees);
  },

  async addEmployee({ dispatch, rootGetters }, payload) {
    const emp = {
      FirstName: payload.empFirstName,
      LastName: payload.empLastName,
      Address: payload.empAddress,
      Email: payload.empEmail,
      Phone: payload.empPhone,
      JobTitle: payload.empJobTitle,
      Degree: payload.empDegree,
      DateOfBirth: payload.empDateOfBirth,
      StartOfEmployment: payload.empStartOfEmployment,
      HourlyRate: payload.empHourlyRate,
      DepartmentId: payload.empDepartmentId,
    };

    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch("https://localhost:7059/api/employee/", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emp),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to add an employee!");
      throw error;
    }
  },

  async editEmployee({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
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
      DateOfBirth: payload.empDateOfBirth,
      StartOfEmployment: payload.empStartOfEmployment,
      HourlyRate: payload.empHourlyRate,
    };

    const res = await fetch("https://localhost:7059/api/employee", {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emp),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to edit employee!");
      throw error;
    }
  },

  async deleteEmployee({ dispatch, rootGetters }, payload) {
    await dispatch("auth/checkTokens", null, { root: true });
    const res = await fetch(
      `https://localhost:7059/api/employee/${payload.empId}`,
      {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to delete employee!");
      throw error;
    }
  },
};
