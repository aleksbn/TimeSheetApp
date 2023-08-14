<template>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <header>
    <nav>
      <h2><router-link to="/about">Readme</router-link></h2>
      <input id="menu-toggle" type="checkbox" />
      <label class="menu-button-container" for="menu-toggle">
        <div class="menu-button"></div>
      </label>
      <ul class="menu">
        <li v-if="isLoggedIn"><router-link to="/companies">All Companies</router-link></li>
        <li v-if="hasComId && isLoggedIn">
          <router-link :to="depIdLink">All Departments</router-link>
        </li>
        <li v-if="hasComId && isLoggedIn">
          <router-link :to="empIdLink">All Employees</router-link>
        </li>
        <li v-if="!isLoggedIn">
          <router-link to="/auth">Login/Signup</router-link>
        </li>
        <li v-if="isLoggedIn">
          <router-link to="/logout">Logout</router-link>
        </li>
        <li v-if="isLoggedIn">
          <router-link to="/editprofile">Edit profile</router-link>
        </li>
      </ul>
    </nav>
  </header>
</template>

<script>
export default {
  mounted() {
    const menuToggleCheckbox = document.getElementById('menu-toggle');

    const menuItems = document.querySelectorAll('.menu li');
    menuItems.forEach((menuItem) => {
      menuItem.addEventListener('click', () => {
        menuToggleCheckbox.checked = false;
      });
    });
  },
  data() {
    return {
      comid: null,
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters["auth/isAuthenticated"];
    },
    hasComId() {
      return this.comid;
    },
    hasDepId() {
      return this.depid;
    },
    depIdLink() {
      return `/departments/${this.comid}`;
    },
    hasEmpId() {
      return this.empid;
    },
    empIdLink() {
      return `/employees/${this.comid}`;
    },
  },
  methods: {
    checkLocalStorage() {
      setInterval(() => {
        this.comid = localStorage.getItem("comid")
          ? localStorage.getItem("comid")
          : null;
      }, 200);
    },
    async logout() {
      await this.$store.dispatch("auth/logout");
      this.$router.replace("/auth");
    },
  },
  created() {
    this.checkLocalStorage();
  },
};
</script>

<style scoped>
header {
  width: 90%;
  height: 5rem;
  background-color: #c71616;
  display: flex;
  justify-content: center;
  border-radius: 0 0 15px 15px;
  margin-left: auto;
  margin-right: auto;
}

header a {
  text-decoration: none;
  color: #f391e3;
  display: inline-block;
  padding: 0.75rem 1.5rem;
  border: 1px solid transparent;
}

a:active,
a:hover,
a.router-link-active {
  border: 1px solid #f391e3;
}

h1 {
  margin: 0;
}

h1 a {
  color: white;
  margin: 0;
}

h1 a:hover,
h1 a:active,
h1 a.router-link-active {
  border-color: transparent;
  color: rgb(25, 186, 226);
}

header nav {
  width: 90%;
  margin: auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

header ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}

li {
  margin: 0 0.5rem;
}

h2 {
  vertical-align: center;
  text-align: center;
}

html,
body {
  margin: 0;
  height: 100%;
}

.top-nav {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  background-color: #00baf0;
  background: linear-gradient(to left, #f46b45, #eea849);
  /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
  color: #fff;
  height: 60px;
  padding: 1em;
}

.menu {
  display: flex;
  flex-direction: row;
  list-style-type: none;
  margin: 0;
  padding: 0;
}

.menu>li {
  margin: 0 1rem;
  overflow: hidden;
}

.menu-button-container {
  display: none;
  height: 100%;
  width: 30px;
  cursor: pointer;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

#menu-toggle {
  display: none;
}

.menu-button,
.menu-button::before,
.menu-button::after {
  display: block;
  background-color: #fff;
  position: absolute;
  height: 4px;
  width: 30px;
  transition: transform 400ms cubic-bezier(0.23, 1, 0.32, 1);
  border-radius: 2px;
}

.menu-button::before {
  content: "";
  margin-top: -8px;
}

.menu-button::after {
  content: "";
  margin-top: 8px;
}

#menu-toggle:checked+.menu-button-container .menu-button::before {
  margin-top: 0px;
  transform: rotate(405deg);
}

#menu-toggle:checked+.menu-button-container .menu-button {
  background: rgba(255, 255, 255, 0);
}

#menu-toggle:checked+.menu-button-container .menu-button::after {
  margin-top: 0px;
  transform: rotate(-405deg);
}

@media (max-width: 700px) {
  .menu-button-container {
    display: flex;
  }

  .menu {
    position: absolute;
    top: 0;
    margin-top: 50px;
    left: 0;
    flex-direction: column;
    width: 100%;
    justify-content: center;
    align-items: center;
  }

  #menu-toggle~.menu li {
    height: 0;
    margin: 0;
    padding: 0;
    border: 0;
    transition: height 400ms cubic-bezier(0.23, 1, 0.32, 1);
  }

  #menu-toggle:checked~.menu li {
    border: 1px solid #333;
    vertical-align: middle;
    height: 3em;
    padding: 0em;
    transition: height 400ms cubic-bezier(0.23, 1, 0.32, 1);
  }

  .menu>li {
    display: flex;
    justify-content: center;
    margin: 0;
    padding: 0.5em 0;
    width: 100%;
    color: white;
    background-color: #222;
  }

  .menu>li:not(:last-child) {
    border-bottom: 1px solid #444;
  }
}</style>
