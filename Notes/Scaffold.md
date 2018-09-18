### EF Core CLI ###
`dotnet ef DbContext scaffold`
### EF Core Pwershell ###
`scaffold-dbcontext "Server = (localdb)\mssqllocaldb; Database = SamuraiData; Trusted_Connection = True; " Microsoft.EntityFrameworkCore.SqlServer`

`command + "connectionstring" + provider`

The second provider makes reference to the provider of the Db it´s going to be scalffolded. It´s __needed__ for the project. __Also is needed__ the Provider.Design Package. Ex __Microsoft.EntityFrameworkCore.SqlServer.Design__