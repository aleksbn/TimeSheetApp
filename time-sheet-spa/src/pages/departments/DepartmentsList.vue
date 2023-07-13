<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <base-card>
        <div class="controls">
          <base-button @click="refresh">Refresh</base-button>
          <base-button link to="/adddepartment"
            >Add another department</base-button
          >
        </div>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <ul v-if="hasDepartments">
          <department-item
            v-for="department in filteredDepartments"
            :key="department.ID"
            :comid="this.comid"
            :ID="department.ID"
            :Name="department.Name"
          >
          </department-item>
        </ul>
        <h3 v-else-if="!isLoading">
          There are no departments for this company.
          <router-link to="/adddepartment">Add one!</router-link>
        </h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import DepartmentItem from "../../components/departments/DepartmentItem.vue";
export default {
  data() {
    return {
      isLoading: false,
      error: null
    };
  },
  props: ["comid"],
  components: {
    DepartmentItem,
  },
  computed: {
    hasDepartments() {
      return !this.isLoading && this.$store.getters["departments/hasDepartments"];
    },
    filteredDepartments() {
      return this.$store.getters["departments/departments"];
    },
  },
  methods: {
    async loadDepartments() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("departments/loadDepartments", {
          comid: this.comid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting departments" || "Something went wrong!";
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    },
    async refresh() {
      await this.loadDepartments();
    }
  },
  async created() {
    await this.loadDepartments();
    localStorage.removeItem("depid");
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
