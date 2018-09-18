### Basic Migrations Workflow

1. Define Change Model 
2. Create a Migration File
3. Aply Migration to DB or Script

### Comands
Data project should be the Startud Project for applying commands

`get-help EntityFrameworkCore`
`Add-Migration`

`Script Migration`
* More Control
* Production databases
* Apply needed tweeks

`Update-Database`
* Best for local Development

`Remove-Migration`
__Does:__
* Delete latest migration
* Updates snapshot

__Does Not:__
* Undo migrations on Database
* Modify your model´s code (domain classes, DbContext)

`Script-Migration -from 2ndMigration -to 4thMigration`
From the next one after this migration (3rd Migration)