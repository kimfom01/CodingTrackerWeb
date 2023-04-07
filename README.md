# CodingTrackerWeb

ASP.Net Core based CRUD application to keep track of coding hours. Developed using ASP.NET Core Razor Pages, C# and Postgres.

# Given Requirements
- [x] You should be able to insert, delete, update and view your logged hours.
- [x] You can only interact with the database using mappers such as Entity Framework. You can't use raw SQL
- [x] You should use the date and time picker to log and not allow any other formats.
- [x] The user should be able to input the start and end times from the time picker.
- [x] Your connection strings should be stored in the appsettings.json file for local development and you should find the connection string in the environment if you wish to deploy the program on the web.
- [x] You'll need to create a "CodingHours" model class in a separate file. It will contain the properties of your coding session: Id, Date, StartTime, EndTime, Duration.
- [x] The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- [x] When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
