# Lp2 Projeto 3

## Autoria

Lee Dias, a22302809;

## Lista de Tarefas

| Data | Nome | Tarefa |
|------|------|--------|
|25/12|Lee Dias|Adiciona um map reader que so le lands mas mostra os no mapa com scriptable objects de land|
|25/12|Lee Dias|Completou o projeto 1 sem UI|
|27/12|Lee Dias|Adiciona as units mas ainda sem movimento e sem a logica completa de harvest|
|27/12|Lee Dias|Adiciona as units a serem geradas no ecrã|
|28/12|Lee Dias|Acaba praticamente o codigo todo do jogo faltando UI e validações|
|29/12|Lee Dias|Acabou o UI e validações faltando apenas uma melhor organização|
|29/12|Lee Dias|Codigo melhor organizado e commentado necisstando apenas do markdown|
|29/12|Lee Dias|Completou o markdown|


## [>Repositório Git<](https://github.com/Lee-Dias/LP2_Projeto3)

## Arquitetura da Solução

### Descrição

Começou se por refazer a logica toda do projeto 1 e para isso começou se por criar um 
scriptable object para lands e um script que gera o mapa consoante um ficheiro,
.map4x que verifica o tamanho se existe a land que é pretendida e gera em, 
posições diferentes a cada um, em seguida fez se as units tambem com scriptable object,
e criou se um script units para as units, em seguida fez se as units serem geradas dentro do jogo,
e depois completou se o script de units para elas moverem ou darem harvest com isso houve a adição do 
script de unitselectmanager para se saber quais units estão selecionadas depois de disso,
notou se que era algo que melhorava o codigo por isso tambem se adicionou o script tileSelectManager,
por fim completou-se o UI todo juntamente com a camera do jogo.

A nivel de principio SOLID acerdito que estejam todos, o maior cuidado foi na parte de tentar fazer ao maximo com que 
as classes nunca precissasem ser alteradas caso fosse adicionada um tile ou um unit novo para isso foi criado scriptable objects
e fez se verificações a partir de tudo existente e que pode vir a existir e não o que se sabe que ja existe. 


### Diagrama Uml
```mermaid
---
title: Lp2 Project 3
---
classDiagram
    note "Lp2 Project 3"
    Start --> GameMap
    Start --> CameraController
    GameMap <|-- TileInfo
    GameMap <|-- CameraController
    TileInfo <|-- Tile
    TileInfo <|-- Resources
    TileInfo <|-- Lands
    UnitSelectManager <|-- Units
    UnitSelectManager <|-- Unit
    TileSelectManager <|-- TileInfo
    Units <|-- TileInfo
    Units <|-- Resources
    Unit --> Resources
    TIlesChecker --> TileInfo
    GenerateUnits --> Units
    GenerateUnits --> TileInfo
    UiManager --> UnitSelectManager
    UiManager --> Resources
    UiManager --> TileInfo
    UiManager --> Units
    UiManager --> GameMap

    class Start {
        +StartGame()
        +OpenExplorer()
    }
    class GameMap {
        +GenerateMap(mapInfo)
        +GameStart(mapPath)
    }
    class TileInfo {
        +Initialize(Lands land)
        +AddResources(Resources resources)
        +RemoveResources(Resources resources)
        +GetTotalCoins()
        +GetTotalFood()
        +GetTileNameAndResources()
        +checkChild()
    }
    class Tile {
        +CalculateCoins()
        +CalculateFood()
        +AddResource(Resources resourceToAdd)
        +RemoveResource(Resources resourceToRemove)
    }
    class Resources {
        +coinModifier()
        +foodModifier()
    }
    class Lands {
    }
    class UnitSelectManager {
        +SelectUnit(Units unit)
        +DeselectUnit(Units unit)
        +MoveAllUnits()
        +DeselectAllUnits()
        +RemoveAllUnits()
        +HarvestSelectedUnits()
        +GetSelectedUnits()
    }
    class TileSelectManager {
        +SelectTile(TileInfo tile)
        +DeselectTile()
    }
    class Units {
        +OnSelected()
        +OnDeselected()
        +MoveUnit(TileInfo tile)
        +UnitRemoveSelf()
        +UnitHarvest()
    }
    class Unit {
        +ResourcesToHarvest
        +ResourcesToGenerate
        +UnitImage
        +Movement
    }
    class CameraController {
        +HandleZoom()
        +HandleRightClickDrag()
        +CameraPosition()
    }
    class TIlesChecker {
        +checkchilds()
    }
    class GenerateUnits {
        +PlaceUnit(Unit unit)
    }
    class UiManager {
        +Play()
        +UnitInfoSlected()
        +checkAllResourcesToGet()
        +UnitsSelected()
    }

