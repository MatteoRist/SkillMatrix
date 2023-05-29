# Introduction

DOIT Internal project to manage skill matrix

# Definition

- User login with AAD federation on DOIT tenant
- A list of skills appear
- For every skill, if a user mark that he/she knows it, a secondary section appear containing some detailed requests.
    - Yeras of work (1,2,3,4,>=5)
    - Level (1 only training,2,3,4,5 ninja)
    - ...
- A user can save in every moment
- Every time the user opens up this dashboard it see the same skill assessment

# Tech

- SQL Server DB
- Entity Framework Core with code first
- ASP.NET Api
- Client React

# Models

- User: Id, Name, email
- Skill: Id, UserId, Description, ....
- Skill level: ID, SkillId, Type (Years of work, level, ecc...), Response