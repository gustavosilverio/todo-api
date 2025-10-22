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
The main focus of this project is the API. I added several modern technologies because I wanted to implement them in one of my projects: I integrated easy-to-read logs with Serilog, authentication with JWT and Refresh-Token to increase security and allow the user to avoid having to log in every time. I also added an Exception Handler so I can handle custom exceptions as I see fit. In the end, this API turned out to be quite robust, even for a to-do app.
