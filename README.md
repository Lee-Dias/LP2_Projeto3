# Lp2 Projeto 3

## Autoria

Lee Dias, a22405765;

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
|24/01|Lee Dias|fez os resources aparecerem no ecra|
|26/01|Lee Dias|atualizou o projeto e o codigo consoante o feedback do professor(80 linhas, using inuteis, publics inuteis)|
|26/01|Lee Dias|atualizou o relatorio|



## [>Repositório Git<](https://github.com/Lee-Dias/LP2_Projeto3)

## Arquitetura da Solução

### Descrição

Antes de começar o projeto pensou se logo em como ia se implementar e quais 
patterns seriam os melhores a serem implementados e percebeu-se que o 
flyweight pattern seria algo bastante bom para este projeto então implementou-se 
scriptable objects para coisas que se repetiam tal como recursos,lands e units.

Começou se por refazer a logica toda do projeto 1, para isso começou se por criar um 
scriptable object para lands e um script que gera o mapa consoante um ficheiro
.map4x, que verifica o tamanho se existe a land que é pretendida e gera em, 
posições diferentes a cada um. 

Em seguida fez se as units, e criou se um script unit para as units,
em seguida fez se as units serem geradas dentro do jogo,
e depois completou se o script de units para elas moverem ou darem harvest com isso houve a adição do 
script de unitselectmanager para se saber quais units estão selecionadas depois disso,
tambem se adicionou o script tileSelectManager para ficar mais coerente,
por fim completou-se o UI todo juntamente com a camera do jogo.

A nivel de MVC o projeto foi feito de forma que quando nos necessitavamos de uma classe,
antes de a criarmos viamos aonde é que ela entrava dentro do MVC,
e tentava se fazer com que realmente fizesse sentido aquela classe para aquela parte do MVC,
então evitava-se logo coisas como alterar textos numa classe de modelo. 

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
    GenerateUnits --> Units
    GenerateUnits --> TileInfo
    UiManager --> UnitSelectManager
    UiManager --> Resources
    UiManager --> TileInfo
    UiManager --> Units
    UiManager --> GameMap

    class Start {
    }
    class GameMap {
    }
    class TileInfo {
    }
    class Tile {
    }
    class Resources {
    }
    class Lands {
    }
    class UnitSelectManager {
    }
    class TileSelectManager {
    }
    class Units {
    }
    class Unit {
    }
    class CameraController {
    }
    class GenerateUnits {
    }
    class UiManager {
    }

