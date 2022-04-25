Welcome To Simple Timesheet tool.
In this documentation i will provide clear guid to build this soluion successfully.
Please Note that this code is developed under ubuntu 20.04 LTS with vscode 1.53.2

**Requirements:
1- .Net Core 3.1
2- Angular CLI 9
3- EF Core 3.1
4- MySql 14.14

**Installtion:
A- Build Angular Application:
    1- Open Terminal or cmd then navigate to > timesheet-tool/src/Webapp
    2- Install node_modules by typing > npm install

Done Now we are ready to build API

**Build .Net Core Application:
    1- Open Termincal or cmd then navigate to > timesheet-tool/src/TimeSheetAPI
    2- Install required packages by typing > dotnet restore
    3- Open timesheet-tool/src/TimeSheetAPI/appsettings.json and change connectionstring to your MySql server
    4- Migrate Users database by typing > dotnet ef database update --context IdentityDataContext
    5- Migrate TimeSheet database by typing > dotnet ef database update --context TimeSheetDbContext
    6- build application by typing > dotnet watch run

Done !!

This will create 2 databases (TimeSheetAppDb, TimeSheetAppUsersDb)
Please register new user and start test system.

**Regards TimeSheetAPI.Tests, this project contains two different testing techniques:
1- Integration test in (AccountControllerTests, TimeSheetControllerTests)
2- Unit test in UserLoginTests

Happy Coding :)
