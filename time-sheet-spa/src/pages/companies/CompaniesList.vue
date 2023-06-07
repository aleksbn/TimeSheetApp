<template>
  <div>
    <section>FILTER</section>
    <section>
      <base-card>
        <div class="controls">
          <base-button>Refresh</base-button>
          <base-button link v-if="hasCompanies" to="/">Add company</base-button>
        </div>
        <ul v-if="hasCompanies">
          <company-item
            v-for="company in filteredCompanies"
            :key="company.ID"
            :ID="company.ID"
            :Name="company.Name"
            :Address="company.Address"
            :City="company.City"
            :Country="company.Country"
            :Email="company.Email"
          ></company-item>
        </ul>
        <h3 v-else>
          There are no companies for this user.
          <router-link to="/">Add one!</router-link>
        </h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import CompanyItem from "../../components/companies/CompanyItem.vue";
export default {
  components: {
    CompanyItem,
  },
  computed: {
    filteredCompanies() {
      return this.$store.getters["companies/companies"];
    },
    hasCompanies() {
      return this.$store.getters["companies/hasCompanies"];
    },
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
