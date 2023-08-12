<template #default>
  <div>
    <p style="text-align: justify">
      Would you like to delete the employee? If you choose YES, all of the
      employee's working times will be deleted as well.
    </p>
  </div>
  <div class="form-control">
    <base-button style="display: inline" @click="deleteEmployee"
      >Delete</base-button
    >
    <base-button style="display: inline" @click="cancel">Cancel</base-button>
  </div>
</template>

<script>
export default {
  emits: ["cancel"],
  methods: {
    async deleteEmployee() {
      try {
        await this.$store.dispatch("employees/deleteEmployee", {
          empId: localStorage.getItem("empid"),
        });
        let link = "/employees/" + localStorage.getItem("comid");
        if (localStorage.getItem("depid") !== null) {
          link += "/" + localStorage.getItem("depid");
        }
        this.$router.push(link);
      } catch (error) {
        this.error =
          error.message + " in deleting employee." || "Something went wrong!";
      }
    },
    cancel() {
      this.$emit("cancel");
    },
  },
};
</script>
