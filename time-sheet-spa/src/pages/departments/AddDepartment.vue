<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new department</h2>
    </section>
    <section>
      <department-form
        @save-data="saveData"
        :Mode="this.EditMode"
      ></department-form>
    </section>
  </div>
</template>

<script>
import DepartmentForm from "../../components/departments/DepartmentForm.vue";
export default {
  components: {
    DepartmentForm,
  },
  data() {
    return {
      EditMode: "new",
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("departments/addDepartment", data);
      this.$router.push("/departments/" + localStorage.getItem('comid'));
    },
  },
};
</script>
