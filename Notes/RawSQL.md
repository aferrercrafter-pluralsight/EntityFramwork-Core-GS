### What about? ###

* Querys that are hard to express in LINQ
* Querys thats result in icky SQL
* Querys that result in slow SQL
* All of my Stored Procedures

### Raw SQL commands ###

__Querys__
`DbSet.FromSql()`
_Replaces DbSet.SqlQuery, DbContext.Database.SqlQuery_

* SQL query or executes store procedure
* Allos parameters
* Must return full entities types (1.1)
* Result set column names = mapped names
* Query must be flat ...no related data
* Not yet: store procedure mappings

__Command__
`DbContext.Database.Command()`
_Same as earlier EF versions_

* Returns int (row affected)
* Can use output parameters
* ExecuteSqlCommandAsync available
* Does not start a transaction  ala SaveChanges()