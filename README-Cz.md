# Witcher system

## System by měl umožňovat:
- autentizaci uživatelů
- správci osob přidělovat uživatelům role
- uživateli změnit si osobní údaje
- zaklínači přistupovat k zakázkám
- zaklínači aktualizovat stav zakázky
- správci zakázek přidávat, upravovat a mazat zakázky
- správci zakázek přiřadit zakázku k zaklínači

## Volitelná rozšíření zadání:
- žádost o přidělení zakázky (bude implementováno)
- databáze zaklínačských znalostí (návrh k dispozici v ERD, zatím nebude implementováno)

## Členové týmu (v abecedním pořadí)
- Michael Koudela 485441
- Patrik Procházka 467880
- Peter Šípoš 527365

## Rozběhnutí localDB MSSQL (1.10.2022)

* Stáhnout Microsoft SQL server (mě fungoval postup z tohoto odkazu)
  * https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?source=recommendations&view=sql-server-ver16:
* Po nainstalování v terminálu lokalizovat **SqllocalDB.exe**
  * Já jsem  našel na adrese: C:\Program Files\Microsoft SQL Server\150\Tools\Binn>
* Zadat příkazy 
  * Vytvoření LocalDB `.\SqlLocalDb create KaerMorhenIS`
  * Spuštění LocalDB `.\SqlLocalDb start KaerMorhenIS`
  * Zkontrolovat, že vše funguje: `.\SqlLocalDb info KaerMorhenIS`
    * ![Info](img.png)
* `connectionString` by měl být nastaven v appsettings.json na DB s názvem KaerMorhenIS.
* V IDE přidat nový Data Source -> Microsoft SQL Server Local Db a jako instanci vybrat KaerMorhen IS
* Vytvořit migraci: *dotnet ef migrations add <jmenomigrace>* NEBO přes plugin entity framework core UI: right click solution -> Tools -> Entity Framework Core -> *Add Migration* 
* Updatovat databázi dle migrace: *dotnet ef database update* NEBO přes plugin *Update Database*
* Data by měla být viditelná v DB

## Hodnocení:
* Kdyby chyběl půlbod na konci semestru, Dominik ho přidá
