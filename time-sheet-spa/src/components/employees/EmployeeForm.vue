<template>
  <div>
    <base-card>
      <form @submit.prevent="submitForm()">
        <div class="form-control" v-if="this.Mode === 'old'">
          <label for="ID">ID:</label>
          <input type="text" name="ID" disabled :value="this.ID" />
        </div>
        <div class="form-control" :class="{ invalid: !empFirstName.isValid }">
          <label for="Firstame">First name:</label>
          <input type="text" name="FirstName" :disabled="!editMode" ref="empFirstName"
            @blur="clearValidity('empFirstName')" />
        </div>
        <p class="invalid" v-if="!empFirstName.isValid">
          First name cannot be empty, must contain only letters, but it is allowed to use dashes (like in Marc-Anthony),
          or single quotes (like in O'Neal).
        </p>
        <div class="form-control" :class="{ invalid: !empLastName.isValid }">
          <label for="LastName">Last name:</label>
          <input type="text" name="LastName" :disabled="!editMode" ref="empLastName"
            @blur="clearValidity('empLastName')" />
        </div>
        <p class="invalid" v-if="!empLastName.isValid">
          Last name cannot be empty, must contain only letters, but it is allowed to use dashes (like in Schwartz-Muller),
          or single quotes (like in O'Brian).
        </p>
        <div class="form-control" :class="{ invalid: !empJobTitle.isValid }">
          <label for="JobTitle">Job title:</label>
          <input type="text" name="JobTitle" :disabled="!editMode" ref="empJobTitle"
            @blur="clearValidity('empJobTitle')" />
        </div>
        <p class="invalid" v-if="!empJobTitle.isValid">
          Job title cannot be empty, must contain only letters, but it is allowed to use dashes or single quotes.
        </p>
        <div class="form-control" :class="{ invalid: !empDegree.isValid }">
          <label for="Degree">Degree:</label>
          <input type="text" name="Degree" :disabled="!editMode" ref="empDegree" @blur="clearValidity('empDegree')" />
        </div>
        <p class="invalid" v-if="!empDegree.isValid">
          Degree cannot be empty, must contain only letters, but it is allowed to use dashes or single quotes.
        </p>
        <div class="form-control" :class="{ invalid: !empDepartmentId.isValid }">
          <label for="DepartmentId">Department:</label>
          <select name="DepartmentId" id="DepartmentId" :disabled="!editMode" ref="empDepartmentId"
            @blur="clearValidity('empDepartmentId')">
            <option v-for="dep in departments" :key="dep.ID" :value="dep.ID" :selected="depId === dep.ID ? true : false">
              {{ dep.Name }}
            </option>
          </select>
        </div>
        <div class="form-control" :class="{ invalid: !empEmail.isValid }">
          <label for="Email">Email:</label>
          <input type="text" name="Email" :disabled="!editMode" ref="empEmail" @blur="clearValidity('empEmail')" />
        </div>
        <div class="form-control" :class="{ invalid: !empAddress.isValid }">
          <label for="Address">Address:</label>
          <input type="text" name="Address" :disabled="!editMode" ref="empAddress" @blur="clearValidity('empAddress')" />
        </div>
        <p class="invalid" v-if="this.Mode === 'new' && !empEmail.isValid">
          Email and its corresponding confirmation field must match and be valid email addresses.
        </p>
        <p class="invalid" v-if="this.Mode === 'old' && !empEmail.isValid">
          Both old and new emails must be valid email addresses.
        </p>
        <div class="form-control" :class="{ invalid: !empPhone.isValid }">
          <label for="Phone">Phone:</label>
          <input type="text" name="Phone" :disabled="!editMode" ref="empPhone" @blur="clearValidity('empPhone')" />
        </div>
        <p class="invalid" v-if="!empPhone.isValid">
          Phone number must be in +999999999 or 099999999 format.
        </p>
        <div class="form-control" :class="{ invalid: !empDateOfBirth.isValid }">
          <label for="DateOfBirth">Date of birth:</label>
          <input type="text" name="DateOfBirth" :disabled="!editMode" ref="empDateOfBirth"
            @blur="clearValidity('empDateOfBirth')" />
        </div>
        <p v-if="!empDateOfBirth.isValid">
          The only available format is DD.MM.YYY! You must use that one!
        </p>
        <div class="form-control" :class="{ invalid: !empStartOfEmployment.isValid }">
          <label for="StartOfEmployment">Start of employment:</label>
          <input type="text" name="StartOfEmployment" :disabled="!editMode" ref="empStartOfEmployment"
            @blur="clearValidity('empStartOfEmployment')" />
        </div>
        <p v-if="!empStartOfEmployment.isValid">
          The only available format is DD.MM.YYY! You must use that one!
        </p>
        <div class="form-control" :class="{ invalid: !empHourlyRate.isValid }">
          <label for="HourlyRate">Hourly rate ($):</label>
          <input type="number" step="0.01" name="HourlyRate" :disabled="!editMode" ref="empHourlyRate"
            @blur="clearValidity('empHourlyRate')" />
        </div>
        <p v-if="!formIsValid">
          Please, fix the above errors and submit again.
        </p>
        <div>
          <base-button style="display: inline">{{ textForMode }}</base-button>
          <base-button v-if="this.ID" @click="openWorkingTimes()" style="display: inline"
            :to="'/workingtimes/' + this.ID">Working times</base-button>
          <base-button :type="'button'" @click="deleteEmployee" style="display: inline">Delete this employee</base-button>
        </div>
      </form>
    </base-card>
  </div>
</template>

<script>
import moment from "moment";
import DataCheckers from "@/data-checkers";
export default {
  emits: ["save-data", "delete-employee"],
  props: [
    "ID",
    "FirstName",
    "LastName",
    "Degree",
    "JobTitle",
    "HourlyRate",
    "Department",
    "DepartmentId",
    "Email",
    "Address",
    "Phone",
    "DateOfBirth",
    "StartOfEmployment",
    "Mode",
  ],
  data() {
    return {
      editModeType: true,
      formIsValid: true,
      depid: null,
      empFirstName: {
        isValid: true,
        val: "",
      },
      empLastName: {
        isValid: true,
        val: "",
      },
      empDegree: {
        isValid: true,
        val: "",
      },
      empJobTitle: {
        isValid: true,
        val: "",
      },
      empHourlyRate: {
        isValid: true,
        val: 0,
      },
      empDepartmentId: {
        isValid: true,
        val: "",
      },
      empEmail: {
        isValid: true,
        val: "",
      },
      empAddress: {
        isValid: true,
        val: "",
      },
      empPhone: {
        isValid: true,
        val: "",
      },
      empDateOfBirth: {
        isValid: true,
        val: "",
      },
      empStartOfEmployment: {
        isValid: true,
        val: "",
      },
    };
  },
  methods: {
    handleError() {
      this.error = null;
    },
    clearValidity(input) {
      this[input].isValid = true;
    },
    submitForm() {
      if (!this.editModeType) {
        this.editModeType = true;
        return;
      }

      this.empAddress.val = this.$refs.empAddress.value.trim();
      this.empDateOfBirth.val = this.$refs.empDateOfBirth.value;
      this.empDegree.val = this.$refs.empDegree.value.trim();
      this.empDepartmentId.val = this.$refs.empDepartmentId.value;
      this.empEmail.val = this.$refs.empEmail.value.trim();
      this.empFirstName.val = this.$refs.empFirstName.value.trim();
      this.empHourlyRate.val = this.$refs.empHourlyRate.value;
      this.empJobTitle.val = this.$refs.empJobTitle.value.trim();
      this.empLastName.val = this.$refs.empLastName.value.trim();
      this.empPhone.val = this.$refs.empPhone.value.trim();
      this.empStartOfEmployment.val = this.$refs.empStartOfEmployment.value;

      this.validateForm();

      if (!this.formIsValid) {
        return;
      }
      var formData = {
        empAddress: this.empAddress.val,
        empEmail: this.empEmail.val,
        empDateOfBirth: new Date(this.empDateOfBirth.val),
        empDegree: this.empDegree.val,
        empDepartmentId: this.empDepartmentId.val,
        empFirstName: this.empFirstName.val,
        empHourlyRate: parseFloat(this.empHourlyRate.val),
        empJobTitle: this.empJobTitle.val,
        empLastName: this.empLastName.val,
        empPhone: this.empPhone.val,
        empStartOfEmployment: new Date(this.empStartOfEmployment.val),
      };

      if (this.Mode === "old") {
        formData = {
          ...formData,
          empId: this.ID,
        };
      }

      this.$emit("save-data", formData);
    },
    validateForm() {
      this.formIsValid = true;
      if (!DataCheckers.checkPersonName(this.empFirstName.val)) {
        this.empFirstName.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkPersonName(this.empLastName.val)) {
        this.empLastName.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkPersonName(this.empJobTitle.val)) {
        this.empJobTitle.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkEmail(this.empEmail.val)) {
        this.empEmail.isValid = false;
        this.formIsValid = false;
      }
      if (this.empAddress.val.length < 5) {
        this.empAddress.isValid = false;
        this.formIsValid = false;
      }
      if (
        this.empDateOfBirth.val === "" ||
        !DataCheckers.checkDate(this.empDateOfBirth.val)
      ) {
        this.empDateOfBirth.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkDegree(this.empDegree.val)) {
        this.empDegree.isValid = false;
        this.formIsValid = false;
      }
      if (
        isNaN(parseFloat(this.empHourlyRate.val)) ||
        parseFloat(this.empHourlyRate.val) <= 0
      ) {
        this.empHourlyRate.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkPhone(this.empPhone.val)) {
        this.empPhone.isValid = false;
        this.formIsValid = false;
      }
      if (
        this.empStartOfEmployment.val === "" ||
        !DataCheckers.checkDate(this.empStartOfEmployment.val)
      ) {
        this.empStartOfEmployment.isValid = false;
        this.formIsValid = false;
      }
    },
    async loadDepartments() {
      try {
        await this.$store.dispatch("departments/loadDepartments", {
          comid: localStorage.getItem("comid"),
        });
      } catch (error) {
        this.error =
          error.message + " in getting departments" || "Something went wrong!";
      }
    },
    openWorkingTimes() {
      localStorage.removeItem("comidwt");
      localStorage.removeItem("depidwt");
      this.$router.push("/workingtimes/" + this.empid);
    },
    deleteEmployee() {
      this.$emit("delete-employee", this.ID);
    },
  },
  computed: {
    editMode() {
      return this.editModeType;
    },
    textForMode() {
      return this.editModeType === true ? "Save" : "Edit";
    },
    departments() {
      return this.$store.getters["departments/departments"];
    },
    depId() {
      return this.DepartmentId;
    },
  },
  async created() {
    await this.loadDepartments();
    this.comid = localStorage.getItem("comid");
    this.empid = localStorage.getItem("empid");
    this.depid = localStorage.getItem("depid");
  },
  mounted() {
    this.editModeType = this.Mode === "new" ? true : false;
    if (this.Mode === "old") {
      this.$refs.empAddress.value = this.Address;
      this.$refs.empDateOfBirth.value = moment(this.DateOfBirth).format(
        "DD.MM.YYYY"
      );
      this.$refs.empDegree.value = this.Degree;
      this.$refs.empDepartmentId.value = this.DepartmentId;
      this.$refs.empEmail.value = this.Email;
      this.$refs.empFirstName.value = this.FirstName;
      this.$refs.empHourlyRate.value = this.HourlyRate;
      this.$refs.empJobTitle.value = this.JobTitle;
      this.$refs.empLastName.value = this.LastName;
      this.$refs.empPhone.value = this.Phone;
      this.$refs.empStartOfEmployment.value = moment(
        this.StartOfEmployment
      ).format("DD.MM.YYYY");
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

.invalid input {
  border: 1px solid red;
}

p {
  font-weight: bold;
  font-size: large;
}
</style>
