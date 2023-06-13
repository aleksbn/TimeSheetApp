<template>
  <div>
    <base-card>
      <form v-if="hasDepartment" @submit.prevent="stop()" action="/">
        <div class="form-control">
          <label for="department.ID">ID:</label>
          <input
            type="text"
            name="department.ID"
            disabled
            :value="department.ID"
          />
        </div>
        <div class="form-control">
          <label for="department.Name">Name:</label>
          <input
            type="text"
            name="department.Name"
            :disabled="editMode"
            :value="department.Name"
          />
        </div>
        <div>
          <base-button style="display: inline" @click="toggleMode()">{{
            textForMode
          }}</base-button>
          <base-button
            link
            style="display: inline"
            :to="'/employees/' + this.comid + '/' + this.depid"
            >Employees</base-button
          >
          <base-button
            @click="openWorkingTimes()"
            style="display: inline"
            :to="'/workingtimes/' + this.depid"
            >Working times</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: ["depid", "comid"],
  data() {
    return {
      mode: true,
    };
  },
  methods: {
    async loadDepartment() {
      try {
        await this.$store.dispatch("departments/loadDepartment", {
          comid: this.comid,
          depid: this.depid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting department." || "Something went wrong!";
      }
    },
    stop() {},
    toggleMode() {
      this.mode = !this.mode;
    },
    openWorkingTimes() {
      localStorage.removeItem("comidwt");
      localStorage.removeItem("empidwt");
      this.$router.push("/workingtimes/" + this.depid);
    },
  },
  created() {
    this.loadDepartment();
    localStorage.setItem("depid", this.depid);
    localStorage.setItem("depidwt", this.depid);
  },
  computed: {
    editMode() {
      return this.mode;
    },
    textForMode() {
      return this.mode === false ? "Save" : "Edit";
    },
    hasDepartment() {
      return this.$store.getters["departments/hasDepartment"];
    },
    department() {
      return this.$store.getters["departments/department"];
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
