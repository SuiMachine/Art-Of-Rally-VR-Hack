Art of Rally - VR Hack
============
Currently an experimental VR hack for Art of Rally.

To do
--------
* Camera is not always ending up in the correct position after initiation - this has to be fixed (pressing F8 should recenter the camera).
* Many canvases has to be reworked, as their scaling breaks, when converting them from screen space to world space.
* Frustum culling for vegetation is now disabled (uses only spherical culling based on distance). This has to be corrected to improve performance.

Limitations
--------
* Beautify's Depth of Field seems to always render after UI elements, which makes UI unusable for the most part when converted to world space, which is why it's disabled.

Things that won't happen
--------
* Support for VR controllers. There just isn't any point for it. Just use a gamepad or wheel or something.

Installation
--------
* Copy the files from Release directory into the game directory. Then start the game with ```-vrmode OpenVR``` command-line argument (otherwise it starts it as None).