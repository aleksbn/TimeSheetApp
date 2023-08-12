<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <employee-form @save-data="saveData" @delete-employee="deleteEmployee" v-if="hasEmployee" :key="employee.ID"
      :ID="employee.ID" :FirstName="employee.FirstName" :LastName="employee.LastName" :Degree="employee.Degree"
      :JobTitle="employee.JobTitle" :HourlyRate="employee.HourlyRate" :Department="employee.Department"
      :DepartmentId="employee.DepartmentId" :Address="employee.Address" :Phone="employee.Phone" :Email="employee.Email"
      :DateOfBirth="employee.DateOfBirth" :StartOfEmployment="employee.StartOfEmployment"
      :Mode="this.EditMode"></employee-form>
    <h3 v-else-if="!isLoading">There's some problems with loading of this employee.</h3>
    <base-dialog :showClose="false" fixed title="Select some delete options" :show="deleting">
      <employee-delete-options @cancel="cancelDeletion"></employee-delete-options>
    </base-dialog>
  </div>
</template>

<script>
import EmployeeForm from "../../components/employees/EmployeeForm.vue";
import EmployeeDeleteOptions from "@/components/employees/EmployeeDeleteOptions.vue";

export default {
  components: {
    EmployeeForm,
    EmployeeDeleteOptions,
  },
  props: ["empid"],
  data() {
    return {
      isLoading: false,
      deleting: false,
      mode: true,
      EditMode: "old",
      error: null,
    };
  },
  methods: {
    async saveData(data) {
      this.isLoading = true;
      try {
        await this.$store.dispatch("employees/editEmployee", data);
        this.$router.push("/employees/" + localStorage.getItem("comid"));
      } catch (error) {
        this.error = error;
      }
      this.isLoading = false;
    },
    async loadEmployee() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("employees/loadEmployee", {
          empid: this.empid,
        });
      } catch (error) {
        this.error =
          `${error.message} in getting employee.` || "Something went wrong!";
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    },
    cancelDeletion() {
      this.deleting = false;
    },
    deleteEmployee() {
      this.deleting = true;
    },
  },
  async created() {
    await this.loadEmployee();
    localStorage.setItem("empid", this.empid);
    localStorage.setItem("empidwt", this.empid);
  },
  computed: {
    hasEmployee() {
      return !this.isLoading && this.$store.getters["employees/hasEmployee"];
    },
    employee() {
      return this.$store.getters["employees/employee"];
    },
  },
};
</script>
