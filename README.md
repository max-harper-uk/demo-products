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
1.  Create a new MVC project with entity framework ✅

2.  Add Entity Framework scaffold from DB ✅

3.  Recreate Models & Controllers ✅

4.  Add Open API Generatror / SwaggerGen ✅

5.  Error Handling ✅

6.  Ensure every endpoint has been recreated ✅
    -  Products ✅
    -  ProductOptions ✅

7.  Integration Tests
    -  Get & Create  ✅
    -  Authorization once implemented [ToDo]

8.  Unit Tests
    - All CRUD endpoints with In-Memory DB [ToDo]

9.  Complete Open API spec with futher Swagger Attributes [ToDo]

10.  Authentication & Authorization [ToDo]

11.  Report Metrics [ToDo]

12.  Move database into it's own container ✅

13.  Refactor database to MySql 
     - Configuration, Queries & Commands ✅
     - Script initial EF Migration [ToDo]

14.  Build and Test in Github Actions [Todo]

15.  Documentation ✅

