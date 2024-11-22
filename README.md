# Tank-Rush 
10th and 11th November 2024
48h hackathon style game assignment
Known bug - Changing the weapon too fast will block the weapons
	  - UI for Weapons don't work, cooldown not shown
All knows bugs are related to the Weapon manager, with more time, the game should
be bug free.

Difficulty: every 15 seconds, one of these four action will happen
 - spawn of enemies increases
 - Enemy damage increases
 - Enemy health increases
 - Boss damage and speed increases

Tank Mouvement resembles known games like World of Tank and Battlefield
The mouse moves first, and then the turret follows the aim. Turret have a 
dynamic crosshair that shows where the turret points. Tank will accelerate 
and decelerate with keyboard keys steering right and left.

 - Minimap
 - Cute particles
 - Enemy and player heatlh bar displayed
 - Spawns are random, enemies will spawn arround the player 
	and also follow these rules : 
	Tank traps have higher chance to spawn near each other
	Mortars and Tanks have higher chance to spawn near towers
	Towers will spawn as far as possible to cover the most area

* No tutorial, no hints, no difficulty progression from a game to another
* Minimal level design
