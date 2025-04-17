# Design decisions & class structure
So my design decision and planning was really horrble. I started out making the map this would take a longer than I would've liked but I did get help through the online research and tutorials. I also tried to think of the class structure kinda like unity, so I thought of it like scene > gameobject > component.
The scene list was hieracrchy with the list of game objects, then those game objects having a list of components with components having the methods. This would've help me more but like I said horrible planning and lack of fully understanding mono game and components as a whole.
Although this project is bad it did help me understand more about game engines, I think this project did teach me the importants of planning and not overthink because I did alot of that.
Overthinking was my downfall on this project, but slow and visual planning out helped with components. But I was still overthinking the player movement and map alot, i'll will continue to do this next time.

PS: sorry for turning this into a refection 

# Additional features
The only thing I think additional feature would be maybe the components thats really it.

# Enemy Design and implention 
For this sprint I had spent most of my time trying to add the map generation, find game object and get component.
So that where most of my time went unfortunately but I got most of it, due to my poor planning last time I didn't think about
enemies so the player and enemy key vector and value don't move.
So I think I'll plan on making a actor class that has all the components I need for them

# Map generation
Like I said I didn't think about how my gameobjects would move around the board and did end up making something
kinda like unity tile map with the check tile so I think I'll try something like set tile for the feature.
The random map generation took a bit mostly because overthinking I did get the wall clusters working great and they leave some space.
my player and enemy were kinda quick fixes but they work, now the doors I will need to try more testing because sometimes they don't spawn.

# Combat System
I didn't have much time in the end for this, but I think I would make a gameobject that holds this system and methods that takes game objects or entity's.
entity that has something like collider or health system as base component.

# GameFlow 
I did fix it and add the visual for enemies getting in stun and they can die aswell as the player. if the player dies you get a bad gameover screen, I didn't have time to add the boss becasue everything broke. On the last day and spent all day trying to fix it which i did in the end. the menus all have working button the game able to restart there is no victory screen just gameover. I didn't have time to test the items that much :\ oh well.  