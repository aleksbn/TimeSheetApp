<template>
  <div>
    <section>
      <h2 style="text-align: center">Add a new employee</h2>
    </section>
    <section>
      <employee-form :Mode="this.mode"></employee-form>
    </section>
  </div>
</template>

<script>
import EmployeeForm from "../../components/employees/EmployeeForm.vue";
export default {
  components: {
    EmployeeForm,
  },
  data() {
    return {
      mode: "new",
    };
  },
};
</script>
