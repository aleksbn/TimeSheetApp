export default {
  async loadEmployee(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/employee/" + payload.empid
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
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
    context.commit("setEmployee", employee);
  },
  async loadEmployeesFromCompany(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/employee/" + payload.comid
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
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
    context.commit("setEmployees", employees);
  },
  async loadEmployeesFromDepartment(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/employee/" +
        payload.comid +
        "/" +
        payload.depid
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
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

    context.commit("setEmployees", employees);
  },
  async addEmployee(_1, payload) {
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

    const res = await fetch("https://localhost:7059/api/employee/", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emp),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to post data!");
      throw error;
    }
  },
  async editEmployee(_1, payload) {
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
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emp),
    });

    if (!res.ok) {
      const error = new Error(res.message);
      throw error;
    }
  },

  async deleteEmployee(_1, payload) {
    const res = await fetch(`https://localhost:7059/api/employee/${payload.empId}`, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to load data!");
      throw error;
    }
  },
};
