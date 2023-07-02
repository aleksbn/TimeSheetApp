<template>
  <div>
    <base-card>
      <user-form :Mode="'new'"></user-form>
    </base-card>
  </div>
</template>

<script>
import UserForm from "../../components/auth/UserForm.vue";

export default {
  components: {
    UserForm,
  },
};
</script>
