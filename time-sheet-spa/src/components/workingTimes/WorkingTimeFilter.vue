<template>
  <section>
    <base-card style="text-align: center">
      <label style="font-weight: bold"
        >Page number <span style="margin: 0 20px">/</span> Page size</label
      >
      <select name="pageNumber" id="pageNumber" @change="setParams">
        <option v-for="number in pageSizeNumbers" :key="number" :value="number">
          {{ number + 1 }}
        </option>
      </select>
      /
      <select name="pageSize" id="pageSize" @change="setParams">
        <option v-for="number in pageSize" :key="number" :value="number">
          {{ number }}
        </option>
      </select>
    </base-card>
  </section>
</template>

<script>
export default {
  emits: ["params-changed"],
  data() {
    return {
      pageNumbers: [],
      pageSize: [10, 20, 50, 100],
      numberOfRecords: 0,
      params: {
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
  created() {
    this.numberOfRecords =
      this.$store.getters["workingTimes/numberOfWorkingTimes"];
    this.loadSelect();
  },
  methods: {
    setParams(event) {
      const inputId = event.target.id;
      const value = parseInt(event.target.value);
      const updatedParams = {
        ...this.params,
        [inputId]: value,
      };
      this.params = updatedParams;
      if(inputId === "pageSize") {
        this.params.pageNumber = 0;
      }
      this.$emit("params-changed", this.params);
      this.loadSelect();
    },
    loadSelect() {
      this.pageNumbers = [];
      for (let i = 0; i < this.numberOfRecords; i += this.params.pageSize) {
        this.pageNumbers.push(i / this.params.pageSize);
      }
    },
  },
};
</script>

<style scoped>
select {
  width: 75px;
  height: 2em;
  border-radius: 15px;
  text-align: center;
  margin: 0 20px;
}
</style>
