<template>
  <tr>
    <td>{{ ID }}</td>
    <td>{{ fullName }}</td>
    <td>{{ new Date(WtDate).toDateString() }}</td>
    <td>{{ StartTime }}</td>
    <td>{{ EndTime }}</td>
    <td id="actions">
      <base-button :type="'button'" style="display: inline;" @click="editWT">Edit</base-button>
      <base-button :type="'button'" style="display: inline;" @click="deleteWT">Delete</base-button>
    </td>
  </tr>
</template>

<script>
export default {
  props: ["ID", "Employee", "WtDate", "StartTime", "EndTime"],
  emits: ["delete", "edit"],
  computed: {
    fullName() {
      return this.Employee.firstName + " " + this.Employee.lastName;
    },
  },
  methods: {
    deleteWT() {
      this.$emit("delete", this.ID);
    },
    editWT() {
      this.$emit("edit", this.ID);
    }
  }
};
</script>

<style scoped>
td {
  height: 2em;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

#actions {
  width: 15%;
}
</style>