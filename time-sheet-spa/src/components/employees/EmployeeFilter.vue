<template>
  <base-card>
    <form action="/">
      <div class="form-control">
        <label for="ID">ID:</label>
        <input type="text" name="id" id="id" @input="setFilter" />
      </div>
      <div class="form-control">
        <label for="firstName">First name:</label>
        <input type="text" name="firstName" id="firstName" @input="setFilter" />
      </div>
      <div class="form-control">
        <label for="lastName">Last name:</label>
        <input type="text" name="lastName" id="lastName" @input="setFilter" />
      </div>
      <div class="form-control">
        <label for="department">Department:</label>
        <input
          type="text"
          name="department"
          id="department"
          @input="setFilter"
        />
      </div>
      <div class="form-control">
        <label for="hourlyRate">Hourly rate:</label>
        <input
          type="number"
          name="hourlyRate"
          id="hourlyRate"
          value="0"
          min="0"
          @change="setFilter"
        />
      </div>
    </form>
  </base-card>
</template>

<script>
export default {
  emits: ["change-filter"],
  data() {
    return {
      filters: {
        id: "",
        firstName: "",
        lastName: "",
        department: "",
        hourlyRate: 0,
      },
    };
  },
  methods: {
    setFilter(event) {
      const inputId = event.target.id;
      var value = event.target.value;
      if (inputId === "hourlyRate") {
        if (value === "") {
          value = 0;
        }
      }
      const updatedFilter = {
        ...this.filters,
        [inputId]: value,
      };
      this.filters = updatedFilter;
      this.$emit("change-filter", this.filters);
    },
  },
};
</script>

<style scoped>
.form-control {
  display: inline;
}
input {
  height: 2em;
  border-radius: 5px;
  width: 12%;
}
label {
  padding: 10px;
  width: 7%;
}
</style>
