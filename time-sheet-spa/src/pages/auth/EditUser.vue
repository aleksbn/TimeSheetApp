<template>
  <div>
    <base-card>
      <user-form v-if="hasCurrentUser" :Mode="'old'" :key="currentUser.Email" :FirstName="currentUser.FirstName"
        :LastName="currentUser.LastName" :Email="currentUser.Email"></user-form>
      <p v-else>There's something wrong with loading of the user data.</p>
    </base-card>
  </div>
</template>

<script>
import UserForm from "../../components/auth/UserForm.vue";

export default {
  components: {
    UserForm,
  },
  methods: {
    async loadUser() {
      await this.$store.dispatch("auth/getUser");
    },
  },
  computed: {
    currentUser() {
      return this.$store.getters["auth/currentUser"];
    },
    hasCurrentUser() {
      return this.$store.getters["auth/hasCurrentUser"];
    },
  },
  async created() {
    await this.loadUser();
  },
};
</script>
