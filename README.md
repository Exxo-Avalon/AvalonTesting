# Exxo Avalon Origins

Welcome to Exxo Avalon: Origins! Avalon is a vanilla-oriented mod, expanding on content while keeping to a generally vanilla feel. As of now, there are:
- Over 1200 items
- Over 100 NPCs, including 7 bosses and 2 town NPCs (plans for more eventually)
- Several new structures, including Ice Shrines, the Evil Shrine, the Temple of the Observer, the Hellcastle, and the Sky Fortress
- A new world evil, the Contagion, which is an alternate to the Corruption and Crimson
- Plans for the Tropics, an alternate to the Jungle, which will be complete with alternates to everything the Jungle has to offer
- Superhardmode, which is currently unfinished, and is after Hardmode - activated after killing the Wall of Steel, it will increase enemy health, damage, and defense, as well as add several new enemies to the spawnpool
- Plans for a new final boss, fought after the end of Superhardmode, Ultrablivion
- A stamina mechanic, which is completely optional, but allows you to do things such as rocket jumping, teleporting, dashing, sprinting, wall climbing, and swimming without the need for an accessory
- Several new prefixes (including some for armor only)
- Mystical Tomes, which are similar to accessories, but only grant stat boosts

Stay updated and join the discord community: <https://discord.gg/rtm99Uq>

## Todo List

### General

* Update to-do list

### Pre-Release

* Fix even more consumables
* Fix more buffs
* Do late-game world gen
* Make sure some less major stuff works
* Do even more World Gen
* Add NPC Gore
* Do many minor (usually decorative) tile things
* Fix a few NPC AIs
* Implement Dusts
* Fit Avalon progression within the 1.4.x progression
* Fix more bugs
* Add missing stuff
* Fit expert mode items (and maybe AIs) in and disable expert mode
* Do Primordial Ore
* Fix Oblivion

### Extra

* Redo Desert Beak and Bacterium Prime
* Get many sprites updated
* Add better boss health bars (using event progress bars)
* Add Ultrablivion and other TConfig only stuff
* Add Death point mirror
* Clean up code
* Add more content
* Cross mod compatibility for certain mods
* Potion campfires
* Moar campfires, heart lanterns and such
* Add and implement Contagion
* Finish the Tropics

### Easier Recipes

* Chlorophyte armor and weapons

# Compiling shaders on linux

Download fxcompiler and reach
from https://github.com/tModLoader/tModLoader/wiki/Expert-Shader-Guide#compiling-your-shader

Extract fxcompiler.zip and move the fxcompiler_reach.exe to the fxcompiler folder

Create a new wineprefix using the following commnand `WINEPREFIX=~/.dotnet winecfg`

Open winetricks using the command `WINEPREFIX=~/.dotnet winetricks` and select default wineprefix and install component
xna40

Use the the following command to now run fxcompiler_reach.exe `WINEPREFIX=~/.dotnet wine ./fxcompiler_reach.exe`

You can also create a helper shell script containing the following:

```shell
#!/bin/sh
WINEPREFIX=~/.dotnet wine ./fxcompiler_reach.exe
```