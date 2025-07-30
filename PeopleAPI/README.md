# PeopleAPI

A RESTful Web API built with .NET 9 for managing people, their professions, and hobbies using Entity Framework Core and MySQL. Created with a code first approach

## Technologies Used

- .NET 9 & ASP.NET Core Web API
- Entity Framework Core 9.0
- MySQL 9.3
- Swagger/OpenAPI

## Database Entities

- **Person**: Id, FirstName, LastName, DateOfBirth, Comments, ProfessionId (nullable)
- **Profession**: Id, Name
- **Hobby**: Id, Name

## API Endpoints

Each entity will have 5 CRUD endpoints: 
- `GET /api/{Entity}` - Get all
- `GET /api/{Entity}/{id}` - Get by ID
- `POST /api/{Entity}` - Create
- `PUT /api/{Entity}/{id}` - Update
- `DELETE /api/{Entity}/{id}` - Delete

## Run on your device
**Setup MySQL database** named `PeopleAPI`

**Install and run**
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

**View and test endpoints through Swagger UI**: `http://localhost:5062/swagger`
<i>All Controllers have been created using scaffolding</i>