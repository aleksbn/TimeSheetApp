<template>
  <div>
    <base-card>
      <form v-if="hasEmployee" @submit.prevent="stop()" action="/">
        <div class="form-control">
          <label for="empid">ID:</label>
          <input type="text" name="empid" disabled :value="empid" />
        </div>
        <div class="form-control">
          <label for="employee.Firstame">First name:</label>
          <input
            type="text"
            name="employee.FirstName"
            :disabled="editMode"
            :value="employee.FirstName"
          />
        </div>
        <div class="form-control">
          <label for="employee.LastName">Last name:</label>
          <input
            type="text"
            name="employee.LastName"
            :disabled="editMode"
            :value="employee.LastName"
          />
        </div>
        <div class="form-control">
          <label for="employee.JobTitle">Job title:</label>
          <input
            type="text"
            name="employee.JobTitle"
            :disabled="editMode"
            :value="employee.JobTitle"
          />
        </div>
        <div class="form-control">
          <label for="employee.Degree">Degree:</label>
          <input
            type="text"
            name="employee.Degree"
            :disabled="editMode"
            :value="employee.Degree"
          />
        </div>
        <div class="form-control">
          <label for="employee.DepartmentId">Department:</label>
          <select
            name="employee.DepartmentId"
            id="employee.DepartmentId"
            :disabled="editMode"
          >
            <option
              v-for="dep in departments"
              :key="dep.ID"
              :value="dep.ID"
              :selected="depId === dep.ID ? true : false"
            >
              {{ dep.Name }}
            </option>
          </select>
        </div>
        <div class="form-control">
          <label for="employee.Address">Address:</label>
          <input
            type="text"
            name="employee.Address"
            :disabled="editMode"
            :value="employee.Address"
          />
        </div>
        <div class="form-control">
          <label for="employee.Phone">Phone:</label>
          <input
            type="text"
            name="employee.Phone"
            :disabled="editMode"
            :value="employee.Phone"
          />
        </div>
        <div class="form-control">
          <label for="employee.DateOfBirth">Date of birth:</label>
          <input
            type="text"
            name="employee.DateOfBirth"
            :disabled="editMode"
            :value="new Date(employee.DateOfBirth).toDateString()"
          />
        </div>
        <div class="form-control">
          <label for="employee.StartOfEployment">Start of employment:</label>
          <input
            type="text"
            name="employee.StartOfEployment"
            :disabled="editMode"
            :value="new Date(employee.StartOfEployment).toDateString()"
          />
        </div>
        <div class="form-control">
          <label for="employee.HourlyRate">Hourly rate:</label>
          <input
            type="number"
            step="0.01"
            name="employee.HourlyRate"
            :disabled="editMode"
            :value="employee.HourlyRate"
          />
        </div>

        <div>
          <base-button style="display: inline" @click="toggleMode()">{{
            textForMode
          }}</base-button>
          <base-button
            @click="openWorkingTimes()"
            style="display: inline"
            :to="'/workingtimes/' + this.empid"
            >Working times</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: ["empid"],
  data() {
    return {
      mode: true,
    };
  },
  created() {
    this.loadEmployee();
    this.loadDepartments();
    localStorage.setItem("empidwt", this.empid);
  },
  methods: {
    stop() {},
    toggleMode() {
      this.mode = !this.mode;
    },
    async loadEmployee() {
      try {
        await this.$store.dispatch("employees/loadEmployee", {
          empid: this.empid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting employee." || "Something went wrong!";
      }
    },
    async loadDepartments() {
      try {
        await this.$store.dispatch("departments/loadDepartments", {
          comid: localStorage.getItem("comid"),
        });
      } catch (error) {
        this.error =
          error.message + " in getting departments" || "Something went wrong!";
      }
    },
    openWorkingTimes() {
      localStorage.removeItem("comidwt");
      localStorage.removeItem("depidwt");
      this.$router.push("/workingtimes/" + this.empid);
    },
  },
  computed: {
    editMode() {
      return this.mode;
    },
    textForMode() {
      return this.mode === false ? "Save" : "Edit";
    },
    hasEmployee() {
      return this.$store.getters["employees/hasEmployee"];
    },
    employee() {
      return this.$store.getters["employees/employee"];
    },
    departments() {
      return this.$store.getters["departments/departments"];
    },
    depId() {
      return this.employee.DepartmentId;
    },
  },
};
</script>

<style scoped>
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input,
select {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
  font-size: 1em;
  border-radius: 15px;
  height: 40px;
}

input:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}

.invalid label {
  color: red;
}

.invalid input {
  border: 1px solid red;
}
</style>
