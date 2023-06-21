<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <employee-filter @change-filter="setFilters"></employee-filter>
    </section>
    <section>
      <base-card>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <div class="controls">
          <base-button @click="refresh">Refresh</base-button>
          <base-button link to="/addemployee" v-if="hasEmployees"
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
            <th>Department</th>
            <th>Hourly rate</th>
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
            :Department="employee.Department"
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
import EmployeeFilter from "../../components/employees/EmployeeFilter.vue";
export default {
  components: {
    EmployeeItem,
    EmployeeFilter,
  },
  computed: {
    hasEmployees() {
      return !this.isLoading && this.$store.getters["employees/hasEmployees"];
    },
    filteredEmployees() {
      var employees = this.$store.getters["employees/employees"];
      employees = employees.filter((employee) => {
        if (employee.ID.includes(this.activeFilters.id)) {
          return true;
        }
      });
      employees = employees.filter((employee) => {
        if (
          employee.FirstName.toUpperCase().includes(
            this.activeFilters.firstName.toUpperCase()
          )
        ) {
          return true;
        }
      });
      employees = employees.filter((employee) => {
        if (
          employee.LastName.toUpperCase().includes(
            this.activeFilters.lastName.toUpperCase()
          )
        ) {
          return true;
        }
      });
      employees = employees.filter((employee) => {
        if (
          employee.Department.name
            .toUpperCase()
            .includes(this.activeFilters.department.toUpperCase())
        ) {
          return true;
        }
      });
      employees = employees.filter((employee) => {
        if (
          this.activeFilters.hourlyRate === 0 ||
          employee.HourlyRate === this.activeFilters.hourlyRate
        ) {
          return true;
        }
      });

      return employees;
    },
  },
  data() {
    return {
      isLoading: false,
      error: null,
      activeFilters: {
        id: "",
        firstName: "",
        lastName: "",
        department: "",
        hourlyRate: 0,
      },
    };
  },
  methods: {
    async loadEmployees() {
      this.isLoading = true;
      try {
        if (
          localStorage.getItem("comid") !== null &&
          localStorage.getItem("depid") === null
        ) {
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
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    },
    refresh() {
      this.loadEmployees();
    },
    setFilters(updatedFilters) {
      this.activeFilters = updatedFilters;
      this.activeFilters.hourlyRate = isNaN(updatedFilters.hourlyRate)
        ? 0
        : parseFloat(updatedFilters.hourlyRate);
    },
  },
  created() {
    this.loadEmployees();
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
