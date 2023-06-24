export default {
  async loadCompanies(context) {
    const res = await fetch("https://localhost:7059/api/company");

    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }

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
      };
      companies.push(com);
    }
    context.commit("setCompanies", companies);
  },

  async loadCompany(context, payload) {
    const res = await fetch("https://localhost:7059/api/company/" + payload.id);
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }
    var company = {
      ID: data.id,
      Name: data.name,
      Address: data.address,
      City: data.city,
      Country: data.country,
      Email: data.email,
      CompanyManagerId: data.companyManagerId,
    };
    context.commit("setCompany", company);
  },

  async addCompany(_1, payload) {
    const com = {
      Name: payload.comName,
      Address: payload.comAddress,
      City: payload.comCity,
      Country: payload.comCountry,
      Email: payload.comEmail,
    };

    const res = await fetch("https://localhost:7059/api/company", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(com),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to post data!");
      throw error;
    }
  },

  async editCompany(_1, payload) {
    const com = {
      ID: payload.comId,
      Name: payload.comName,
      Address: payload.comAddress,
      City: payload.comCity,
      Country: payload.comCountry,
      Email: payload.comEmail,
    };

    const res = await fetch("https://localhost:7059/api/company", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(com),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to load data!");
      throw error;
    }
  },

  async deleteCompany(_1, payload) {
    const res = await fetch(
      `https://localhost:7059/api/company/${payload.id}?targetDepartmentId=${payload.targetDepartmentId}&&deleteEmployees=${payload.deleteEmployees}`,
      {
        method: "DELETE",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    );

    if (!res.ok) {
      const error = new Error(res.message || "Failed to load data!");
      throw error;
    }
  },
};
