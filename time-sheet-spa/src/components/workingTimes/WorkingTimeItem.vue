<template>
  <tr>
    <td>{{ ID }}</td>
    <td>{{ fullName }}</td>
    <td>{{ new Date(WtDate).toDateString() }}</td>
    <td>{{ StartTime }}</td>
    <td>{{ EndTime }}</td>
  </tr>
</template>

<script>
export default {
  props: ["ID", "Employee", "WtDate", "StartTime", "EndTime"],
  computed: {
    fullName() {
      return this.Employee.firstName + " " + this.Employee.lastName;
    },
  },
};
</script>

<style scoped>
td {
  height: 2em;
}
</style>