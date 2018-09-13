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

#### EF 4.1 - EF 6 Code First Magic
* Infer database provider if missing
* Infer sonnection string if missing
* Create database if missiong (CreateDatabaseIfNoExists IDatabaseInitializer)
* Other IDatabaseInitializer