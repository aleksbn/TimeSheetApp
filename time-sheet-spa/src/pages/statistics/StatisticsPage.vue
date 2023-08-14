<template>
  <div>
    <section>
      <statistics-filter @filter-changed="changeData"></statistics-filter>
    </section>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <base-card>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <table v-if="hasStatistics">
          <tr>
            <th>ID</th>
            <th>Full name</th>
            <th>Department</th>
            <th>Hourly rate</th>
            <th>Total working days</th>
            <th>Regular working hours</th>
            <th>Overtime working hours</th>
            <th>Total earnings</th>
          </tr>
          <tr v-for="stat in statistics" :key="stat.ID">
            <td>{{ stat.ID }}</td>
            <td>{{ stat.FirstName + " " + stat.LastName }}</td>
            <td>{{ stat.Department }}</td>
            <td>{{ stat.HourlyRate }} $</td>
            <td>{{ stat.WorkingDays }}</td>
            <td>{{ stat.RegularWorkingHours }}</td>
            <td>{{ stat.OvertimeHours }}</td>
            <td>{{ stat.Earnings.toFixed(2) }} $</td>
          </tr>
        </table>
        <p v-else-if="!isLoading">
          There are no statistics for this company, or for this period. You must add some data
          first, or revisit filter section.
        </p>
      </base-card>
    </section>
  </div>
</template>

<script>
import StatisticsFilter from "../../components/statistics/StatisticsFilter.vue";

export default {
  data() {
    return {
      error: null,
      isLoading: false,
      filterData: {
        month: 0,
        year: 0,
      },
    };
  },
  components: {
    StatisticsFilter,
  },
  methods: {
    async loadStatistics() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("statistics/loadStatistics", {
          comid: parseInt(localStorage.getItem("comid")),
          month: this.filterData.month,
          year: this.filterData.year,
        });
      } catch (error) {
        this.error =
          `${error.message} in getting statistics.` || "Something went wrong!";
      }
      this.isLoading = false;
    },
    async changeData(data) {
      this.filterData = data;
      await this.loadStatistics();
    },
    handleError() {
      this.error = null;
    },
  },
  computed: {
    statistics() {
      return this.$store.getters["statistics/statistics"];
    },
    hasStatistics() {
      return this.$store.getters["statistics/hasStatistics"];
    },
  },
  async created() {
    await this.loadStatistics();
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
  background-color: #f2f2f2;
  /* Light gray */
}

tr:nth-child(odd) {
  background-color: rgba(255, 0, 0, 0.801);
  /* Red */
  color: #fff;
  /* White text on red rows */
}

tr:nth-child(even):hover,
tr:nth-child(odd):hover {
  background-color: rgba(0, 0, 255, 0.5);
  /* Blue on hover */
  color: #fff;
  /* White text on blue rows on hover */
}
</style>
