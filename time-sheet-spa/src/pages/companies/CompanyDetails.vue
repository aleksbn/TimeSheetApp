<template>
  <div>
    <base-card>
      <form @submit.prevent="stop()" action="/">
        <div class="form-control">
          <label for="comid">ID:</label>
          <input type="text" name="comid" disabled :value="comid" />
        </div>
        <div class="form-control">
          <label for="selectedCompany.Name">Name:</label>
          <input type="text" name="selectedCompany.Name" :disabled="editMode" :value="selectedCompany.Name" />
        </div>
        <div class="form-control">
          <label for="selectedCompany.Address">Address:</label>
          <input
            type="text"
            name="selectedCompany.Address"
            :disabled="editMode"
            :value="selectedCompany.Address"
          />
        </div>
        <div class="form-control">
          <label for="selectedCompany.City">City:</label>
          <input type="text" name="selectedCompany.City" :disabled="editMode" :value="selectedCompany.City" />
        </div>
        <div class="form-control">
          <label for="selectedCompany.Country">Country:</label>
          <input
            type="text"
            name="selectedCompany.Country"
            :disabled="editMode"
            :value="selectedCompany.Country"
          />
        </div>
        <div class="form-control">
          <label for="selectedCompany.Email">Email:</label>
          <input type="text" name="selectedCompany.Email" :disabled="editMode" :value="selectedCompany.Email" />
        </div>
        <div class="form-control">
          <base-button @click="toggleMode()">{{ textForMode }}</base-button>
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
      selectedCompany: null,
      mode: true,
    };
  },
  created() {
    this.selectedCompany = this.$store.getters["companies/companies"].find(
      (c) => c.comid === this.ID
    );
  },
  computed: {
    editMode() {
      return this.mode;
    },
    textForMode() {
      return this.mode === false ? "Save" : "Edit";
    },
  },
  methods: {
    toggleMode() {
      this.mode = !this.mode;
    },
    stop() {
      
    }
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
