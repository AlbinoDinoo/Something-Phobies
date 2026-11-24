PSX Abandoned Hospital Pack (Free) v1.0
by Nickben12

18 free PS1 style models to start an abandoned hospital: modular ward walls, a bed, a wheelchair, an IV stand and more, with tiny dithered textures. Ready for Unity, Unreal and Godot.

CONTENTS (18 models)
  Structure (6)
    Wall_2m_01                  52 tris    2.00 x  0.17 x  2.60 m   256 px
    Wall_Door_2m_01            174 tris    2.00 x  0.18 x  2.60 m   256 px
    Wall_Window_Barred_2m_01   254 tris    2.00 x  0.23 x  2.60 m   256 px
    Door_Ward_01                42 tris    1.06 x  0.10 x  2.10 m   256 px
    Floor_Tile_2m_01            12 tris    2.00 x  2.00 x  0.04 m   256 px
    Ceiling_Tile_2m_01          12 tris    2.00 x  2.00 x  0.05 m   256 px
  Ward (5)
    Light_Fluorescent_01        64 tris    1.24 x  0.32 x  0.13 m   128 px
    Bed_Hospital_01            382 tris    1.00 x  2.21 x  1.07 m   256 px
    Nightstand_01               70 tris    0.50 x  0.44 x  0.76 m   128 px
    Folding_Screen_01          372 tris    1.75 x  0.35 x  1.81 m   256 px
    Wheelchair_01              664 tris    0.69 x  0.95 x  0.96 m   256 px
  Medical (2)
    Oxygen_Tank_01             256 tris    0.38 x  0.33 x  1.18 m   128 px
    IV_Stand_01                258 tris    0.54 x  0.57 x  1.95 m   128 px
  Clutter (4)
    Clipboard_01                22 tris    0.23 x  0.33 x  0.02 m   64 px
    Pill_Bottles_01            232 tris    0.28 x  0.27 x  0.10 m   128 px
    Bin_Biohazard_01            32 tris    0.41 x  0.49 x  0.65 m   128 px
    Sign_Exit_01                12 tris    0.48 x  0.10 x  0.19 m   128 px
  Horror (1)
    Blood_Pool_01               80 tris    0.89 x  0.92 x  0.00 m   128 px

MORE
  This is the free version. The Extra version has all 55 models (37 more) and the Blender source file: https://nickben12.itch.io/psx-abandoned-hospital-pack

FOLDERS
  FBX/                  one .fbx per model (Unity, Unreal, most engines)
  GLB/                  one .glb per model, texture embedded, nearest filtering (Godot, three.js...)
  OBJ/                  one .obj + .mtl per model
  Textures/             <Model>_Albedo.png: one tiny texture per model (64 to 256 px,
                        32-colour palette, dithered, dirt and shading painted in)
  Previews/             a preview image of every model

TECHNICAL NOTES
  - Real-world scale: 1 unit = 1 metre.
  - Pivot at the bottom centre of every model, front facing -Y in Blender
    (-Z in Unity / Godot after import).
  - Modular pieces (walls, floor, ceiling) snap on a 2 m grid; walls are 2.6 m high and
    0.15 m thick and finished on both sides, so one wall serves two rooms.
  - Wall-mounted items (signs, X-ray viewer, handrail, extinguisher, scrub sink) have
    their back at Y = 0. Floor tiles, ceiling tiles and ceiling lights have their top at height 0.
  - Doors have their pivot at the bottom centre and fit the matching wall openings.
  - Every model: one mesh, one material, one albedo texture. No normal or PBR maps:
    the PS1 look comes from the low-res, dithered textures.

GETTING THE PS1 LOOK
  - Texture filtering: Point / Nearest, no mipmaps, no compression.
  - Unlit or simple vertex-lit materials, low render resolution (e.g. 320x240 upscaled),
    optional vertex snapping and affine texture mapping shaders (engine side).

IMPORT TIPS
  Unity   : drag the FBX and Textures folders into Assets, set the textures to
            Filter Mode: Point, Compression: None, Generate Mip Maps: off.
  Unreal  : import the FBX files with their textures, set Texture Group to '2D Pixels'.
  Godot 4 : drop the GLB files into your project (nearest filtering is already set).

LICENSE
  See LICENSE.txt. Thank you for downloading!
