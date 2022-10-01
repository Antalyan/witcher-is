# Zaklínačský systém / Witcher system

## Systém by měl umožňovat:
- autentizaci uživatelů
- správci osob přidělovat uživatelům role
- uživateli změnit si osobní údaje
- zaklínači přistupovat k zakázkám
- zaklínači aktualizovat stav zakázky
- správci zakázek přidávat, upravovat a mazat zakázky
- správci zakázek přiřadit zakázku k zaklínači

## Volitelná rozšíření zadání:
- žádost o přidělení zakázky
- databáze zaklínačských znalostí

## Členové týmu (v abecedním pořadí)
- Michael Koudela 485441
- Patrik Procházka 467880
- Peter Šípoš 527365

## Rozběhnutí localDB MSSQL (1.10.2022)

* Stáhnout Microsoft SQL server (mě fungoval postup z tohoto odkazu)
  * https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?source=recommendations&view=sql-server-ver16:
* Po naistalování v termínálu lokalizovat **SqllocalDB.exe**
  * Já jsem  našel na adrese: C:\Program Files\Microsoft SQL Server\150\Tools\Binn>
* Zadat příkazy 
  * Vytvoření LokalDB `SqlLocalDb create KaerMorhenIS`
  * Spuštění LocalDB `SqlLocalDb start KaerMorhenIS`
  * Zkontrolovat že vše funguje: `SqlLocalDb info KaerMorhenIS`
    * ![Info](img.png)
* `connectionString` by měl být nastaven na DB s názvem KaerMorhenIS.
* Po naseedování databáze přes `database update` by měli být data viditelné v DB