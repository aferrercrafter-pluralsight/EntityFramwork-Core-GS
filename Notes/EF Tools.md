Dotnet Entity Framewoks CLI commands (dotnet CLI) cannot run into Core Library Projects. It's needed create a executable project in order to run those commands. This project must have Microsoft.EntityFrameworkCore.Design package installed and Domain and Data project referenced.

`dotnet ef migrations add Init --startup-project ../MyExecutableProject`