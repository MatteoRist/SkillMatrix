# Project Overview

## Introduction

#### DOIT Internal project to manage skill matrix

This document provides an overview of Skill Matrix, which will be a social network that allows developers to share their
skills and knowledge with others in the business. The software is designed to give administrators and project managers
an overview of the developers' skills to help them manage resources the best way possible and also provide a friendly
mean of communication among the teams' members.

## Structure

Skill Matrix is a full-stack project, provided of its own api and user interface.

**Skill Matrix API:** is a restfull api, it interacts with the database to serve the client all the data it needs to
let the application work:

- User's data
- Statistics
- Skills and Question to which the user will answer

**Skill Matrix Client:** it is served as a web application and user end point where all of their interactions with the
system will happen. Showing a dashboard as landing page and a sidebar to assist user's navigation Skill Matrix provides
a user-friendly interface

## Tech

#### Data Base

- SQL Server DB

#### API

- Entity Framework Core
- ASP.NET Api
- C#

#### Client

- React
- Material UI
- Typescript

# Architecture and Design

## Database - Models

Models define how data is stored and managed at database level. Note: it's not necessary the same way as they are
returned from the api, check out the swagger documentation for that.

- User: UserId, Name, email
- Skill: SkillId, CategoryId, Title,
- Category: CategoryId, Name
- Question: QuestionId, Body, MinValue, MaxValue
- Record: RecordId, UserId, SkillId, QuestionId, Value

## Api - Services

The API is built with modularity. Skill Matrix uses multiple services through dependency injection to interact with the
database:

- IUserRepository - provides method to query users data
- ISkillRepository - provides method to query skills data
- ICategoryRepository - provides method to query categories data
- IQuestionRepository - provides method to query questions data
- IRecordRepository - provides method to query records data

furthermore some of these inherit from ICrudRepository which contains methods entity independent

## Api - Controllers

Controllers define the Api endpoints. Skill Matrix has one controller for each model:

- UsersController - provides endpoints to retrieve users data
- SkillsController - provides endpoints to retrieve skills data
- CategoriesController - provides endpoints to retrieve categories data
- QuestionsController - provides endpoints to retrieve questions data
- RecordsController - provides endpoints to retrieve records data
- HomeController - provides endpoints for general responses like errors

## Api - Endpoints

Launch the Api in development environment to check out swagger UI documentation, or check out the
[swagger json documentation](swagger_documentation_v1.0.json)

## Client - React App

The skill Matrix Web App is built with React. React components are injected in the page by the framework whenever they
are needed.

Main and global files:

- [index.html](clientapp%2Fpublic%2Findex.html) - the index file, where the whole page is injected
- [index.tsx](clientapp%2Fsrc%2Findex.tsx) - the file containing the React application
- [App.tsx](clientapp%2Fsrc%2FApp.tsx) - acting as routing manager sets up all the routes and feeds data to the
  components
- [routingData.tsx](clientapp%2Fsrc%2FroutingData.tsx) - exports data about the different routes, such as: name, path,
  icon and various flags
- [MatrixApi.tsx](clientapp%2Fsrc%2FMatrixApi.tsx) - using Axios provides methods to retrieve data from the api. Used in
  App.tsx
- [constants.tsx](clientapp%2Fsrc%2Fconstants.tsx) - contains constant global values
- [global.d.ts](clientapp%2Fsrc%2Fglobals%2Fglobal.d.ts) - contains global interface and types declarations
- [theme.tsx](clientapp%2Fsrc%2Ftheme.tsx) - contains Material UI theme settings (styles)

## Client - MUI and style

