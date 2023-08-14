<template>
  <div>
    <form @submit.prevent="submitForm()">
      <div class="form-control" v-if="this.Mode === 'old'">
        <label for="ID">ID</label>
        <input type="text" name="ID" disabled :value="this.ID" />
      </div>
      <div class="form-control">
        <label for="Name">Name</label>
        <input type="text" name="Name" disabled :value="fullName" />
      </div>
      <div class="form-control" :class="{ invalid: !wtDate.isValid }">
        <label for="wtDate">Date</label>
        <input type="text" name="wtDate" ref="wtDate" @blur="clearValidity('wtDate')" />
      </div>
      <div class="form-control" :class="{ invalid: !wtStartTime.isValid }">
        <label for="StartTime">Start time</label>
        <input type="time" name="StartTime" ref="wtStartTime" @blur="clearValidity('wtStartTime')" />
      </div>
      <div class="form-control" :class="{ invalid: !wtEndTime.isValid }">
        <label for="EndTime">End time</label>
        <input type="time" name="EndTime" ref="wtEndTime" @blur="clearValidity('wtEndTime')" />
      </div>
      <p v-if="!formIsValid">Please, fix the above errors and submit again.</p>
      <base-button style="display: inline">Save</base-button>
      <base-button style="display: inline" :type="'button'" @click="cancel">Cancel</base-button>
    </form>
  </div>
</template>

<script>
import moment from "moment";

export default {
  props: ["ID", "Employee", "WtDate", "StartTime", "EndTime", "Mode"],
  emits: ["close", "cancel", "catchError"],
  methods: {
    cancel() {
      this.$emit("cancel");
    },
    async submitForm() {
      try {
        this.wtDate.val = this.$refs.wtDate.value;
        this.wtStartTime.val = this.$refs.wtStartTime.value;
        this.wtEndTime.val = this.$refs.wtEndTime.value;

        this.validateForm();

        if (!this.formIsValid) {
          return;
        }

        const formData = {
          wtDate: this.wtDate.val,
          wtStartTime: this.wtStartTime.val,
          wtEndTime: this.wtEndTime.val,
          wtEmployeeId: this.Employee.id,
        };

        if (this.Mode === "old") {
          formData.wtId = this.ID;
        }

        await this.$store.dispatch("workingTimes/editWorkingTime", formData);
        this.$emit("close");
      } catch (error) {
        this.$emit("cancel");
        this.$emit("catchError", error);
      }
    },
    clearValidity(input) {
      this[input].isValid = true;
    },
    validateForm() {
      this.formIsValid = true;
      if (
        this.wtDate.val === "" ||
        !moment(this.wtDate.val, "DD.MM.YYYY", true).isValid()
      ) {
        this.wtDate.isValid = false;
        this.formIsValid = false;
      }
      if (!this.wtStartTime.val.match(/\d+:\d+/)) {
        this.wtStartTime.isValid = false;
        this.formIsValid = false;
      }
      if (!this.wtEndTime.val.match(/\d+:\d+/)) {
        this.wtEndTime.isValid = false;
        this.formIsValid = false;
      }
    },
  },
  data() {
    return {
      formIsValid: true,
      wtDate: {
        isValid: true,
        val: "",
      },
      wtEndTime: {
        isValid: true,
        val: "",
      },
      wtStartTime: {
        isValid: true,
        val: "",
      },
    };
  },
  computed: {
    fullName() {
      return `${this.Employee.firstName} ${this.Employee.lastName}`;
    },
  },
  mounted() {
    if (this.Mode === "old") {
      this.$refs.wtDate.value = moment(this.WtDate.substring(0, 10)).format(
        "DD.MM.YYYY"
      );
      this.$refs.wtStartTime.value = this.StartTime;
      this.$refs.wtEndTime.value = this.EndTime;
    }
  },
};
</script>

<style scoped>
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input,
select {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
  font-size: 1em;
  border-radius: 15px;
  height: 40px;
}

input:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}

.invalid label {
  color: red;
}

.invalid input,
.invalid select {
  border: 1px solid red;
}

p {
  font-weight: bold;
  font-size: large;
}
</style>
