
<div>

  <h1>Akvelon Task Tracker lab</h1>
  
  <p>
    I hope you like it:)
  </p>
  </div>

  

<!-- About the Project -->
## :star2: About the Project


<!-- TechStack -->
### :space_invader: Tech Stack

<details>
  <summary>Server</summary>
  <ul>
    <li>C# lang</li>
    <li><a href="https://dotnet.microsoft.com/en-us/download">.NET</a></li>
    <li><a href="https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-7.0?view=aspnetcore-7.0">ASP .NET Core</a></li>
    <li><a href="https://swagger.io/">Swagger</a></li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.postgresql.org/">PostgreSQL</a></li>
    <li><a href="https://learn.microsoft.com/en-us/ef/core/">Entity Framework Core</a></li>
  </ul>
</details>

<!-- Features -->
### :dart: Features

- Projects CRUD functionality
- Tasks CRUD functionality

<!-- Getting Started -->
## 	Getting Started

<!-- Prerequisites -->
### :gear: Installation

- Install .NET <a href="https://dotnet.microsoft.com/en-us/download">here</a>
- Then, install <a href="https://www.postgresql.org/download/">PostgreSQL</a>
- Clone the project
```bash
  git clone https://github.com/Raimbek-Takshibaev/Akvelon.TaskTracker.git
```
- Change DefaultConnection value in $(main_project_dir)\Akvelon.TaskTracker.Web.API\appsettings.json
- Update database
```bash
  cd $(main_project_dir)\Akvelon.TaskTracker.Data\
  dotnet ef --startup-project ..\Akvelon.TaskTracker.Web.API\ database update
```

<!-- Run Locally -->
### :running: Run Locally

Clone the project

#### Via VS Community
- Open project in Visual Studio
- Make sure that start project is Akvelon.TaskTracker.Web.Api
- Run


#### Via terminal
Go to the project directory

```bash
  cd $(main_project_dir)\Akvelon.TaskTracker.Web.API\
```


Start the server

```bash
  dotnet run
```

then, you can launch in browser swagger url localhost:5251/swagger/index.html

