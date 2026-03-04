Functional Requirements
Room:

The game world is a fixed-size 20x40 grid.
Each cell of the world can be:
empty ( ),
a wall (█),
occupied by the player (¶),
containing multiple items (any symbol, depending on the items).
Player's starting position: (0, 0).
At this stage, generating a labyrinth or randomly placing items is not required. The world can have predefined placements for items and walls.
Player:

Moves in four directions (controlled by W, S, A, D).
Cannot move beyond the world boundaries.
Cannot pass through walls.
Has various attributes such as Strength, Dexterity, Health, Luck, Aggression, and Wisdom.
Has two hands, each capable of holding an item.
Items:

The room contains predefined items coded within the program.
There are at least three types of weapons, including one two-handed weapon. Weapons have damage value.
There are at least three types of unusable items.
There are two types of currency: coins and gold.
Each weapon has a damage value (weapon usage is not implemented at this stage, only equipping is).
If the player steps on a tile with an item, they can pick it up by pressing E.
Game State Display:

The world is drawn in the console.
Next to the world, the following information is displayed:
inventory,
currently equipped items,
if the player is standing on an item, information about it,
the player’s current attribute values,
the number of collected coins and gold.
Inventory Management:

The player can manage their inventory by:
dropping items on the ground,
equipping and unequipping items in both hands (correct handling of two-handed weapons is required).
Remarks
The code must be object-oriented.
Adding a new type of: weapon, item, board tile etc. should be easy.
The use of runtime type identification is prohibited (e.g., is, as, typeof, RTTI in C++), as well as using enumerations to determine the specific type of an object.
Each use of prohibited language features results in a penalty of -1 point and requires correction in later stages of the project.
The game board should be displayed in a way that ensures a positive gameplay experience (in particular, make sure that the screen does not flicker or jump after each move and that changes in the game state are reflected reasonably quickly).