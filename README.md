# TimeSheetApp
This project is about taking care of working hours of every emplyee in someone's company. When You make an account on it, type in data about the compoany, it's departments and your employees. Of course, if you just wanna test out the app itself, you can use generate methods insde the app to try out some fake data.

# Installation
First of all, you need backend. 

## Database
Go to the [SQL server download page](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), select Express edition SQL server and install it. Next, head down to the [SQL server management studio download page](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) and download the tool for accessing SQL server in graphic interface.

After you're done with installation, you need to create an empty DB. Type in "sql server management studio" in your start menu and open it. You'll get something like this:
![SQL1](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/69b955da-b274-47fa-90ac-c006ffd285d0)
Right-click on the Databases folder and create a new database under the "TimeSheetDB" name. You'll get something like this:
![SQL2](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/35e86aa0-9663-4794-a75f-0163357ba667)
Don't close the Management Studio window just yet. We still have something to do before starting the app.

## Backend application
Next, download the code from this repositorium. You need an IIS server in order to start it, but a simple Visual Studio will be enough. In case you're not sure where to get Visual Studio, just go to the [VS 2022 download page](https://visualstudio.microsoft.com/downloads/) and download Community version. Once you're done with that, open the Visual Studio and open up the project you've downloaded (double-click on the TimeSheetApp.sln file) and it should open. Oh, and just so you know, you need .net 7. In case you don't have it, head to the [.NET Download page](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), download version that suits your OS and install it.
If everything goes smooth and you manage to open a project, you'll have to edit an address to your server. Yes, I know, it's a lot of fuss, but it's a multilayer app, and it should be complicated. And hopefully...
![image](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/aca85fb6-19a5-4802-8d00-d3267b1fdab3)
Anyway, go back to the SQL Server management studio and copy of the name of your server instance. How do you do that? Just right-click on the server name, select "properties" and copy "Name". Now, head back to the project opened in Visual Studio. double-click the "appsettings.json" file. You'll get this:
![VS1](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/c0181c03-5717-4e85-8d8a-5a0fe27f869d)
Replace data source *ALEXPC\\\ALEX* with the name of your server. Save the file and backend is ready.

## Frontend application
Head down to the [frontend repo](https://github.com/aleksbn/time-sheet-frontend-vue.js) and download the code from it.
Open up your console and go to the folder where you copied frontend project to. In case you need to go a folder up, type:
```
cd ..
```
and in case you need to go down the folder paths, type in:
```
cd \documents\timesheet-frontend-app
```
This is, of course, just an example of the location we need. If, for any reason, your preffered folder is on another disk, say, disk D, type in
```
d:
```
in order to change path to disk D. Rest of the process is the same.

## Installing frontend libraries
By default, most of the projects on github are stored without node_modules folder that consists of many required libraries in order to run the project. In order to download them, once you're in selected location in your command window, type in
```
npm install
```
And that's it, the required libraries are downloaded.
Now, you also need node.js, so head down to the [Node.js download page](https://nodejs.org/en/download), download a desired package and install it.

# Running the app
Head back to the visual studio and press F5 on your keyboard. You'll get console with some data about the app, chrome browser with swagger for testing purposes, and that's it. Backend is running. Now, head to the frontend console where you typed "npm install" in order to install required libraries and type
```
npm run serve
```
Now, head to your preffered web browser, open it up and go to this address: [http://192.168.1.125:8080/](http://192.168.1.125:8080/). Nice, the app is fully launched and ready.

# Using the app
Usage is quite simple, really. The app is meant to take care of the working times of employees in any company, along wth the stats displaying working hours, overtime hours and earned money. Once you press Login/Signup link in the upper right corner, you will get a possibility to either login, or register.
![App01](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/e4b084bb-44c8-49b5-86c3-b7a0edf76ff8)

Fill in the form the way you want (data, of course, does not have to be accurate or real) and you just made yourself a profile! The app will redirect you to the login page. After you login, you can add a company (or more of them). Every company you own will be displayed on the "All companies" link in the navigation bar. Once you click on it, you will get something like this:
![App02](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/290dc3ad-c36d-4f9b-a6c2-1928f3c01078)

In order to see the employees or departments, you must click on the "Details" button on the right side of the company element. You'll get something like this:
![App03](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/431dff71-934c-4b88-b8b4-eecac959c594)

You can now see that extra options appeared on the nav bar. By pressing the self explanatory buttons in the lower part of the screen, you can do the following:
1. Edit company data
2. See the department list
3. See the list of employees
4. See the working times of all company workers
5. See the calculations
6. Delete company

Edit company is pretty much self-explanatory. If you tap on the Departments button, you'll get something like this:
![App04](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/f21e3024-b4a4-44aa-ab67-000bddc0974a)

Pressing the Details button will allow you to see everything about that department, as well as edit it.
![App05](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/e79be0b4-cd01-4f60-9139-30e9078cab1e)

Now, I'm perfectly aware, if you're just checking out the app, you're not willing to add, like, 20 workers and, like, 1 month of their working times day by day, so I made a possibility to generate them randomly. Once you open Employees list, you'll be able to generate certain ammount of employees per department you made and a certain number of working days by cliking on the "randomly generate both employees and their working times", just below the nav bar. Their working hours will also be randomly generated, in range from 6 do 10. Now, if you ether decide to see all the employees from the company, or just the specific ones from some department, the appereance will be the same. As you can see, you can view data about the employee, as well as display and filter them by various criteria and numbers.
![App06](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/2089cfb9-a084-4eca-bd9f-8b8eac03cc01)

In case you want to see their working times, you may do so from the company menu (and see working times of all employees), from the department menu (and see only those under that department), or individually, from the employee details page.
![App07](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/c95ff2bb-5fda-4e27-9b73-7a0dc0de9892)

If you decide to see statistic for any given month, you may do so by clicking on the "Statistics" button from Company Details page. You will get something like this. Just select the period you need the stats for.
![App08](https://github.com/aleksbn/time-sheet-backend-asp.net/assets/44110941/bb867e34-2ffd-41b3-b15c-06b988665b31)

And that's it! If you notice some errors or you want to contact me, you can mail me to [aleksandar.matic986@gmail.com](mailto:aleksandar.matic986@gmail.com).

# Architecture considerations
Project is using the following infrastructure:
1. MS SQL server for database
2. ASP.NET Web API for backend:
- Project is using migrations for altering database structure and seeding data
- Converting entities to DTOs and vice-versa is done my Auto Mapper library
- App is using repository pattern, as well as custom Unit Of Work pattern
- App is using JWT with refresh token pattern
3. Vue.js for frontend:
- Router for dealing with various routes
- Build-in vue transitions for animations
- Vuex store for managing app data
- Vue page components
- Vue UI components
