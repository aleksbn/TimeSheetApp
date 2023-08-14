<template>
  <div>
    <base-card>
      <div class="links">
        <table style="width: 100%">
          <tr>
            <th>
              <router-link to="/about">
                <h1>About me</h1>
              </router-link>
            </th>
            <th>
              <router-link to="/installationmanual">
                <h1>Installation manual</h1>
              </router-link>
            </th>
          </tr>
        </table>
      </div>
      <p>
        Usage is quite simple, really. The app is meant to take care of the
        working times of employees in any company, along wth the stats
        displaying working hours, overtime hours and earned money.
      </p>
      <p>
        Once you press Login/Signup link in the upper right corner, you will get
        a possibility to either login, or register.
      </p>
      <p>
        <img src="../../media/images/App01.jpg" alt="" />
      </p>
      <p>
        Fill in the form the way you want (data, of course, does not have to be
        accurate or real) and you just made yourself a profile! The app will
        redirect you to the login page. After you login, you can add a company
        (or more of them).
      </p>
      <p>
        Every company you own will be displayed on the "All companies" link in
        the navigation bar. Once you click on it, you will get something like
        this:
      </p>
      <p>
        <img src="../../media/images/App02.jpg" alt="" />
      </p>
      <p>
        In order to see the employees or departments, you must click on the
        "Details" button on the right side of the company element. You'll get
        something like this:
      </p>
      <p>
        <img src="../../media/images/App03.jpg" alt="" />
      </p>
      <p> You can now see that extra options appeared on the nav bar.
      <ul>By pressing the self explanatory buttons in the lower part of the screen, you can do the following:
        <li>Edit company data</li>
        <li>See the department list</li>
        <li>See the list of employees</li>
        <li>See the working times of all company workers</li>
        <li>See the calculations</li>
        <li>Delete company</li>
      </ul>
      </p>
      <p>
        Edit company is pretty much self-explanatory. If you tap on the Departments button, you'll get something like
        this:
      </p>
      <p>
        <img src="../../media/images/App04.jpg" alt="" />
      </p>
      <p>Pressing the Details button will allow you to see everything about that department, as well as edit it.</p>
      <p>
        <img src="../../media/images/App05.jpg" alt="" />
      </p>
      <p>
        Now, I'm perfectly aware, if you're just checking out the app, you're not willing to add, like, 20 workers and,
        like, 1 month of their working times day by day, so I made a possibility to generate them randomly. Once you open
        Employees list, you'll be able to generate certain ammount of employees per department you made and a certain
        number of working days by cliking on the "randomly generate both employees and their working times", just below
        the nav bar. Their working hours will also be randomly generated, in range from 6 do 10. Now, if you ether decide
        to see all the employees from the company, or just the specific ones from some department, the appereance will be
        the same. As you can see, you can view data about the employee, as well as display and filter them by various
        criteria and numbers.
      </p>
      <p>
        <img src="../../media/images/App06.jpg" alt="" />
      </p>
      <p>
        In case you want to see their working times, you may do so from the company menu (and see working times of all
        employees), from the department menu (and see only those under that department), or individually, from the
        employee details page.
      </p>
      <p>
        <img src="../../media/images/App07.jpg" alt="" />
      </p>
      <p>
        If you decide to see statistic for any given month, you may do so by clicking on the "Statistics" button from
        Company Details page. You will get something like this. Just select the period you need the stats for.
      </p>
      <p>
        <img src="../../media/images/App08.jpg" alt="" />
      </p>
      <p>And that's it! If you notice some errors or you want to contact me, you can mail me to <a
          href="mailto:aleksandar.matic986@gmail.com">aleksandar.matic986@gmail.com</a>. Cheers!</p>
    </base-card>
  </div>
</template>

<style scoped>
p {
  text-align: justify;
}

img {
  width: 100%;
}

iframe {
  aspect-ratio: 16 / 9;
  height: 90%;
  width: 90%;
}

.links h1 {
  display: inline;
}

.links a:visited,
a:link {
  text-decoration: none;
}
</style>
