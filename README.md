# BattleshipGame
 
## Rules
Battleship Game rules ->  [Rules](https://en.wikipedia.org/wiki/Battleship_(game))

## Basic info
The application is an API which provides endpoints to initialize a game and make a move (shoot) by player. These two are sufficient to play the game and successfully finish it. Game data is stored in MSSQL database.  

**Game** is represented as a class with formed relationships to **Players**.  
Each **Player** has two **Boards**, one for tracking enemy's shoots and one for tracking his own progress.  
Each **Board** has a list of **Ships** and **Shots**. 
Each **Ship** and **Shot** store a list of strings with positions. List is stored as a string in JSON format (it's defined in modelBuilder and looks neat in models).

Each operation to database is done in repository, which is injected to controller.

## Implementation

#### Init game
The main role of game initialization process is to generate random positions for ships. The position is defined by two characters: a letter from range A-J and a number from range 1-10. Steps to generate positions are as follows:
1. Draw a letter and number form specified ranges
    * Drawing a letter is achieved by drawing a number and converting it to char [ASCII table](https://www.asciitable.com)
2. Check if position (a combination of letter and number) is occupied by ship
    * If yes, return to 1. step
3. Draw a direction (one of four, one doesn't get drew twice)
4. Check if ship, if placed in drawn direction, doesn't exceed map
    * If it does, return to 3. step
5. Check if ship, if placed in drawn direction, doesn't intersect with any allied ship placed before
    * If it does, return to 3. step. If it does and all directions have been tried, start over at step 1.
6. Add ship to players boards

#### Shot
Shooting leads the main game flow. Players are allowed to shoot when it's their turn. After a successful shot, if the game hasn't finished, another player gets to shoot. A shoot request can return with a BadRequest response if one of the following happens:
1. Position row is not valid
2. Position column is not valid
3. Game is not found (Here, 404 is returned)
4. Wrong player is making a move
5. Position has already been shot before

If none of those happened, a shot is made. Additional info is returned if the ship got sank and if the game has finished.
