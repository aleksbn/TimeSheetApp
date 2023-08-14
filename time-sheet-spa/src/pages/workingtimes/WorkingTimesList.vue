<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" @close="handleError" :showClose="true">
      <p>{{ error }}</p>
    </base-dialog>
    <base-dialog :show="editing" title="Edit working time" :showClose="false">
      <working-time-form v-if="hasWorkingTime" :key="workingTime.ID" :ID="workingTime.ID" :Employee="workingTime.Employee"
        :WtDate="workingTime.WtDate" :StartTime="workingTime.StartTime" :EndTime="workingTime.EndTime"
        :Mode="editing === true ? 'old' : 'new'" @close="closeEditor" @cancel="cancelEditing"
        @catch-error="catchError"></working-time-form>
    </base-dialog>
    <base-dialog :show="creating" title="Add working data" :showClose="false">
      <add-working-time @close="close" @cancel="close" @catch-error="catchError"></add-working-time>
    </base-dialog>
    <section>
      <working-time-filter @params-changed="setParams" v-if="show"></working-time-filter>
    </section>
    <section>
      <base-card>
        <div class="controls">
          <base-button @click="refresh">Refresh</base-button>
          <base-button @click="create" v-if="hasWorkingTimes">Add a working time</base-button>
          <base-button link :to="addMultipleWTsLink" mode="a" style="display: inline">Add multiple working
            times</base-button>
        </div>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <table v-else-if="hasWorkingTimes">
          <tr>
            <th>ID</th>
            <th>Employee name</th>
            <th>Date</th>
            <th>Start time</th>
            <th>End time</th>
            <th></th>
          </tr>
          <working-time-item v-for="wt in filteredWorkingTimes" @delete="deleteWT" @edit="editWT" :key="wt.ID" :ID="wt.ID"
            :Employee="wt.Employee" :WtDate="wt.Date" :StartTime="wt.StartTime" :EndTime="wt.EndTime"></working-time-item>
        </table>
        <h3 v-else>
          There are no working times.
          <base-button @click="create" id="addLink">Add one</base-button>
        </h3>
      </base-card>
      <base-dialog :show="deleting" title="Do you want to delete selected working time?" :showClose="false">
        <div class="form-control" style="text-align: center">
          <base-button style="display: inline" @click="deleteWTnow">Delete</base-button>
          <base-button style="display: inline" @click="cancel">Cancel</base-button>
        </div>
      </base-dialog>
    </section>
  </div>
</template>

<script>
import WorkingTimeItem from "../../components/workingTimes/WorkingTimeItem.vue";
import { defineAsyncComponent } from "vue";
import WorkingTimeForm from "@/components/workingTimes/WorkingTimeForm.vue";
import AddWorkingTime from "../../pages/workingtimes/AddWorkingTime.vue";

export default {
  components: {
    WorkingTimeItem,
    WorkingTimeFilter: defineAsyncComponent(() =>
      import("../../components/workingTimes/WorkingTimeFilter.vue")
    ),
    WorkingTimeForm,
    AddWorkingTime,
  },
  data() {
    return {
      isLoading: false,
      deleting: false,
      editing: false,
      creating: false,
      wtForDel: null,
      wtForEdit: null,
      id: null,
      link: null,
      error: null,
      activeFilters: {
        pageNumber: 0,
        pageSize: 10,
      },
      show: false,
    };
  },
  computed: {
    hasWorkingTimes() {
      return (
        !this.isLoading && this.$store.getters["workingTimes/hasWorkingTimes"]
      );
    },
    addMultipleWTsLink() {
      const addWTsLink = window.location.pathname.split('/');
      return `/addmultiplewt/${addWTsLink[2]}${addWTsLink[3] ? '/' + addWTsLink[3] : ''}`;
    },
    hasWorkingTime() {
      return this.$store.getters["workingTimes/hasWorkingTime"];
    },
    workingTime() {
      return this.$store.getters["workingTimes/workingTime"];
    },
    filteredWorkingTimes() {
      return this.$store.getters["workingTimes/workingTimes"];
    },
  },
  watch: {
    activeFilters() {
      this.loadWorkingTimes();
    },
  },
  methods: {
    async close() {
      this.creating = false;
      await this.refresh();
    },
    catchError(error) {
      this.error = error
    },
    create() {
      this.creating = true;
    },
    cancelEditing() {
      this.editing = false;
    },
    async closeEditor() {
      this.editing = false;
      await this.loadWorkingTime();
      await this.refresh();
    },
    deleteWT(wtid) {
      this.deleting = true;
      this.wtForDel = wtid;
    },
    async editWT(wtid) {
      this.editing = true;
      this.wtForEdit = wtid;
      await this.loadWorkingTime();
    },
    async deleteWTnow() {
      try {
        await this.$store.dispatch("workingTimes/deleteWorkingTime", {
          id: this.wtForDel,
        });
        this.refresh();
      } catch (error) {
        this.error = error;
      }
      this.deleting = false;
    },
    cancel() {
      this.deleting = false;
    },
    setParams(updatedParams) {
      this.activeFilters = updatedParams;
    },
    async loadWorkingTimes() {
      this.isLoading = true;
      if (localStorage.getItem("empidwt")) {
        this.id = localStorage.getItem("empidwt");
        this.link = "workingtime/fromemployee/";
      } else if (localStorage.getItem("depidwt")) {
        this.id = localStorage.getItem("depidwt");
        this.link = "workingtime/fromdepartment/";
      } else if (localStorage.getItem("comidwt")) {
        this.id = localStorage.getItem("comidwt");
        this.link = "workingtime/fromcompany/";
      }
      try {
        await this.$store.dispatch("workingTimes/loadWorkingTimes", {
          id: this.id,
          link: this.link,
          pageNumber: this.activeFilters.pageNumber,
          pageSize: this.activeFilters.pageSize,
        });
        this.show = true;
      } catch (error) {
        this.error =
          `${error.message} in getting working times.` || "Something went wrong!";
      }
      this.isLoading = false;
    },
    async loadWorkingTime() {
      try {
        await this.$store.dispatch(
          "workingTimes/loadWorkingTime",
          this.wtForEdit
        );
      } catch (error) {
        this.error =
          `${error.message} in getting working time.` || "Something went wrong!";
      }
    },
    async refresh() {
      await this.loadWorkingTimes();
    },
    handleError() {
      this.error = null;
    },
  },
  async created() {
    await this.loadWorkingTimes();
  },
};
</script>

<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: space-between;
}

table {
  border-collapse: collapse;
  width: 100%;
  border-radius: 15px;
  margin: 20px 0;
}

th {
  text-align: left;
  border-bottom: 1px solid #ddd;
  padding: 15px 5px;
  margin: 15px 0;
}

tr:nth-child(even) {
  background-color: #f2f2f2;
  /* Light gray */
}

tr:nth-child(odd) {
  background-color: rgba(255, 0, 0, 0.801);
  /* Red */
  color: #fff;
  /* White text on red rows */
}

tr:nth-child(even):hover,
tr:nth-child(odd):hover {
  background-color: rgba(0, 0, 255, 0.5);
  /* Blue on hover */
  color: #fff;
  /* White text on blue rows on hover */
}
</style>
