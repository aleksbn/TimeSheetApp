<template>
  <div>
    <base-card>
      <form @submit.prevent="submitForm()">
        <div class="form-control" v-if="this.Mode === 'old'">
          <label for="ID">ID:</label>
          <input type="text" name="ID" disabled :value="this.ID" />
        </div>
        <div class="form-control" :class="{ invalid: !comName.isValid }">
          <label for="Name">Name:</label>
          <input
            type="text"
            name="Name"
            ref="comName"
            :disabled="!editMode"
            @blur="clearValidity('comName')"
          />
        </div>
        <div class="form-control" :class="{ invalid: !comAddress.isValid }">
          <label for="Address">Address:</label>
          <input
            type="text"
            name="Address"
            ref="comAddress"
            :disabled="!editMode"
            @blur="clearValidity('comAddress')"
          />
        </div>
        <div class="form-control" :class="{ invalid: !comCity.isValid }">
          <label for="City">City:</label>
          <input
            type="text"
            name="City"
            ref="comCity"
            :disabled="!editMode"
            @blur="clearValidity('comCity')"
          />
        </div>
        <div class="form-control" :class="{ invalid: !comCountry.isValid }">
          <label for="Country">Country:</label>
          <input
            type="text"
            name="Country"
            ref="comCountry"
            :disabled="!editMode"
            @blur="clearValidity('comCountry')"
          />
        </div>
        <div class="form-control" :class="{ invalid: !comEmail.isValid }">
          <label for="cEmail">Email:</label>
          <input
            type="text"
            name="Email"
            ref="comEmail"
            :disabled="!editMode"
            @blur="clearValidity('comEmail')"
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
            :to="'/departments/' + this.ID"
            >Departments</base-button
          >
          <base-button
            v-if="this.ID"
            @click="openEmployees()"
            style="display: inline"
            >Employees</base-button
          >
          <base-button
            v-if="this.ID"
            @click="openWorkingTimes()"
            style="display: inline"
            >Working times</base-button
          >
          <base-button
            v-if="this.ID"
            :type="'button'"
            @click="deleteCompany()"
            style="display: inline"
            >Delete this company</base-button
          >
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  props: ["ID", "Name", "Address", "City", "Country", "Email", "Mode"],
  emits: ["save-data"],
  data() {
    return {
      editModeType: true,
      formIsValid: true,
      comName: {
        isValid: true,
        val: "",
      },
      comAddress: {
        isValid: true,
        val: "",
      },
      comCity: {
        isValid: true,
        val: "",
      },
      comCountry: {
        isValid: true,
        val: "",
      },
      comEmail: {
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
      const regexExp =
        /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gi;
      this.formIsValid = true;
      if (this.comName.val === "") {
        this.comName.isValid = false;
        this.formIsValid = false;
      }
      if (this.comAddress.val === "") {
        this.comAddress.isValid = false;
        this.formIsValid = false;
      }
      if (this.comCity.val === "") {
        this.comCity.isValid = false;
        this.formIsValid = false;
      }
      if (this.comCountry.val === "") {
        this.comCountry.isValid = false;
        this.formIsValid = false;
      }
      if (this.comEmail.val === "" || !regexExp.test(this.comEmail.val)) {
        this.comEmail.isValid = false;
        this.formIsValid = false;
      }
    },

    submitForm() {
      if (!this.editModeType) {
        this.editModeType = true;
        return;
      }

      this.comName.val = this.$refs.comName.value.trim();
      this.comAddress.val = this.$refs.comAddress.value.trim();
      this.comCity.val = this.$refs.comCity.value.trim();
      this.comCountry.val = this.$refs.comCountry.value.trim();
      this.comEmail.val = this.$refs.comEmail.value.trim();

      this.validateForm();

      if (!this.formIsValid) {
        return;
      }

      var formData = {
        comName: this.comName.val,
        comAddress: this.comAddress.val,
        comCity: this.comCity.val,
        comCountry: this.comCountry.val,
        comEmail: this.comEmail.val,
      };

      if(this.Mode === "old") {
        formData = {
          ...formData,
          comId: this.ID
        };
      }

      this.$emit("save-data", formData);
    },
    deleteCompany() {
      alert('About to be done');
    },
    openEmployees() {
      localStorage.removeItem("depid");
      this.$router.push("/employees/" + this.ID);
    },
    openWorkingTimes() {
      localStorage.removeItem("depidwt");
      localStorage.removeItem("empidwt");
      this.$router.push("/workingtimes/" + this.ID);
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
  mounted() {
    this.editModeType = this.Mode === "new" ? true : false;
    if (this.Mode === "old") {
      this.$refs.comAddress.value = this.Address;
      this.$refs.comCity.value = this.City;
      this.$refs.comName.value = this.Name;
      this.$refs.comCountry.value = this.Country;
      this.$refs.comEmail.value = this.Email;
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
