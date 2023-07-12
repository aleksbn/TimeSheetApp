<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new employee</h2>
    </section>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <section>
      <employee-form @save-data="saveData" :Mode="this.EditMode"></employee-form>
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
      error: null,
      isLoading: false,
      EditMode: "new",
    };
  },
  methods: {
    handleError() {
      this.error = null;
    },
    async saveData(data) {
      try {
        await this.$store.dispatch("employees/addEmployee", data);
        this.$router.push("/employees/" + localStorage.getItem("comid"));
      } catch (error) {
        this.error = error;
      }
    },
  },
};
</script>
