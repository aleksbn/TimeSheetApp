<template>
  <div>
    <base-dialog :show="!!error" title="An error occured" :showClose="true" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <keep-alive>
      <form @submit.prevent="submitForm()">
        <div class="form-control" :class="{ invalid: !uFirstName.isValid }">
          <label for="FirstName">First name</label>
          <input type="text" name="FirstName" id="FirstName" ref="FirstName" @blur="clearValidity('uFirstName')" />
        </div>
        <p class="invalid" v-if="!uFirstName.isValid">
          First name cannot be empty, must contain only letters, but it is allowed to use dashes (like in Marc-Anthony),
          or
          single quotes (like in O'Neal).
        </p>
        <div class="form-control" :class="{ invalid: !uLastName.isValid }">
          <label for="LastName">Last name</label>
          <input type="text" name="LastName" id="LastName" ref="LastName" @blur="clearValidity('uLastName')" />
        </div>
        <p class="invalid" v-if="!uLastName.isValid">
          Last name cannot be empty, must contain only letters, but it is allowed to use dashes (like in Schwartz-Muller),
          or single quotes (like in O'Brian).
        </p>
        <div class="form-control" :class="{ invalid: !uEmail.isValid }">
          <label for="Email" v-if="this.Mode === 'new'">Email</label>
          <label for="Email" v-if="this.Mode === 'old'">Old email</label>
          <input type="email" name="Email" id="Email" ref="Email" @blur="clearValidity('uEmail')" />
        </div>
        <div class="form-control" :class="{ invalid: !uEmailConfirm.isValid }">
          <label for="EmailConfirm" v-if="this.Mode === 'new'">Email confirmation</label>
          <label for="EmailConfirm" v-if="this.Mode === 'old'">New email</label>
          <input type="email" name="EmailConfirm" id="EmailConfirm" ref="EmailConfirm"
            @blur="clearValidity('uEmailConfirm')" />
        </div>
        <p class="invalid" v-if="this.Mode === 'new' && (!uEmail.isValid || !uEmailConfirm.isValid)
          ">
          Email and its corresponding confirmation field must match and be valid email addresses.
        </p>
        <p class="invalid" v-if="this.Mode === 'old' && (!uEmail.isValid || !uEmailConfirm.isValid)
          ">
          Both old and new emails must be valid email addresses.
        </p>
        <div class="form-control" :class="{ invalid: !uPassword.isValid }">
          <label for="Password" v-if="this.Mode === 'new'">Password</label>
          <label for="Password" v-if="this.Mode === 'old'">Old password</label>
          <input type="password" name="Password" id="Password" ref="Password" @blur="clearValidity('uPassword')" />
        </div>
        <div class="form-control" :class="{ invalid: !uPasswordConfirm.isValid }">
          <label for="PasswordConfirm" v-if="this.Mode === 'new'">Password confirmation</label>
          <label for="PasswordConfirm" v-if="this.Mode === 'old'">New password</label>
          <input type="password" name="PasswordConfirm" id="PasswordConfirm" ref="PasswordConfirm"
            @blur="clearValidity('uPasswordConfirm')" />
        </div>
        <p class="invalid" v-if="this.Mode === 'new' &&
          (!uPassword.isValid || !uPasswordConfirm.isValid)
          ">
          Password and its corresponding confirmation field must match and be at least 8 characters long, containing
          letters, numbers and at least one special sign.
        </p>
        <p class="invalid" v-if="this.Mode === 'old' && uPassword.val.trim() === ''
          ">
          You must type in old password for the sake of verification.
        </p>
        <p class="invalid" v-else-if="this.Mode === 'old' &&
          (!uPassword.isValid || !uPasswordConfirm.isValid)
          ">
          Both old and new passwords must be at least 8 characters long, containing letters, numbers and special signs.
        </p>
        <p v-if="!formIsValid">Please, fix the above errors and submit again.</p>
        <div class="form-control">
          <base-button style="display: inline">Save</base-button>
          <base-button :type="'button'" style="display: inline" @click="deleteUser"
            v-if="this.Mode === 'old'">Delete</base-button>
        </div>
      </form>
    </keep-alive>
  </div>
</template>

<script>
import DataCheckers from '@/data-checkers';
export default {
  props: ["Mode", "FirstName", "LastName", "Email"],
  data() {
    return {
      error: null,
      isLoading: false,
      formIsValid: true,
      uFirstName: {
        isValid: true,
        val: "",
      },
      uLastName: {
        isValid: true,
        val: "",
      },
      uEmail: {
        isValid: true,
        val: "",
      },
      uEmailConfirm: {
        isValid: true,
        val: "",
      },
      uPassword: {
        isValid: true,
        val: "",
      },
      uPasswordConfirm: {
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
    deleteUser() {
      alert("to be done");
    },
    async submitForm() {
      this.isLoading = true;
      try {
        this.uFirstName.val = this.$refs.FirstName.value;
        this.uLastName.val = this.$refs.LastName.value;
        this.uEmail.val = this.$refs.Email.value;
        this.uEmailConfirm.val = this.$refs.EmailConfirm.value;
        this.uPassword.val = this.$refs.Password.value;
        this.uPasswordConfirm.val = this.$refs.PasswordConfirm.value;

        this.validateForm();

        if (!this.formIsValid) {
          this.isLoading = false;
          return;
        }

        if (this.Mode === "old") {
          const formData = {
            Id: this.ID,
            FirstName: this.uFirstName.val,
            LastName: this.uLastName.val,
            OldEmail: this.uEmail.val,
            NewEmail: this.uEmailConfirm.val,
            NewEmailConfirmation: this.uEmailConfirm.val,
            OldPassword: this.uPassword.val,
            NewPassword: this.uPasswordConfirm.val,
            PasswordConfirmation: this.uPasswordConfirm.val,
          };
          await this.$store.dispatch("auth/editUser", formData);
          this.$router.push("/companies");
        } else {
          const formData = {
            FirstName: this.uFirstName.val,
            LastName: this.uLastName.val,
            Email: this.uEmail.val,
            Password: this.uPassword.val,
            Role: "COMPANY MANAGER",
          };
          await this.$store.dispatch("auth/register", formData);
          this.$router.push("/login");
        }
      } catch (error) {
        this.error = error;
      }
      this.isLoading = false;
    },
    validateForm() {
      this.formIsValid = true;
      if (!DataCheckers.checkPersonName(this.uFirstName.val)) {
        this.uFirstName.isValid = false;
        this.formIsValid = false;
      }
      if (!DataCheckers.checkPersonName(this.uLastName.val)) {
        this.uLastName.isValid = false;
        this.formIsValid = false;
      }

      if (this.Mode === "new") {
        if (
          !DataCheckers.checkEmail(this.uEmail.val) ||
          this.uEmail.val !== this.uEmailConfirm.val
        ) {
          this.uEmail.isValid = false;
          this.uEmailConfirm.isValid = false;
          this.formIsValid = false;
        }
      } else {
        if (!DataCheckers.checkEmail(this.uEmail.val)) {
          this.uEmail.isValid = false;
          this.formIsValid = false;
        }
        if (this.uEmailConfirm.val !== "") {
          if (!DataCheckers.checkEmail(this.uEmailConfirm.val)) {
            this.uEmailConfirm.isValid = false;
            this.formIsValid = false;
          }
        }
      }

      if (this.Mode === "new") {
        if (
          !DataCheckers.checkPassword(this.uPassword.val) ||
          this.uPassword.val !== this.uPasswordConfirm.val
        ) {
          this.uPassword.isValid = false;
          this.uPasswordConfirm.isValid = false;
          this.formIsValid = false;
        }
      } else {
        if (!DataCheckers.checkPassword(this.uPassword.val)) {
          this.uPassword.isValid = false;
          this.formIsValid = false;
        }
        if (this.uPasswordConfirm.val !== "") {
          if (!DataCheckers.checkPassword(this.uPasswordConfirm.val)) {
            this.uPasswordConfirm.isValid = false;
            this.formIsValid = false;
          }
        }
      }
    },
  },
  mounted() {
    if (this.Mode === "old") {
      this.$refs.FirstName.value = this.FirstName;
      this.$refs.LastName.value = this.LastName;
      this.$refs.Email.value = this.Email;
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

.invalid p {
  color: red;
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
