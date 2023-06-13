export default {
  async loadCompanies(context) {
    const res = await fetch("https://localhost:7059/api/company");
    
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }

    const companies = [];
    for(const key in data) {
        const com = {
            ID: data[key].id,
            Name: data[key].name,
            Address: data[key].address,
            City: data[key].city,
            Country: data[key].country,
            Email: data[key].email,
            CompanyManagerId: data[key].companyManagerId
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
        CompanyManagerId: data.companyManagerId
    };
    context.commit("setCompany", company)
  }
};
