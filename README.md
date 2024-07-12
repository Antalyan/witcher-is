# Witcher system

## Domain description
Project features an information system for witchers (fictional job, known from A. Sapkowski's world). 

The domain for which the system will be modeled is the witcher castle Kaer
Morhen. The system should provide all the information related to the
witcher job activities for witchers from this fort. In addition to basic personal
information and the distribution of individual responsibilities, this includes especially detailed
data on individual contracts and their distribution to individual persons. Important
part of the system is also the knowledge, obtained and preserved for future generations of witchers for centuries.

People play several roles in the system. Order manager
registers all requests from the public and assigning witches to
individual orders. He is responsible for the records of all persons and their roles
people manager. The main work is then performed by the witcher himself, who
executes individual orders.

Project documentation corresponds to several months of work by a professional company. The implementation in this repository has therefore been greatly simplified:

## Function requirements for implementation

### Compulsory use cases
Information system enables:
- user authentication
- person manager to add new users
- person manager to assign roles to users
- user to change their personal data
- witcher to create and view contracts
- witcher to update contract states
- contract manager to add, edit and delete contracts 
- contract manager to assign a contract to a witcher
- witcher to ask for contracts

### Optional uses cases (assignment extensions):
- witcher knowledge database/wikipedia (included in ERD, currently not in implementation plan)

Project was implemented within an optional course PV179: System development in C#/.NET at Faculty of Informatics, Masaryk University.

Project documentation was created mainly within courses PA116: Domain Understanding and Modelling and PA179: Project Management. 

## Documentation

Project documentation for implementation includes:

- ERD diagram of the implemented system
- a diagram showing the infrastructure of the entire project

In addition, additional analytical documents for the extended assignment are included:

- project diagrams (for the waterfall development model): mind map, use cases and their description, use case diagram, event partitioning, data flow diagrams, wireframes, data dictionary, ERD, sequence diagram, state diagram, Gantt diagram
- (fictive) project documentation (for agile development): PPP, project charter, WBS, duration, effort and financial estimates; risk analysis; Gantt chart, implementation of changes and closure of the project

## Project structure

This is one .NET solution, divided into several projects by standard layers, including test examples.

The structure of the project is more complex because the goal was to try different design patterns: Query Object, Unit of Work, Repository, CQRS.

Technologies used: C#/.NET, Blazor, Entity Framework, ASP.NET Core Identity

## Implementation team members
- Michael Koudela 
- Patrik Procházka 
- Peter Šípoš

## Planning team members
- Ondřej Dacer
- Zdena Faragulová
- Michael Koudela 
- Peter Šípoš

## Running project with MS SQL & Rider (1.10.2022)

* Download Microsoft SQL server
  * https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?source=recommendations&view=sql-server-ver16:
* After it is installed, find **SqllocalDB.exe** in file explorer and switch to that folder
  * default: C:\Program Files\Microsoft SQL Server\150\Tools\Binn
  * cd <installation_path>
* Use the following commands (in this project, *dbname* = *KaerMorhenIS*)
  * Create LocalDB: `.\SqlLocalDb.exe create <dbname>`
  * Run LocalDB: `.\SqlLocalDb.exe start <dbname>`
  * Check that everything works: `.\SqlLocalDb.exe info <dbname>`
* Check that database `connectionString` in appsettings.json is set to *dbname*.
* Add new Data Source in Rider IDE -> Microsoft SQL Server Local Db -> choose *dbname* as instance name
* Create migration: *dotnet ef migrations add <jmenomigrace>* OR using plugin entity framework core UI: right click solution -> Tools -> Entity Framework Core -> *Add Migration* 
* Update database according to chosen migration: *dotnet ef database update* OR using plugin... *Update Database*
* Data should be visible in DB

* Starting app: https://www.jetbrains.com/help/rider/Running_IISExpress.html

## Running Blazor in hot-reload mode

* Use command `dotnet watch run --version 6`

OR

* Install .NET Watch Run Configuration plugin and create a new edit configuration based on this plugin with program arguments `--version 6`