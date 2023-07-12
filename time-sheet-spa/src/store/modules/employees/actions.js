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
      const error = new Error(data || "Failed to load employee data!");
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
    console.log(payload);
    const res = await fetch(
      "https://localhost:7059/api/employee/" +
        payload.comid +
        "?PageNumber=" +
        payload.pageNumber +
        "&PageSize=" +
        payload.pageSize,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${rootGetters["auth/token"].token}`,
        },
      }
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to load employees from company!");
      throw error;
    }
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
        payload.depid +
        "?PageNumber=" +
        payload.pageNumber +
        "&PageSize=" +
        payload.pageSize,
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
        data || "Failed to load employees from department!"
      );
      throw error;
    }
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

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to add an employee!");
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

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to edit employee!");
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

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data || "Failed to delete employee!");
      throw error;
    }
  },
};
