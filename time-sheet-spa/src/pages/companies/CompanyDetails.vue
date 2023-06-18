<template>
  <div>
    <company-form
      v-if="hasCompany"
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
    <h3 v-else>There's some problems with loading of this company.</h3>
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
      mode: true,
      EditMode: "old",
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("companies/editCompany", data);
      this.$router.push("/companies");
    },
    async loadCompany() {
      try {
        await this.$store.dispatch("companies/loadCompany", { id: this.comid });
      } catch (error) {
        this.error =
          error.message + " in getting company." || "Something went wrong!";
      }
    },
  },
  created() {
    this.loadCompany();
    localStorage.setItem("comid", this.comid);
    localStorage.setItem("comidwt", this.comid);
  },
  computed: {
    hasCompany() {
      return this.$store.getters["companies/hasCompany"];
    },
    company() {
      return this.$store.getters["companies/company"];
    },
  },
};
</script>
