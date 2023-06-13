export default {
  async loadEmployee(context, payload) {
    const res = await fetch("https://localhost:7059/api/employee/" + payload.empid);
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
      Address: data.address,
      Phone: data.phone,
      DateOfBirth: data.dateOfBirth,
      StartOfEployment: data.startOfEmployment,
      HourlyRate: data.hourlyRate,
      DepartmentId: data.departmentId,
    };
    context.commit("setEmployee", employee);
  },
  async loadEmployeesFromCompany(context, payload) {
    const res = await fetch("https://localhost:7059/api/employee/" + payload.comid);
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
        StartOfEployment: data[key].startOfEmployment,
        HourlyRate: data[key].hourlyRate,
        DepartmentId: data[key].departmentId,
      };
      employees.push(employee);
    }

    context.commit("setEmployees", employees);
  },
  async loadEmployeesFromDepartment(context, payload) {
    const res = await fetch("https://localhost:7059/api/employee/" + payload.comid + "/" + payload.depid);
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
        StartOfEployment: data[key].startOfEmployment,
        HourlyRate: data[key].hourlyRate,
        DepartmentId: data[key].departmentId,
      };
      employees.push(employee);
    }

    context.commit("setEmployees", employees);
  },
};
