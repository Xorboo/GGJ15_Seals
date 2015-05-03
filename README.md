# Jumuzzy
## Story
A game about a special forces seal, who got lost in the wild jungle after his squad were captured and recklessly killed by cruel raccoons and he was the only one who got away. After weeks of wandering around, he got biten by The Poisonous Snake. After that his personality was split into parts. And while he was barely able to control himself, he were attacked by hordes of evil raccoons. Will he be able to get himself together and seal with this situation? His multiple minds will have to cooperate with each other to decide what to do with the only body they have. 
## Description
The game is calle "Jumuzzy", which on Yoruba language means "cooperation". It is a multiplayer game, where every player controls the same character, therefore players have to cooperate with each other to decide, what the poor seal have to do. The character have to use availible weapons (katana and rifle of course, what else do you expect for military seal to have?) to defeat hordes of zombie and sniper raccoons and the (literally) biggest arch enemy - The Bear. After defeating the bear seal will find a magic burger, which will be able to heal him from his ilness. And if the players wouldn't be able to decide what to do, the seal will die in battle. 

## Technical info
Game written on Unity 4.6 and requires pro versions to compile on mobile devices (due to usage of .NET Sockets).

The game runs on server, which also renders current game status. The clients can be installed on any device (mobile/pc) and only show the oldschool controller with joystick and 2 buttons. The server gathers the commands from all players and determines what the seal will do now depending on commands distribution.
The connection is made via .NET sockets (TCP) and simple self-made protocol. Chance of doing some action depends on number of players "voting" for it.

Project contains two scenes:
  1. Main - game server (can also work in single-player mode, with the keyboard controls):
    * Simulate the game
    * Exchange information with clients
    * Show game on screen
  2. Client - simple client, that automatically connects to the server (server IP is hardcoded) and sends information about user actions.


## Controls
### PC (singleplayer mode)
WASD to move, Z to attack, X to switch weapons
### Mobile
Joystick to move, buttons to attack and switch weapons
