# Pi3DW Mini-project - fnorre23


## Overview of the Game
The game is inspired by Baldurs Gate Dark Alliance II, with a top down angled view, with a camera that rotates around the player, like a sort of spotlight. 
It is a dungeon crawler practice, where the goal is to shoot all targets, so you have to find them in order to shoot them. This way, you practice checking everything. 
By shooting the targets, you gain speed, so you can find the other targets faster.

The main parts of the game are:
- Player - Pill, moved with WASD
- Camera - following the player, and rotating around the player with Q and E. If a wall obstructs the player, it turns see through, so the player always can see the character.
- Fireball shooting - Shoot a fireball by pressing F. The fireball uses a particle system to give some movement.
- Targets - Shoot targets to gain speed. Shoot all targets to win.
- Dungeon - A small dungeon layout for the player to find the targets.


Game features:
- The game incentivises to explore the entire map, by hiding targets, and forcing to use the camera to discover everything
- The game keeps track of how many targets are left


## Running It
1. Download Unity >= 6000.0.23f1
2. Clone or Download the project
3. The game requires a computer with a mouse and keyboard


## Project Parts

### Scripts
- CameraFollow - used for following the player character
- CameraOrbit - used to handle rotation of the camera around the player
- Fireball - used to give the fireball properties
- FireballShooter - used to handle how whatever shoots the fireball 
- GameManager - handles game-winning related code, such as keeping track of targets, and turning on win screen when no targets are left.
- PlayerMovement2 - used to handle player movement
- Targhet - used to handle the targets’ properties
- WallVisibility - used to handle how the walls turn invisible to see the player character.


### Models & Prefabs
- A model of the cheese downloaded from [sketchfab](https://sketchfab.com/3d-models/cheese-78642517ca7e43b495e73509810fbbe1)
- Rat and cat models made with Unity primitives

| **Task**                                                                | **Time it Took (in hours)** |
|--------------------------------------------------------------------------------|------------------------------------|
|     Setting up   Unity, making a project in GitHub                             |     0.25                           |
|     Making character move + camera follow                                      |     2.5                            |
|     Turning walls invisible when between camera and player                     |     1.5                            |
|     Adding fireball                                                            |     0.5                            |
|     Level creation                                                             |     1                              |
|     Target creation + GameManager                                              |     0.75                           |
|     Final touches                                                              |     1                              |                                                                           |
|     Making readme                                                              |     0.5                            |
|     **All**                                                                    |     **8**                          |

## References
- Getting character moving: https://www.youtube.com/watch?v=ONlMEZs9Rgw 
- Probuilder help https://www.youtube.com/watch?app=desktop&v=uSZUgs8UGEs&t=0s 





