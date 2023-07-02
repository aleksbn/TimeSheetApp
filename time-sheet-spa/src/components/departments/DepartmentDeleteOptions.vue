<template #default>
  <div>
    <p style="text-align: justify">
      Would you like to delete all of the employees from this department? If you
      choose YES, all of the employees and their working times will be deleted.
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
      Select destination department you would like to transfer the employees to.
    </p>
    <div class="form-control">
      <label for="companyId">Select department:</label>
      <select
        name="departmentId"
        v-model="depId"
        id="departmentId"
        v-if="loadedDepartments.length > 0"
      >
        <option
          v-for="department in departments"
          :key="department.ID"
          :value="department.ID"
        >
          {{ department.Name }}
        </option>
      </select>
      <p v-else>
        There are no departments to choose from. Either add some departments to
        the company, or delete all the employees.
      </p>
    </div>
  </div>
  <div class="form-control">
    <base-button style="display: inline" @click="deleteDepartment"
      >Delete</base-button
    >
    <base-button style="display: inline" @click="cancel">Cancel</base-button>
  </div>
</template>

<script>
import { ref } from "vue";

export default {
  emits: ["cancel"],
  data() {
    return {
      delSelected: ref("1"),
      depId: ref(""),
      loadedDepartments: [],
    };
  },
  computed: {
    delSel() {
      return this.delSelected;
    },
    departments() {
      return this.loadedDepartments;
    },
  },
  watch: {
    async delSelected() {
      if (this.delSelected == 0) {
        await this.loadDepartments();
      }
    },
  },
  methods: {
    async deleteDepartment() {
      const deleteData = {
        depId: parseInt(localStorage.getItem("depid")),
        targetDepId: this.depId > 0 ? this.depId : 0,
        deleteEmployees: this.delSelected == 0 ? false : true,
      };
      try {
        await this.$store.dispatch("departments/deleteDepartment", deleteData);
        this.$router.push("/departments/" + localStorage.getItem("comid"));
      } catch (error) {
        this.error =
          error.message + " in deleting department." || "Something went wrong!";
      }
    },
    cancel() {
      this.$emit("cancel");
    },
    async loadDepartments() {
      try {
        await this.$store.dispatch("departments/loadDepartments", {
          comid: parseInt(localStorage.getItem("comid")),
        });
      } catch (error) {
        this.error =
          error.message + " in getting departments" || "Something went wrong!";
      }
      this.loadedDepartments = this.$store.getters[
        "departments/departments"
      ].filter((d) => d.ID != parseInt(localStorage.getItem("depid")));
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
