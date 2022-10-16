# Witcher system

## Compulsory use cases
Information system enables:
- user authentication
- person manager to assign roles to users
- user to change their personal data
- witcher to create and view contracts
- witcher to update contract states
- contract manager to add, edit and delete contracts 
- contract manager to assign a contract to a witcher

## Optional uses cases (assignment extensions):

- contract requests (to be implemented)
- witcher knowledge database/wikipedia (included in ERD, currently not in implementation plan)

## Team members (alphabetical order)
- Michael Koudela 485441 FI MU
- Patrik Procházka 467880 FI MU
- Peter Šípoš 527365 FI MU

## Running project with MS SQL & Rider (1.10.2022)

* Download Microsoft SQL server
  * https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?source=recommendations&view=sql-server-ver16:
* After it is installed, find **SqllocalDB.exe** in file explorer and switch to that folder
  * default: C:\Program Files\Microsoft SQL Server\150\Tools\Binn>
  * cd <installation_path>
* Use the following commands (<dbname> = KaerMorhenIS)
  * Create LocalDB: `.\SqlLocalDb.exe create <dbname>`
  * Run LocalDB: `.\SqlLocalDb.exe start <dbname>`
  * Check that everything works: `.\SqlLocalDb.exe info <dbname>`
    * ![Info](img.png)
* Check that database `connectionString` in appsettings.json is set to <dbname>.
* Add new Data Source in Rider IDE -> Microsoft SQL Server Local Db -> choose <dbname> as instance name
* Create migration: *dotnet ef migrations add <jmenomigrace>* OR using plugin entity framework core UI: right click solution -> Tools -> Entity Framework Core -> *Add Migration* 
* Update database according to chosen migration: *dotnet ef database update* OR using plugin... *Update Database*
* Data should be visible in DB
