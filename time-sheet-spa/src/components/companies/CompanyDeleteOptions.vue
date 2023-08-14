<template #default>
  <div>
    <p style="text-align: justify">
      Would you like to delete all of the employees from this company? If
      you choose YES, all of the employees and their working times will be
      deleted.
    </p>
    <div class="form-control">
      <label for="deleteAll">Delete all employees:</label>
      <select name="deleteAll" id="deleteAll" v-model="delSelected">
        <option value="0" selected>NO</option>
        <option value="1">YES</option>
      </select>
    </div>
  </div>
  <div v-if="!(delSel > 0)">
    <p style="text-align: justify">
      Select the company you would like to transfer employees of deleting
      company.
    </p>
    <div class="form-control">
      <label for="companyId">Select company:</label>
      <select name="companyId" v-model="comId" id="companyId">
        <option value="0">None. Just leave them unemployed.</option>
        <option v-for="company in companies" :key="company.ID" :value="company.ID">
          {{ company.Name }}
        </option>
      </select>
    </div>
  </div>
  <div v-if="comSel > 0">
    <p style="text-align: justify">
      Select the department of the company you would like to transfer
      employees of deleting company.
    </p>
    <div class="form-control">
      <label for="companyId">Select department:</label>
      <select name="departmentId" v-model="depId" id="departmentId" v-if="loadedDepartments.length > 0">
        <option v-for="department in departments" :key="department.ID" :value="department.ID">
          {{ department.Name }}
        </option>
      </select>
      <p v-else>
        There are no departments to choose from. Either add some
        departments to the selected company, or delete all the employees.
      </p>
    </div>
  </div>
  <div class="form-control">
    <base-button style="display: inline" @click="deleteCompany">Delete</base-button>
    <base-button style="display: inline" @click="cancel">Cancel</base-button>
  </div>
</template>



<script>
import { ref } from "vue";

export default {
  emits: ["cancel"],
  data() {
    return {
      comId: ref("0"),
      delSelected: ref("1"),
      loadedCompanies: [],
      depId: ref(""),
      loadedDepartments: [],
    };
  },
  computed: {
    delSel() {
      return this.delSelected;
    },
    comSel() {
      return this.comId;
    },
    companies() {
      return this.loadedCompanies;
    },
    departments() {
      return this.loadedDepartments;
    },
  },
  watch: {
    async delSelected() {
      if (this.delSelected == 0) {
        await this.loadCompanies();
      }
    },
    async comId() {
      if (this.comId != 0) {
        await this.loadDepartments();
      }
    },
  },
  methods: {
    async deleteCompany() {
      const deleteData = {
        id: parseInt(localStorage.getItem("comid")),
        targetDepartmentId: this.depId != '' ? this.depId : 0,
        deleteEmployees: this.delSelected == 0 ? false : true,
      };
      try {
        await this.$store.dispatch("companies/deleteCompany", deleteData);
        this.$router.push("/companies");
      } catch (error) {
        this.error =
          `${error.message} in deleting company.` || "Something went wrong!";
      }
    },
    cancel() {
      this.$emit("cancel");
    },
    async loadCompanies() {
      try {
        if (!this.$store.getters["companies/hasCompanies"]) {
          await this.$store.dispatch("companies/loadCompanies");
        }
      } catch (error) {
        this.error =
          `${error.message} in getting companies.` || "Something went wrong!";
      }
      this.loadedCompanies = this.$store.getters["companies/companies"].filter(
        (c) => +c.ID !== parseInt(localStorage.getItem("comid"))
      );
    },
    async loadDepartments() {
      try {
        await this.$store.dispatch("departments/loadDepartments", {
          comid: this.comId,
        });
      } catch (error) {
        this.error =
          `${error.message} in getting epartments.` || "Something went wrong!";
      }
      this.loadedDepartments = this.$store.getters["departments/departments"];
    },
  },
};
</script>

<style scoped>
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input,
select {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
  font-size: 1em;
  border-radius: 15px;
  height: 40px;
}

input:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}
</style>
