<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new company</h2>
    </section>
    <base-dialog :show="!!error" title="An error occured" :showClose="true" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <section>
      <company-form @save-data="saveData" :Mode="this.EditMode"></company-form>
    </section>
  </div>
</template>

<script>
import CompanyForm from "../../components/companies/CompanyForm.vue";
export default {
  components: {
    CompanyForm,
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
      this.isLoading = true;
      try {
        await this.$store.dispatch("companies/addCompany", data);
        this.$router.push("/companies");
      } catch (error) {
        this.error = error;
      }
      this.isLoading = false;
    }
  },
};
</script>
