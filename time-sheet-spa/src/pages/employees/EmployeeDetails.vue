<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <employee-form
      @save-data="saveData"
      v-else-if="hasEmployee"
      :key="employee.ID"
      :ID="employee.ID"
      :FirstName="employee.FirstName"
      :LastName="employee.LastName"
      :Degree="employee.Degree"
      :JobTitle="employee.JobTitle"
      :HourlyRate="employee.HourlyRate"
      :Department="employee.Department"
      :DepartmentId="employee.DepartmentId"
      :Address="employee.Address"
      :Phone="employee.Phone"
      :Email="employee.Email"
      :DateOfBirth="employee.DateOfBirth"
      :StartOfEmployment="employee.StartOfEmployment"
      :Mode="this.EditMode"
    ></employee-form>
    <h3 v-else>There's some problems with loading of this employee.</h3>
  </div>
</template>

<script>
import EmployeeForm from "../../components/employees/EmployeeForm.vue";

export default {
  components: {
    EmployeeForm,
  },
  props: ["empid"],
  data() {
    return {
      isLoading: false,
      mode: true,
      EditMode: "old",
      error: null
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("employees/editEmployee", data);
      this.$router.push("/employees/" + localStorage.getItem("comid"));
    },
    async loadEmployee() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("employees/loadEmployee", {
          empid: this.empid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting employee." || "Something went wrong!";
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    }
  },
  created() {
    this.loadEmployee();
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
