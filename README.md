Art of Rally - VR Hack
============
A simple VR hack for Art of Rally. Please do not report any problems caused by this hack to the game developers - respect their time!

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
* Download the release files and make sure to extract it (do not try launching UnityVRPatcher.exe without extracting it).
* Go to **Art of Rally** directory.
* Drag and drop the game's exe onto UnityVRPatcher.exe - this should patch all the necessery files and also copy the content of the release to appropriate directories within the game.
* Once you want to play the game in VR mode, launch the game with ```-vrmode OpenVR```command-line argument - refer to this page for how to use them https://www.pcgamingwiki.com/wiki/Glossary:Command_line_arguments. Launching the game without it, will start the game normally without modifications applied.

If a patch comes out and gamemanagers changes, just run a VR patcher again on the exe.

Additional options
--------
* It's possible to enable low-cam in this hack. To do this run the game at least once, then edit file ```BepInEx/config/ArtOfRallySuiVR.cfg``` and set ```EnableLowCam``` to true. Note that UI may is not designed for cameras like this.

Third-party libraries used:
--------
* OpenVR (as part of Unity)
* [BepInEx 6](https://builds.bepinex.dev/projects/bepinex_be)

Made possible thanks to:
--------
* Raicuparta [Github](https://github.com/Raicuparta) and his [unity-vr-patcher](https://github.com/Raicuparta/unity-vr-patcher).
* [UABEA project](https://github.com/nesrak1/UABEA/)
