<template>
  <div>
    <base-card>
      <form v-if="hasCompany" @submit.prevent="stop()" action="/">
        <div class="form-control">
          <label for="comid">ID:</label>
          <input type="text" name="comid" disabled :value="comid" />
        </div>
        <div class="form-control">
          <label for="company.Name">Name:</label>
          <input
            type="text"
            name="company.Name"
            :disabled="editMode"
            :value="company.Name"
          />
        </div>
        <div class="form-control">
          <label for="company.Address">Address:</label>
          <input
            type="text"
            name="company.Address"
            :disabled="editMode"
            :value="company.Address"
          />
        </div>
        <div class="form-control">
          <label for="company.City">City:</label>
          <input
            type="text"
            name="company.City"
            :disabled="editMode"
            :value="company.City"
          />
        </div>
        <div class="form-control">
          <label for="company.Country">Country:</label>
          <input
            type="text"
            name="company.Country"
            :disabled="editMode"
            :value="company.Country"
          />
        </div>
        <div class="form-control">
          <label for="company.Email">Email:</label>
          <input
            type="text"
            name="company.Email"
            :disabled="editMode"
            :value="company.Email"
          />
        </div>
        <div>
          <base-button style="display: inline" @click="toggleMode()">{{
            textForMode
          }}</base-button>
          <base-button
            link
            style="display: inline"
            :to="'/departments/' + this.comid"
            >Departments</base-button
          >
          <base-button
            @click="openEmployees()"
            style="display: inline"
            >Employees</base-button
          >
          <base-button
            @click="openWorkingTimes()"
            style="display: inline"
            >Working times</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: ["comid"],
  data() {
    return {
      mode: true,
    };
  },
  methods: {
    async loadCompany() {
      try {
        await this.$store.dispatch("companies/loadCompany", { id: this.comid });
        localStorage.setItem("comid", this.comid);
      } catch (error) {
        this.error =
          error.message + " in getting company." || "Something went wrong!";
      }
    },
    stop() {},
    toggleMode() {
      this.mode = !this.mode;
    },
    openEmployees() {
      localStorage.removeItem("depid");
      this.$router.push("/employees/" + this.comid);
    },
    openWorkingTimes() {
      localStorage.removeItem("depidwt");
      localStorage.removeItem("empidwt");
      this.$router.push("/workingtimes/" + this.comid);
    },
  },
  created() {
    this.loadCompany();
    localStorage.setItem("comid", this.comid);
    localStorage.setItem("comidwt", this.comid);
  },
  computed: {
    editMode() {
      return this.mode;
    },
    textForMode() {
      return this.mode === false ? "Save" : "Edit";
    },
    hasCompany() {
      return this.$store.getters["companies/hasCompany"];
    },
    company() {
      return this.$store.getters["companies/company"];
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

input {
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
