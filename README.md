Art of Rally - VR Hack
============
Currently an experimental VR hack for Art of Rally.

To do
--------
* Camera should now end up in correct position, but its orientation is not always correct (especially in main menu). When in-game, you can press F8 to reorient it (there is no workaround for menu)
* Frustum culling for vegetation is now disabled (uses only spherical culling based on distance). This has to be corrected to improve performance.
* Correct camera behaviour after stage end.
* Correct camera behaviour at the beginning of the stage (during intro).

Limitations
--------
* Beautify's Depth of Field seems to always render after UI elements, which makes UI unusable for the most part when converted to world space, which is why it's disabled.

Things that won't happen
--------
* Support for VR controllers. There just isn't any point for it. Just use a gamepad or wheel or something.

Installation
--------
* Copy the files from Release directory into the game directory. Then start the game with ```-vrmode OpenVR``` command-line argument (otherwise it starts it as None).