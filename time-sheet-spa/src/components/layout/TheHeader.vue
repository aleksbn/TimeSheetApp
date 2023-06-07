<template>
    <header>
        <nav>
            <h1><router-link to="/">Make your company</router-link></h1>
            <ul>
                <li><router-link to="/companies">All Companies</router-link></li>
                <li><router-link to="/companies/0/departments">All Departments</router-link></li>
                <li><router-link to="/companies/0/employees">All Employees</router-link></li>
                <li><router-link to="/companies/0/workingtimes">All Working Times</router-link></li>
            </ul>
        </nav>
    </header>
</template>

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
</style>