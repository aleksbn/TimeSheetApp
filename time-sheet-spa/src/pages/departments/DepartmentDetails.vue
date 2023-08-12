<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <department-form v-else-if="hasDepartment" @save-data="saveData" @delete-department="deleteDepartment"
      :key="department.ID" :comid="this.comid" :ID="department.ID" :Name="department.Name"
      :Mode="this.EditMode"></department-form>
    <h3 v-else-if="!isLoading">There's some problems with loading of this department.</h3>
    <base-dialog :showClose="false" fixed title="Select some delete options" :show="deleting">
      <department-delete-options @cancel="cancelDeletion"></department-delete-options>
    </base-dialog>
  </div>
</template>

<script>
import DepartmentForm from "../../components/departments/DepartmentForm.vue";
import DepartmentDeleteOptions from "@/components/departments/DepartmentDeleteOptions.vue";

export default {
  components: {
    DepartmentForm,
    DepartmentDeleteOptions,
  },
  props: ["depid", "comid"],
  data() {
    return {
      error: null,
      isLoading: false,
      deleting: false,
      mode: true,
      EditMode: "old",
    };
  },
  methods: {
    async saveData(data) {
      await this.$store.dispatch("departments/editDepartment", data);
      this.$router.push("/departments/" + this.comid);
    },
    cancelDeletion() {
      this.deleting = false;
    },
    deleteDepartment() {
      this.deleting = true;
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
          `${error.message} in getting department.` || "Something went wrong!";
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    },
  },
  async created() {
    await this.loadDepartment();
    localStorage.setItem("depid", this.depid);
    localStorage.setItem("depidwt", this.depid);
  },
  computed: {
    hasDepartment() {
      return (
        !this.isLoading && this.$store.getters["departments/hasDepartment"]
      );
    },
    department() {
      return this.$store.getters["departments/department"];
    },
  },
};
</script>
