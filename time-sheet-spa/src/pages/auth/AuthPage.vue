<template>
  <div>
    <base-card style="text-align: center">
      <base-button class="authButtons" link to="/register"
        >Register</base-button
      >
      <base-button class="authButtons" link to="/login">Login</base-button>
    </base-card>
  </div>
</template>

<style scoped>
.authButtons {
  display: block;
  padding: 25px 0;
  font-size: 2em;
  margin: 25px;
}
</style>
