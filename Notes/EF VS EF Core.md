### EF VS EF Core

#### Is still Entity Framework
* Data Model
* DB Context API
* Migrations
* LINQ to Entities
* Change Tracking
* SaveChanges

#### Enables Long-requested Features
* Batch Insert, Update, Delete
* Unique constrains
* Smarter queries
* Smarter disconnected patterns
* In-Memory provider for testing
* Extensible and IoC friendly
* Mapping to backing fields & IEnumerables
* Smarter and simpler Mapping

#### Features Not bringued to Core
* EDMX Support
* ObjectContext API
* Entity SQL
* Metadata Workspace API
* Overly complex mappings
* MEST* mapping
* Automatic Migrations

### Code First Magic

#### EF 4.1 - EF 6 
* Infer database provider if missing
* Infer sonnection string if missing
* Create database if missiong (CreateDatabaseIfNoExists IDatabaseInitializer)
* Other IDatabaseInitializers

#### EF Core
* No More Database Magic
* Specifiy Database Provider and Connection
    * Configue in DB Context (Details can be stored in .NET app/web.config)
    * Cofigure in ASP.NET Core Startup.cs