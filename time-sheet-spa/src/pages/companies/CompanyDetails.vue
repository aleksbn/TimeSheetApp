<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <company-form
      v-else-if="hasCompany"
      @save-data="saveData"
      :key="company.ID"
      :ID="company.ID"
      :Name="company.Name"
      :Address="company.Address"
      :City="company.City"
      :Country="company.Country"
      :Email="company.Email"
      :Mode="this.EditMode"
    ></company-form>
    <base-card v-else>
      <h3>There are some problems with loading of this company.</h3>
    </base-card>
  </div>
</template>

<script>
import CompanyForm from "../../components/companies/CompanyForm.vue";

export default {
  components: {
    CompanyForm,
  },
  props: ["comid"],
  data() {
    return {
      isLoading: false,
      error: null,
      mode: true,
      EditMode: "old",
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("companies/editCompany", data);
      this.$router.push("/companies");
    },
    handleError() {
      this.error = null;
    },
    async loadCompany() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("companies/loadCompany", { id: this.comid });
      } catch (error) {
        this.error =
          error.message + " in getting company." || "Something went wrong!";
      }
      this.isLoading = false;
    },
  },
  created() {
    this.loadCompany();
    localStorage.setItem("comid", this.comid);
    localStorage.setItem("comidwt", this.comid);
  },
  computed: {
    hasCompany() {
      return !this.isLoading && this.$store.getters["companies/hasCompany"];
    },
    company() {
      return this.$store.getters["companies/company"];
    },
  },
};
</script>
