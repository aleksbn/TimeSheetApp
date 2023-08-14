<template>
  <base-card>
    <form>
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
        <input type="text" name="department" id="department" @input="setFilter" />
      </div>
      <div class="form-control">
        <label for="hourlyRate">Hourly rate:</label>
        <input type="number" name="hourlyRate" id="hourlyRate" value="0" min="0" @change="setFilter" />
      </div>
      <br />
      <br />
      <div style="text-align: center">
        <div class="form-control">
          <label>Page number <span style="margin: 0 20px">/</span> Page size</label>
          <select name="pageNumber" id="pageNumber" @change="setFilter">
            <option v-for="number in pageSizeNumbers" :key="number" :value="number">
              {{ number + 1 }}
            </option>
          </select>
        </div>
        /
        <div class="form-control">
          <select name="pageSize" id="pageSize" @change="setFilter">
            <option v-for="number in pageSize" :key="number" :value="number">
              {{ number }}
            </option>
          </select>
        </div>
      </div>
    </form>
  </base-card>
</template>

<script>
export default {
  emits: ["change-filter"],
  data() {
    return {
      pageNumbers: [],
      pageSize: [10, 20, 50, 100],
      numberOfRecords: 0,
      filters: {
        id: "",
        firstName: "",
        lastName: "",
        department: "",
        hourlyRate: 0,
        pageNumber: 0,
        pageSize: 10,
      },
    };
  },
  computed: {
    pageSizeNumbers() {
      return this.pageNumbers;
    },
  },
  async created() {
    this.numberOfRecords = await this.$store.getters["employees/numberOfEmployees"];
    this.loadSelect();
  },
  methods: {
    setFilter(event) {
      const inputId = event.target.id;
      let value = event.target.value;
      if (inputId === "hourlyRate") {
        if (value === "") {
          value = 0;
        }
      }
      if (inputId === "pageSize" || inputId === "pageNumber") {
        value = parseInt(value);
      }

      const updatedFilter = {
        ...this.filters,
        [inputId]: value,
      };
      if (inputId === "pageSize") {
        this.filters.pageNumber = 0;
      }
      this.filters = updatedFilter;
      this.$emit("change-filter", this.filters);
    },
    loadSelect() {
      this.pageNumbers = [];
      for (let i = 0; i < this.numberOfRecords; i += this.filters.pageSize) {
        this.pageNumbers.push(i / this.filters.pageSize);
      }
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
  border-radius: 15px;
  width: 12%;
}

label {
  padding: 10px;
  width: 7%;
}

select {
  width: 12%;
  height: 2.2em;
  border-radius: 15px;
  text-align: center;
  margin: 0 20px;
}
</style>
