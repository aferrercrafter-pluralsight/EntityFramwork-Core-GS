Dotnet Entity Framewoks commands cannot run into Core Library Projects. It's needed create a executable project in order to run those commands. This project must have Microsoft.EntityFrameworkCore.Design package installed.

`dotnet ef migrations add Init --startup-project ../MyExecutableProject`