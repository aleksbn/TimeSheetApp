<template>
  <tr>
    <td>{{ ID }}</td>
    <td>{{ fullName }}</td>
    <td>{{ JobTitle }}</td>
    <td>{{ Degree }}</td>
    <td>{{ Phone }}</td>
    <td>{{ Department.name }}</td>
    <td>{{ HourlyRate }} $</td>
    <td><base-button link :to="detailsLink">Details</base-button></td>
  </tr>
</template>

<script>
export default {
  props: [
    "ID",
    "FirstName",
    "LastName",
    "JobTitle",
    "Degree",
    "Phone",
    "Address",
    "DateOfBirth",
    "StartOdEmployment",
    "HourlyRate",
    "DepartmentId",
    "Department"
  ],
  computed: {
    fullName() {
      return `${this.FirstName} ${this.LastName}`;
    },
    detailsLink() {
      if (localStorage.getItem("depid") !== null) {
        return (
          `${this.$route.path.split("/").slice(0, -3).join("/")}/employeedetails/${this.ID}`
        );
      } else {
        return (
          `${this.$route.path.split("/").slice(0, -2).join("/")}/employeedetails/${this.ID}`
        );
      }
    },
  },
};
</script>
