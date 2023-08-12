<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" :showClose="true" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <base-card>
        <div class="controls">
          <base-button @click="refresh">Refresh</base-button>
          <base-button link v-if="hasCompanies" to="/addcompany"
            >Add company</base-button
          >
        </div>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <ul v-else-if="hasCompanies">
          <company-item
            v-for="company in filteredCompanies"
            :key="company.ID"
            :ID="company.ID"
            :Name="company.Name"
            :Address="company.Address"
            :City="company.City"
            :Country="company.Country"
            :Email="company.Email"
            :StartTime="company.StartTime"
            :EndTime="company.EndTime"
          ></company-item>
        </ul>
        <h3 v-else-if="!isLoading">
          There are no companies for this user.
          <router-link to="/addcompany">Add one!</router-link>
        </h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import CompanyItem from "../../components/companies/CompanyItem.vue";
export default {
  data() {
    return {
      isLoading: false,
      error: null,
    };
  },
  components: {
    CompanyItem,
  },
  computed: {
    filteredCompanies() {
      return this.$store.getters["companies/companies"];
    },
    hasCompanies() {
      return !this.isLoading && this.$store.getters["companies/hasCompanies"];
    },
  },
  methods: {
    async loadCompanies() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("companies/loadCompanies");
      } catch (error) {
        this.error =
          `${error.message} in getting companies.` || "Something went wrong!";
      }
      this.isLoading = false;
    },
    async refresh() {
      await this.loadCompanies();
    },
    handleError() {
      this.error = null;
    }
  },
  async created() {
    await this.loadCompanies();
    localStorage.removeItem("comid");
    localStorage.removeItem("depid");
    localStorage.removeItem("empid");
  },
};
</script>

<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: space-between;
}
</style>
