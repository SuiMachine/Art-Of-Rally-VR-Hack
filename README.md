Art of Rally - VR Hack
============
Currently an experimental VR hack for Art of Rally.

To do
--------
* Stage 2 results (and possible later stage results) are displayed incorrectly after returning to menu on rally completion.
* Some stage intros still seem to spawn camera underground :-/
* Various minor fixes, I guess?

Limitations
--------
* The game renders in Mult-pass stereo rendering, resulting in worse performance. Single-pass seems to cause a black screen.
* Beautify's Depth of Field seems to always render after UI elements, which makes UI unusable for the most part when converted to world space, which is why it's disabled.
* Hx Volumetric Lighting doesn't render correctly with Multi-pass stereo rendering, so it was disabled.

Things that won't happen
--------
* Support for VR controllers. There just isn't any point for it. Just use a gamepad or wheel or something.

Installation
--------
* Copy the files from Release directory into the game directory. Then start the game with ```-vrmode OpenVR``` command-line argument (otherwise it starts it as None).

Additional options
--------
* It's possible to enable low-cam in this hack. To do this run the game at least once, then edit file ```BepInEx/config/ArtOfRallySuiVR.cfg``` and set ```EnableLowCam``` to true. Note that UI may is not designed for cameras like this.

Third-party libraries used:
--------
* OpenVR (as part of Unity)
* [BepInEx 6](https://builds.bepinex.dev/projects/bepinex_be)

Made possible thanks to:
--------
* Raicuparta [YouTube](https://www.youtube.com/watch?v=Gt_kIrmTl44) / [Github](https://github.com/Raicuparta)
* [UABEA project](https://github.com/nesrak1/UABEA/)
