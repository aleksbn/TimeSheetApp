<template>
  <div class="card">
    <slot> </slot>
  </div>
</template>

<style scoped>
.card {
  border-radius: 12px;
  box-shadow: 0 5px 20px rgb(0, 0, 0);
  padding: 1rem;
  margin: 2rem auto;
  max-width: 70%;
}
</style>
