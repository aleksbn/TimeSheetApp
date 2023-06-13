<template>
  <div>
    <section>FILTER</section>
    <section>
      <base-card>
        <div class="controls">
          <base-button>Refresh</base-button>
          <base-button link to="/" v-if="hasEmployees"
            >Add employee</base-button
          >
        </div>
        <table v-if="hasEmployees">
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Title</th>
            <th>Degree</th>
            <th>Phone</th>
            <th>Actions</th>
          </tr>
          <employee-item
            v-for="employee in filteredEmployees"
            :key="employee.ID"
            :ID="employee.ID"
            :FirstName="employee.FirstName"
            :LastName="employee.LastName"
            :JobTitle="employee.JobTitle"
            :Degree="employee.Degree"
            :Address="employee.Address"
            :Phone="employee.Phone"
            :DateOfBirth="employee.DateOfBirth"
            :StartOfEmployment="employee.StartOfEmployment"
            :HourlyRate="employee.HourlyRate"
            :DepartmentId="employee.DepartmentId"
          ></employee-item>
        </table>
        <h3 v-else>
          There are no employees.
          <router-link to="/">Add one!</router-link>
        </h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import EmployeeItem from "../../components/employees/EmployeeItem.vue";
export default {
  components: {
    EmployeeItem,
  },
  computed: {
    hasEmployees() {
      return this.$store.getters["employees/hasEmployees"];
    },
    filteredEmployees() {
      return this.$store.getters["employees/employees"];
    },
  },
  methods: {
    async loadEmployees() {
      try {
        if (localStorage.getItem("comid") !== null && localStorage.getItem("depid") === null) {
          await this.$store.dispatch("employees/loadEmployeesFromCompany", {
            comid: localStorage.getItem("comid"),
          });
        } else {
          await this.$store.dispatch("employees/loadEmployeesFromDepartment", {
            comid: localStorage.getItem("comid"),
            depid: localStorage.getItem("depid"),
          });
        }
      } catch (error) {
        this.error =
          error.message + " in getting employees" || "Something went wrong!";
      }
    },
  },
  created() {
    this.loadEmployees();
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

th, td {
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
