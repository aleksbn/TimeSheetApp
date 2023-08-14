<template>
  <div>
    <section>
      <base-card>
        <h2>Page not found!</h2>
        <p>
          The page you requested cannot be found.<br /><br /> Try
          <router-link to="/auth">logging in</router-link> or browsing your
          <router-link to="/companies">current companies</router-link>.
        </p>
      </base-card>
    </section>
  </div>
</template>
