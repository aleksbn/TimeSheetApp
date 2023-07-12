<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new department</h2>
    </section>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <section>
      <department-form
        @save-data="saveData"
        :Mode="this.EditMode"
      ></department-form>
    </section>
  </div>
</template>

<script>
import DepartmentForm from "../../components/departments/DepartmentForm.vue";
export default {
  components: {
    DepartmentForm,
  },
  data() {
    return {
      error: null,
      isLoading: false,
      EditMode: "new",
    };
  },
  methods: {
    async saveData(data) {
      this.isLoading = true;
      try {
        await this.$store.dispatch("departments/addDepartment", data);
        this.$router.push("/departments/" + localStorage.getItem('comid'));
      } catch(error) {
        this.error = error;
      }
      this.isLoading = false;
    },
  },
};
</script>
