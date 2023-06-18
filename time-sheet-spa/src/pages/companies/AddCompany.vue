<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new company</h2>
    </section>
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
      EditMode: "new",
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("companies/addCompany", data);
      this.$router.push("/companies");
    },
  },
};
</script>
