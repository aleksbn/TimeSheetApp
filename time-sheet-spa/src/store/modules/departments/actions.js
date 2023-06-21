export default {
  async loadDepartments(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/department/" + payload.comid
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }

    const departments = [];
    for (const key in data) {
      const dep = {
        ID: data[key].id,
        Name: data[key].name,
      };
      departments.push(dep);
    }

    context.commit("setDepartments", departments);
  },
  async loadDepartment(context, payload) {
    const res = await fetch(
      "https://localhost:7059/api/department/" +
        payload.comid +
        "/" +
        payload.depid
    );
    const data = await res.json();
    if (!res.ok) {
      const error = new Error(data.message || "Failed to load data!");
      throw error;
    }

    var department = {
      ID: data.id,
      Name: data.name,
    };

    context.commit("setDepartment", department);
  },

  async addDepartment(_1, payload) {
    const dep = {
      Name: payload.depName,
      CompanyID: payload.comId
    };

    const res = await fetch("https://localhost:7059/api/department", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dep),
    });

    if (!res.ok) {
      const error = new Error(res.message || "Failed to post data!");
      throw error;
    }
  },

  async editDepartment(_1, payload) {
    const dep = {
        ID: payload.depId,
        Name: payload.depName,
        CompanyID: payload.comId
    };

    const res = await fetch("https://localhost:7059/api/department", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dep),
    });

    if (!res.ok) {
        const error = new Error(res.message || "Failed to post data!");
        throw error;
      }
  }
};
