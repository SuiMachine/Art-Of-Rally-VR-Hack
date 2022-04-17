Art of Rally - VR Hack
============
Currently an experimental VR hack for Art of Rally.

To do
--------
* Camera no longer moves in menu, which makes certain elements in menu difficult, as for example you don't see a car you are selecting. This needs to be reworked - probably by teleporting the camera at the end of animator/tween (depending on what's used).
* Frustum culling for vegetation is now disabled (uses only spherical culling based on distance). This has to be corrected to improve performance.
* Correct camera behaviour after stage end.
* Hx Volumetric Lighting seems to be broken :(

Limitations
--------
* Beautify's Depth of Field seems to always render after UI elements, which makes UI unusable for the most part when converted to world space, which is why it's disabled.

Things that won't happen
--------
* Support for VR controllers. There just isn't any point for it. Just use a gamepad or wheel or something.

Installation
--------
* Copy the files from Release directory into the game directory. Then start the game with ```-vrmode OpenVR``` command-line argument (otherwise it starts it as None).