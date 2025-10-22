## A simple and very robust API using modern technologies

Technologies used here:
- .NET 8
- JWT
- Refresh-Token
- Serilog
- Dynamic DI
- PetaPoco
- SQLKata
- SQL Server
- Newtonsoft.Json
- Docker


### DevOps
- Hosted in a Ubuntu Server VPS with **DOCKER**
- CI/CD to build and deploy to the VPS

------
### Description
The main part of this project is the API. I added several modern technologies because I wanted to implement them in one of my projects: I integrated easy-to-read logs with Serilog, authentication with JWT and Refresh-Token to increase security and allow the user to avoid having to log in every time on the platform. I added an Exception Handler to handle custom exceptions as I see fit, and I also added CI/CD via Github Actions to build and deploy the project via Docker to my Ubuntu VPS. In the end, this API was quite robust, even for a to-do app.

### The frontend of this project is in [other repository](https://github.com/gustavosilverio/todo)
