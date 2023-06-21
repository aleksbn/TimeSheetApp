<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <department-form
      v-else-if="hasDepartment"
      @save-data="saveData"
      :key="department.ID"
      :comid="this.comid"
      :ID="department.ID"
      :Name="department.Name"
      :Mode="this.EditMode"
    ></department-form>
    <h3 v-else>There's some problems with loading of this department.</h3>
  </div>
</template>

<script>
import DepartmentForm from "../../components/departments/DepartmentForm.vue";

export default {
  components: {
    DepartmentForm,
  },
  props: ["depid", "comid"],
  data() {
    return {
      error: null,
      isLoading: false,
      mode: true,
      EditMode: "old",
    };
  },
  methods: {
    saveData(data) {
      this.$store.dispatch("departments/editDepartment", data);
      this.$router.push("/departments/" + this.comid);
    },
    async loadDepartment() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("departments/loadDepartment", {
          comid: this.comid,
          depid: this.depid,
        });
      } catch (error) {
        this.error =
          error.message + " in getting department." || "Something went wrong!";
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    }
  },
  created() {
    this.loadDepartment();
    localStorage.setItem("depid", this.depid);
    localStorage.setItem("depidwt", this.depid);
  },
  computed: {
    hasDepartment() {
      return !this.isLoading && this.$store.getters["departments/hasDepartment"];
    },
    department() {
      return this.$store.getters["departments/department"];
    },
  },
};
</script>
