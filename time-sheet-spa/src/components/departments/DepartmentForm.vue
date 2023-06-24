<template>
  <div>
    <base-card>
      <form @submit.prevent="submitForm()">
        <div class="form-control" v-if="this.Mode === 'old'">
          <label for="ID">ID:</label>
          <input type="text" name="ID" disabled :value="this.ID" />
        </div>
        <div class="form-control" :class="{ invalid: !depName.isValid }">
          <label for="Name">Name:</label>
          <input
            type="text"
            name="Name"
            :disabled="!editMode"
            ref="depName"
            @blur="clearValidity('depName')"
          />
        </div>
        <p v-if="!formIsValid">
          Please, fix the above errors and submit again.
        </p>
        <div>
          <base-button style="display: inline">{{ textForMode }}</base-button>
          <base-button
            v-if="this.ID"
            link
            style="display: inline"
            :to="'/employees/' + this.comid + '/' + this.ID"
            >Employees</base-button
          >
          <base-button
            v-if="this.ID"
            @click="openWorkingTimes()"
            style="display: inline"
            :to="'/workingtimes/' + this.ID"
            >Working times</base-button
          >
          <base-button
            v-if="this.ID"
            :type="'button'"
            @click="deleteDepartment()"
            style="display: inline"
            >Delete this department</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: ["ID", "Name", "Mode"],
  emits: ["save-data", "delete-department"],
  data() {
    return {
      editModeType: true,
      formIsValid: true,
      comid: null,
      depName: {
        isValid: true,
        val: "",
      },
    };
  },
  methods: {
    clearValidity(input) {
      this[input].isValid = true;
    },
    validateForm() {
      this.formIsValid = true;
      if (this.depName.val === "") {
        this.depName.isValid = false;
        this.formIsValid = false;
      }
    },
    submitForm() {
      if (!this.editModeType) {
        this.editModeType = true;
        return;
      }

      this.depName.val = this.$refs.depName.value.trim();

      this.validateForm();

      if (!this.formIsValid) {
        return;
      }

      var formData = {
        depName: this.depName.val,
        comId: localStorage.getItem('comid')
      };

      if (this.Mode === "old") {
        formData = {
          ...formData,
          depId: this.ID,
        };
      }

      this.$emit("save-data", formData);
    },
    openWorkingTimes() {
      localStorage.removeItem("comidwt");
      localStorage.removeItem("empidwt");
      this.$router.push("/workingtimes/" + this.depid);
    },

    deleteDepartment() {
      this.$emit("delete-department", this.ID);
    },
  },
  computed: {
    editMode() {
      return this.editModeType;
    },
    textForMode() {
      return this.editModeType === true ? "Save" : "Edit";
    },
  },
  created() {
    this.comid = localStorage.getItem('comid');
  },
  mounted() {
    this.editModeType = this.Mode === "new" ? true : false;
    if (this.Mode === "old") {
      this.$refs.depName.value = this.Name;
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

input {
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

.invalid input {
  border: 1px solid red;
}
</style>
