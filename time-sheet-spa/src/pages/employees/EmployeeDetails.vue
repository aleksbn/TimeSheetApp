<template>
  <div>
    <employee-form v-if="hasEmployee"
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
      :DateOfBirth="employee.DateOfBirth"
      :StartOfEmployment="employee.StartOfEmployment"
      :Mode="this.mode"
    ></employee-form>
  </div>
</template>

<script>
import EmployeeForm from "../../components/employees/EmployeeForm.vue";

export default {
  props: ["empid"],
  data() {
    return {
      mode: "old"
    }
  },
  components: {
    EmployeeForm,
  },
  created() {
    this.loadEmployee();
    localStorage.setItem("empidwt", this.empid);
  },
  methods: {
    stop() {},
    toggleMode() {
      this.mode = !this.mode;
    },
    async loadEmployee() {
      try {
        await this.$store.dispatch("employees/loadEmployee", {
          empid: this.empid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting employee." || "Something went wrong!";
      }
    },
  },
  computed: {
    hasEmployee() {
      return this.$store.getters["employees/hasEmployee"];
    },
    employee() {
      return this.$store.getters["employees/employee"];
    },
  },
};
</script>
