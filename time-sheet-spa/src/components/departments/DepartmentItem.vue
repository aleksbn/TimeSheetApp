<template>
  <li>
    <h2>{{ ID }} <span>(unique department ID)</span></h2>
    <h2>{{ Name }}</h2>
    <div class="actions">
      <base-button link :to="detailsLink">Details</base-button>
    </div>
  </li>
</template>

<script>
export default {
  props: ["ID", "Name"],
  computed: {
    detailsLink() {
      return `${this.$route.path}/${this.ID}`;
    },
  },
};
</script>

<style scoped>
li {
  margin: 1rem 0;
  border: 1px solid #424242;
  border-radius: 12px;
  padding: 1rem;
}

span {
  font-size: .7em;
}

h2 {
  font-size: 2rem;
}

h2,
h3 {
  margin: 0.5rem 0;
}

div {
  margin: 0.5rem 0;
}

.actions {
  display: flex;
  justify-content: flex-end;
}
</style>