[Material UI (or MUI)](https://mui.com/) is a React library for styling purposes. The Skill Matrix web app's style
derives from MUI. Except for inline styles, MUI theme data is contained in [theme.tsx](clientapp%2Fsrc%2Ftheme.tsx).
The file describes 2 color palettes for dark and light theme, then some of the colors are passed on to ThemeSettings
to be used as default colors for MUI components, the others are available to be used for inline styling.
the theme will be used as a React hook outside theme.tsx.

## Client - Components

React application can usually be broken down un different components.
Skill Matrix web app's components:

- Page Components - the ones that occupy the whole page and are used only once:
    - [Home.tsx](clientapp%2Fsrc%2Fcomponents%2FPages%2FHome.tsx) - the dashboard
    - [NoPage.tsx](clientapp%2Fsrc%2Fcomponents%2FPages%2FNoPage.tsx) - the noPage
    - [Surveys.tsx](clientapp%2Fsrc%2Fcomponents%2FPages%2FSurveys.tsx) - the page that lists available surveys
    - [SingleSurvey.tsx](clientapp%2Fsrc%2Fcomponents%2FPages%2FSingleSurvey.tsx) - the page that lets the user take the
      survey
    - [Skills.tsx](clientapp%2Fsrc%2Fcomponents%2FPages%2FSkills.tsx) - statistics and user skill's data
- Reusable components - the ones that are or can be used in multiple places:
    - [RadarChart.tsx](clientapp%2Fsrc%2Fcomponents%2FRadarChart%2FRadarChart.tsx) - a
      radar [chart.js](https://www.chartjs.org/docs/latest/) it uses the chart.js library
- Others:
    - [SideNav.tsx](clientapp%2Fsrc%2Fcomponents%2FSideNav%2FSideNav.tsx) - the sidebar,
      from [react-pro-sidebar](https://github.com/azouaoui-med/react-pro-sidebar).

# Setup

#### Follow the list step-by-step in order to successfully run the source code

1. Download and install a database of your choice,
   e.g. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Create a new query and run the following code. It will create a new database called skill_database along with all the
   necessary tables and sample data
    ```sql
    CREATE DATABASE skill_database;
    
    USE skill_database;
    
    CREATE TABLE Users (
    UserId INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserId)
    );
    
    CREATE TABLE Skills (
    SkillId INT IDENTITY(1,1) NOT NULL,
    CategoryId INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    PRIMARY KEY (SkillId),
    FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId)
    );
    
    CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    PRIMARY KEY (CategoryId)
    );
    
    CREATE TABLE Questions (
    QuestionId INT IDENTITY(1,1) NOT NULL,
    Body VARCHAR(255) NOT NULL,
    MinValue INT NOT NULL,
    MaxValue INT NOT NULL,
    PRIMARY KEY (QuestionId)
    );
    
    CREATE TABLE Records (
    RecordId INT IDENTITY(1,1) NOT NULL,
    UserId INT NOT NULL,
    SkillId INT NOT NULL,
    QuestionId INT NOT NULL,
    Value INT NOT NULL,
    PRIMARY KEY (RecordId),
    FOREIGN KEY (UserId) REFERENCES Users (UserId),
    FOREIGN KEY (SkillId) REFERENCES Skills (SkillId),
    FOREIGN KEY (QuestionId) REFERENCES Questions (QuestionId)
    );
    
    INSERT INTO Users (Name, Email) VALUES
    ('Matteo Ristoro', 'matteo.ristoro@gmail.com');
    
    INSERT INTO Questions (Body, MinValue, MaxValue) VALUES
    ('How many projects have you carried out in the last year regarding {}?', 1, 5),
    ('How many years have you been working with {}?', 1, 20);
    
    INSERT INTO Category (Name) VALUES
    ('Cloud Providers'),
    ('Databases'),
    ('Frameworks'),
    ('Programming Languages'),
    ('Tools');
    
    INSERT INTO Skills (CategoryId, Title) VALUES
    (1, 'C#'),
    (1, 'Javascript'),
    (1, 'Python'),
    (1, 'Typescript'),
    (1, 'Java'),
    (1, 'Rust'),
    (1, 'Go'),
    (1, 'PHP'),
    (1, 'SQL'),
    (1, 'Swift'),
    (1, 'Dart'),
    (1, 'Kotlin'),
    (2, 'SQL Server'),
    (2, 'Postgres'),
    (2, 'Cosmos'),
    (2, 'MongoDB'),
    (2, 'RavenDB'),
    (2, 'Prometeus'),
    (2, 'Influx'),
    (2, 'Cassandra'),
    (2, 'Neo4j'),
    (3, 'Angular'),
    (3, 'React'),
    (3, 'React Native'),
    (3, 'Next JS'),
    (3, 'Vue'),
    (3, 'AspNet.Core'),
    (3, 'Nodejs'),
    (3, 'Deno'),
    (3, 'Laravel'),
    (3, 'Flask'),
    (3, 'Django'),
    (3, 'Xamarin'),
    (3, 'Apple SDK'),
    (3, 'Android SDK'),
    (3, 'Spring Boot'),
    (4, 'Azure'),
    (4, 'AWS'),
    (4, 'GCP'),
    (5, 'Visual Studio'),
    (5, 'VSCode'),
    (5, 'Powershell'),
    (5, 'Bash'),
    (5, 'Git'),
    (5, 'Kubectl'),
    (5, 'Docker'),
    (5, 'GitHub'),
    (5, 'Azure DevOps'),
    (5, 'Chrome Developer 5'),
    (5, 'MS SQL Server Management Studio'),
    (5, 'Postman'),
    (5, 'Excel'),
    (5, 'Jupiter notebook'),
    (5, 'JetBrains 5');
    ``` 
3. Save the connection string for later
4. download and install the c# compiler. Or an IDE
   like [visual studio](https://visualstudio.microsoft.com/it/downloads/)
   to make things easier (look at step 5)
5. install all the api packages, although if you went with the visual studio option it will install them for you at
   build time, so no need to worry.
6. open [appsettings.json](skill-matrix-api%2Fappsettings.json) and replace the sample connection string with your
   connection string
   ```json
    "ConnectionStrings": {
    "DefaultDatabase": "Your connection string"
    }
7. build the api and run
8. if the api can't connect to the database, try adding ```Trust Server Certificate=True``` at the end of your
   connection string, it will prevent the api from checking CAs, as this is a demo.
   ```json
    "ConnectionStrings": {
    "DefaultDatabase": "Your connection string;Trust Server Certificate=True"
    }
9. assuming the api is working correctly, check the port number where the api is running, it will be the first log from
   Microsoft.Hosting in the api console
    ```
    info: Microsoft.Hosting.Lifetime[14]
    Now listening on: https://localhost:7207
    ```
   the default port number is 7207, if that is the case move on to the next step, if not we must tell the client, so
   copy the port number and open [constants.tsx](clientapp%2Fsrc%2Fconstants.tsx). Replace ```'7207'``` with your port
   number e.g.:
    ```
    const apiPort = 'your port number'
    ```
10. download and install [Node.js](https://nodejs.org/en/download)
11. move to the [clientapp](clientapp) folder and run
    ```npm install```
    to install the dependencies
12. lastly run the client with the following command
    ```npm start```
13. the demo should be now working correctly. Apologise for unprevented issues.
