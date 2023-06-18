<template>
  <div>
    <base-card>
      <form @submit.prevent="stop()" action="/">
        <div class="form-control" v-if="this.Mode === 'old'">
          <label for="ID">ID:</label>
          <input type="text" name="ID" disabled :value="ID" />
        </div>
        <div class="form-control">
          <label for="Firstame">First name:</label>
          <input
            type="text"
            name="FirstName"
            :disabled="editMode"
            :value="FirstName"
          />
        </div>
        <div class="form-control">
          <label for="LastName">Last name:</label>
          <input
            type="text"
            name="LastName"
            :disabled="editMode"
            :value="LastName"
          />
        </div>
        <div class="form-control">
          <label for="JobTitle">Job title:</label>
          <input
            type="text"
            name="JobTitle"
            :disabled="editMode"
            :value="JobTitle"
          />
        </div>
        <div class="form-control">
          <label for="Degree">Degree:</label>
          <input
            type="text"
            name="Degree"
            :disabled="editMode"
            :value="Degree"
          />
        </div>
        <div class="form-control">
          <label for="DepartmentId">Department:</label>
          <select name="DepartmentId" id="DepartmentId" :disabled="editMode">
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
          <label for="eAddress">Address:</label>
          <input
            type="text"
            name="Address"
            :disabled="editMode"
            :value="Address"
          />
        </div>
        <div class="form-control">
          <label for="Phone">Phone:</label>
          <input type="text" name="Phone" :disabled="editMode" :value="Phone" />
        </div>
        <div class="form-control">
          <label for="DateOfBirth">Date of birth:</label>
          <input
            type="text"
            name="DateOfBirth"
            :disabled="editMode"
            :value="DateOfBirth ? new Date(DateOfBirth).toDateString() : ''"
          />
        </div>
        <div class="form-control">
          <label for="StartOfEmployment">Start of employment:</label>
          <input
            type="text"
            name="StartOfEmployment"
            :disabled="editMode"
            :value="
              StartOfEmployment
                ? new Date(StartOfEmployment).toDateString()
                : ''
            "
          />
        </div>
        <div class="form-control">
          <label for="HourlyRate">Hourly rate ($):</label>
          <input
            type="number"
            step="0.01"
            name="HourlyRate"
            :disabled="editMode"
            :value="HourlyRate"
          />
        </div>
        <div>
          <base-button style="display: inline" @click="toggleMode()">{{
            textForMode
          }}</base-button>
          <base-button
            v-if="this.ID"
            @click="openWorkingTimes()"
            style="display: inline"
            :to="'/workingtimes/' + this.ID"
            >Working times</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: [
    "ID",
    "FirstName",
    "LastName",
    "Degree",
    "JobTitle",
    "HourlyRate",
    "Department",
    "DepartmentId",
    "Address",
    "Phone",
    "DateOfBirth",
    "StartOfEmployment",
    "Mode"
  ],
  data() {
    return {
      mode: true,
    };
  },
  methods: {
    stop() {},
    toggleMode() {
      if (this.ID) {
        this.mode = !this.mode;
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
    departments() {
      return this.$store.getters["departments/departments"];
    },
    depId() {
      return this.DepartmentId;
    },
  },
  created() {
    this.loadDepartments();
    this.mode = !!this.ID;
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
