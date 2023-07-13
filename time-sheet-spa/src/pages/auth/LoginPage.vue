<template>
  <div>
    <base-dialog
      :show="!!error"
      title="An error occured"
      :showClose="true"
      @close="handleError"
    >
      <p>{{ error }}</p>
    </base-dialog>
    <div v-if="isLoading">
      <base-spinner></base-spinner>
    </div>
    <base-card>
      <form @submit.prevent="submitData">
        <div class="form-control">
          <label for="email">Email</label>
          <input type="text" id="email" name="email" v-model="email.val" />
        </div>
        <div class="form-control">
          <label for="username">Password</label>
          <input
            type="password"
            id="password"
            name="password"
            v-model="password.val"
          />
        </div>
        <base-button>Login</base-button>
      </form>
    </base-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      error: null,
      isLoading: false,
      formIsValid: true,
      email: {
        isValid: true,
        val: "",
      },
      password: {
        isValid: true,
        val: "",
      },
    };
  },
  methods: {
    handleError() {
      this.error = null;
    },
    async submitData() {
      this.validateForm();

      if (!this.formIsValid) {
        this.isLoading = false;
        return;
      }

      try {
        this.isLoading = true;
        await this.$store.dispatch("auth/login", {
          Email: this.email.val,
          Password: this.password.val,
        });
        this.$router.push("/companies");
      } catch (error) {
        this.error = error.message + " in logging in." || "Something went wrong!";
      }
      this.isLoading = false;
    },
    validateForm() {
      this.formIsValid = true;
      if (this.email === "") {
        this.email.isValid = false;
        this.formIsValid = false;
      }
      if (this.password === "") {
        this.password.isValid = false;
        this.formIsValid = false;
      }
    },
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
p {
  font-weight: bold;
  font-size: large;
}
</style>
