<template>
  <div>
    <section>FILTER</section>
    <section>
      <base-card>
        <div class="controls">
          <base-button>Refresh</base-button>
          <base-button link to="/" v-if="hasWorkingTimes"
            >Add a working time</base-button
          >
        </div>
        <table v-if="hasWorkingTimes">
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
export default {
  components: {
    WorkingTimeItem,
  },
  computed: {
    hasWorkingTimes() {
      return this.$store.getters["workingTimes/hasWorkingTimes"];
    },
    filteredWorkingTimes() {
      return this.$store.getters["workingTimes/workingTimes"];
    },
  },
  methods: {
    async loadWorkingTimes() {
      let id = null;
      let link = null;
      if (localStorage.getItem("empidwt")) {
        id = localStorage.getItem("empidwt");
        link = "https://localhost:7059/api/workingtime/fromemployee/";
      }
      else if (localStorage.getItem("depidwt")) {
        id = localStorage.getItem("depidwt");
        link = "https://localhost:7059/api/workingtime/fromdepartment/";
      }
      else if (localStorage.getItem("comidwt")) {
        id = localStorage.getItem("comidwt");
        link = "https://localhost:7059/api/workingtime/fromcompany/";
      }
      try {
        await this.$store.dispatch("workingTimes/loadWorkingTimes", {
          id: id,
          link: link,
        });
      } catch (error) {
        this.error =
          error.message + " in getting employees" || "Something went wrong!";
      }
    },
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
