<template>
  <the-header></the-header>
  <router-view v-slot="slotProps">
    <transition name="route" mode="out-in">
      <component :is="slotProps.Component"></component>
    </transition>
  </router-view>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
export default {
  components: {
    TheHeader,
  },
  async created() {
    if (this.checkStorage()) {
      await this.$store.dispatch("auth/setUserData", {
        id: localStorage.getItem("userId"),
        token: localStorage.getItem("token"),
        refreshToken: localStorage.getItem("refreshToken"),
        expiresAt: localStorage.getItem("expiresAt")
      })
    } else {
      this.clearStorageRemains();
    }
  },
  methods: {
    checkStorage() {
      return !!localStorage.getItem("userId") && !!localStorage.getItem("token") && !!localStorage.getItem("refreshToken") && !!localStorage.getItem("expiresAt");
    },
    clearStorageRemains() {
      localStorage.removeItem('userId');
      localStorage.removeItem('token');
      localStorage.removeItem('refreshToken');
      localStorage.removeItem('expiresAt');
    }
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap");

html {
  font-family: "Roboto", sans-serif;
}

body {
  background-color: cyan;
  margin: 0;
}

body header {
  text-align: center;
}

.route-enter-from {
  opacity: 0;
  transform: translateY(-30px);
}

.route-leave-to {
  opacity: 0;
  transform: translateY(30px);
}

.route-enter-active {
  transition: all 0.3s ease-out;
}

.route-leave-active {
  transition: all 0.3s ease-in;
}

.route-enter-to,
.route-leave-from {
  opacity: 1;
  transform: translateY(0);
}
</style>
