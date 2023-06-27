<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new employee</h2>
    </section>
    <section>
      <employee-form
        @save-data="saveData"
        :Mode="this.EditMode"
      ></employee-form>
    </section>
  </div>
</template>

<script>
import EmployeeForm from "../../components/employees/EmployeeForm.vue";
export default {
  components: {
    EmployeeForm,
  },
  data() {
    return {
      EditMode: "new",
    };
  },
  methods: {
    async saveData(data) {
      await this.$store.dispatch("employees/addEmployee", data);
      this.$router.push("/employees/" + localStorage.getItem("comid"));
    },
  },
};
</script>
