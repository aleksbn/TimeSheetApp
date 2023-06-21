<template>
  <div>
    <section>
      <base-card>
        <div class="controls">
          <base-button>Refresh</base-button>
          <base-button link to="/adddepartment">Add another department</base-button>
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
        <h3 v-else>
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
  props: ["comid"],
  components: {
    DepartmentItem,
  },
  computed: {
    hasDepartments() {
      return this.$store.getters["departments/hasDepartments"];
    },
    filteredDepartments() {
      return this.$store.getters["departments/departments"];
    },
  },
  methods: {
    async loadDepartments() {
      try {
        await this.$store.dispatch("departments/loadDepartments", { comid: this.comid });
      } catch (error) {
        this.error = error.message + " in getting departments" || "Something went wrong!";
      }
    }
  },
  created() {
    setTimeout(() => {
      this.loadDepartments();
    }, 200);
    localStorage.removeItem("depid");
  }
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