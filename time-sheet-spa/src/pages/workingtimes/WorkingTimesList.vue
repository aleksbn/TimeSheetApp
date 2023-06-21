<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <working-time-filter
        @params-changed="setParams"
        v-if="show"
      ></working-time-filter>
    </section>
    <section>
      <base-card>
        <div class="controls">
          <base-button @click="refresh">Refresh</base-button>
          <base-button link to="/" v-if="hasWorkingTimes"
            >Add a working time</base-button
          >
        </div>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <table v-else-if="hasWorkingTimes">
          <tr>
            <th>ID</th>
            <th>Employee name</th>
            <th>Date</th>
            <th>Start time</th>
            <th>End time</th>
          </tr>
          <working-time-item
            v-for="wt in filteredWorkingTimes"
            :key="wt.ID"
            :ID="wt.ID"
            :Employee="wt.Employee"
            :WtDate="wt.Date"
            :StartTime="wt.StartTime"
            :EndTime="wt.EndTime"
          ></working-time-item>
        </table>
        <h3 v-else>
          There are no working times.
          <router-link to="/">Add one!</router-link>
        </h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import WorkingTimeItem from "../../components/workingTimes/WorkingTimeItem.vue";
import { defineAsyncComponent } from "vue";

export default {
  components: {
    WorkingTimeItem,
    WorkingTimeFilter: defineAsyncComponent(() =>
      import("../../components/workingTimes/WorkingTimeFilter.vue")
    ),
  },
  data() {
    return {
      isLoading: false,
      id: null,
      link: null,
      error: null,
      activeFilters: {
        pageNumber: 0,
        pageSize: 10,
      },
      show: false,
    };
  },
  computed: {
    hasWorkingTimes() {
      return (
        !this.isLoading && this.$store.getters["workingTimes/hasWorkingTimes"]
      );
    },
    filteredWorkingTimes() {
      return this.$store.getters["workingTimes/workingTimes"];
    },
  },
  watch: {
    activeFilters() {
      this.loadWorkingTimes();
    },
  },
  methods: {
    setParams(updatedParams) {
      this.activeFilters = updatedParams;
    },
    async loadWorkingTimes() {
      this.isLoading = true;
      if (localStorage.getItem("empidwt")) {
        this.id = localStorage.getItem("empidwt");
        this.link = "https://localhost:7059/api/workingtime/fromemployee/";
      } else if (localStorage.getItem("depidwt")) {
        this.id = localStorage.getItem("depidwt");
        this.link = "https://localhost:7059/api/workingtime/fromdepartment/";
      } else if (localStorage.getItem("comidwt")) {
        this.id = localStorage.getItem("comidwt");
        this.link = "https://localhost:7059/api/workingtime/fromcompany/";
      }
      try {
        await this.$store.dispatch("workingTimes/loadWorkingTimes", {
          id: this.id,
          link: this.link,
          pageNumber: this.activeFilters.pageNumber,
          pageSize: this.activeFilters.pageSize,
        });
        this.show = true;
      } catch (error) {
        this.error =
          error.message + " in getting employees" || "Something went wrong!";
      }
      this.isLoading = false;
    },
    refresh() {
      this.loadWorkingTimes();
    },
    handleError() {
      this.error = null;
    }
  },
  created() {
    this.loadWorkingTimes();
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

table {
  border-collapse: collapse;
  width: 100%;
  border-radius: 15px;
  margin: 20px 0;
}

th,
td {
  text-align: left;
  border-bottom: 1px solid #ddd;
  padding: 15px;
  margin: 15px;
}

tr:nth-child(even) {
  background-color: #f2f2f2; /* Light gray */
}

tr:nth-child(odd) {
  background-color: rgba(255, 0, 0, 0.801); /* Red */
  color: #fff; /* White text on red rows */
}

tr:nth-child(even):hover,
tr:nth-child(odd):hover {
  background-color: rgba(0, 0, 255, 0.5); /* Blue on hover */
  color: #fff; /* White text on blue rows on hover */
}
</style>
