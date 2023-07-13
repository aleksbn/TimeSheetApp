<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <company-form v-if="hasCompany" @save-data="saveData" @delete-company="deleteCompany" :key="company.ID"
      :ID="company.ID" :Name="company.Name" :Address="company.Address" :City="company.City" :Country="company.Country"
      :Email="company.Email" :Mode="this.EditMode" :StartTime="company.StartTime"
      :EndTime="company.EndTime"></company-form>
    <base-card v-else-if="!isLoading">
      <h3>There are some problems with loading of this company.</h3>
    </base-card>
    <base-dialog :showClose="false" fixed title="Select some delete options" :show="deleting">
      <company-delete-options @cancel="cancelDeletion"></company-delete-options>
    </base-dialog>
  </div>
</template>

<script>
import CompanyForm from "../../components/companies/CompanyForm.vue";
import CompanyDeleteOptions from "@/components/companies/CompanyDeleteOptions.vue";

export default {
  components: {
    CompanyForm,
    CompanyDeleteOptions,
  },
  props: ["comid"],
  data() {
    return {
      isLoading: false,
      deleting: false,
      error: null,
      mode: true,
      EditMode: "old",
    };
  },
  methods: {
    async saveData(data) {
      this.isLoading = true;
      try {
        await this.$store.dispatch("companies/editCompany", data);
        this.$router.push("/companies");
      } catch (error) {
        this.error = error;
      }
      this.isLoading = false;
    },
    cancelDeletion() {
      this.deleting = false;
    },
    deleteCompany() {
      this.deleting = true;
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
  async created() {
    await this.loadCompany();
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
    deletingDialog() {
      return this.deleting;
    },
  },
};
</script>
