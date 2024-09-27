# Demo Products API

## Running in Docker

### Prerequesites
1.  In order to run in Docker, Docker Desktop needs to be installed.

### Command Line
1.  From the root directory run
```
docker compose up --build
```

2.  To Interact with the API using the Swagger interface,
    in the browser navigate to 
    
    http://localhost:8080/swagger/index.html

## Running Locally

### Prerequesites
1.  In order to run locally, this project requires the .NET8 SDK to be installed.

### Command Line
1.  Navigate to `src/Demo.Products.API`
```
cd src/Demo.Products.API
```
2.  Run the command `dotnet run`

3.  To Interact with the API using the Swagger interface,
    in the browser navigate to 
    
    http://localhost:5038/swagger/index.html

## Tasks
1.  Script intial EF Migration ✅
2.  Authentication & Authorization
3.  Healthchecks
4.  Metrics
5.  Integration Tests
    -  Get All Products  ✅
    -  Authorization once implemented 
6.  Build and Test in Github Actions 
7.  Unit Tests
    - All CRUD endpoints with In-Memory DB 
7.  Complete Open API spec with futher Swagger Attributes 
