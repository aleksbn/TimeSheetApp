export default {
    async loadDepartments(context, payload) {
        const res = await fetch("https://localhost:7059/api/department/" + payload.comid);        
        const data = await res.json();
        if(!res.ok) {
            const error = new Error(data.message || "Failed to load data!");
            throw error;
        }

        const departments = [];
        for(const key in data) {
            const dep = {
                ID: data[key].id,
                Name: data[key].name,
            };
            departments.push(dep);
        }

        context.commit("setDepartments", departments);
    },
    async loadDepartment(context, payload) {
        const res = await fetch("https://localhost:7059/api/department/" + payload.comid + "/" + payload.depid);
        const data = await res.json();
        if(!res.ok) {
            const error = new Error(data.message || "Failed to load data!");
            throw error;
        }

        var department = {
            ID: data.id,
            Name: data.name
        };

        context.commit("setDepartment", department);
    }
};