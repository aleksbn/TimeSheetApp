<template>
  <base-card>
    <p>You are being logged out...</p>
  </base-card>
</template>

<script>
export default {
  methods: {
    async logout() {
      try {
        this.$store.dispatch("auth/logout");
      } catch (error) {
        alert(error);
      }
    },
  },
  async created() {
    await this.logout();
    this.$router.replace("/login");
  },
};
</script>
