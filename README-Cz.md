# Witcher system

## Zadání
Projekt představuje informační systém pro zaklínače (fiktivní povolání, známé ze světa A. Sapkowského). 

Doménou, pro kterou bude vymodelován systém, je zaklínačské hradiště Kaer
Morhen. Systém by měl poskytovat veškeré informace související s vykonáváním
zaklínačské činnosti pro zaklínače z tohoto hradiště. Kromě základních osobních
informací a rozdělení jednotlivých zodpovědností to zahrnuje zejména detailní
údaje o jednotlivých zakázkách a jejich rozdělení jednotlivým osobám. Důležitou
složkou informačního systému jsou také poznatky získané vykonáváním tohoto
řemesla po staletí, uchovávané pro další generace zaklínačů.

V systému vystupují osoby několika rolí. Správce zakázek
eviduje veškeré poptávky ze strany veřejnosti a přiřazování zaklínačů k
jednotlivým zakázkám. Evidenci všech osob a jejich role má na starost
správce osob. Hlavní černou práci pak odvádí samotný zaklínač, který
vykonává jednotlivé zakázky.

Projektová dokumentace odpovídá několikaměsíční práci profesionální firmy. Implementace v tomto repozitáři byla proto značně zjednodušena: 

## Funkční požadavky implementace

### System by měl umožňovat:
- autentizaci uživatelů
- správci osob vytvářet uživatele
- správci osob přidělovat uživatelům role
- uživateli změnit si osobní údaje
- zaklínači přistupovat k zakázkám
- zaklínači aktualizovat stav zakázky
- správci zakázek přidávat, upravovat a mazat zakázky
- správci zakázek přiřadit zakázku k zaklínači
- zaklínači požádat o přidělení zakázky

### Volitelná rozšíření zadání:
- databáze zaklínačských znalostí (návrh k dispozici v ERD, zatím nebude implementováno)

Projekt byl implementován v rámci volitelného předmětu PV179: Vývoj systémů v C#/.NET na Fakultě informatiky Masarykovy univerzity.

Projektová dokumentace byla vytvořena v rámci předmětů PA116: Domain Understanding and Modelling a PA179: Project Management. 

## Dokumentace

Projektová dokumentace k implementaci zahrnuje:

- ERD diagram implementovaného systému
- diagram znázorňující infrastrukturu celého projektu

Kromě toho jsou součástí další analytické dokumenty pro rozšířené zadání:

- projektové diagramy (pro vodopádový vývojový model): myšlenková mapa, případy užití a jejich popis, diagram případů užití, event partitioning, diagramy toků dat, wireframy, datový slovník, ERD, sekvenční diagram, stavový diagram, Ganttův diagram 
- (fiktivní) projektová dokumentace (pro agilní vývoj): PPP, project charter, WBS, odhady trvání, úsilí a financí; analýza rizik; Ganttův diagram, realizace změn a uzavření projektu

## Struktura projektu

Jedná se o jednu .NET solutionu, rozdělenou do několika projektů standardně podle vrstev, včetně příkladů testů. 

Struktura projektu je složitější, protože cílem bylo vyzkoušet si různé návrhové vzory: Query Object, Unit of Work, Repository, CQRS.

Využité technologie: C#/.NET, Blazor, Entity Framework, ASP.NET Core Identity

## Členové implementačního týmu (v abecedním pořadí)
- Michael Koudela 
- Patrik Procházka 
- Peter Šípoš

## Členové plánovacího týmu (v abecedním pořadí)
- Ondřej Dacer
- Zdena Faragulová
- Michael Koudela 
- Peter Šípoš

## Jak spustit lokálně

Instrukce jsou k dispozici v anglické verzi README.
