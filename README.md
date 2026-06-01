# ALU 360 Virtual Campus Tour

A Unity-based interactive 360° virtual tour application that allows users to explore two different campus environments — an **Intranet Tour** featuring pre-recorded 360° videos of campus spaces, and a **Custom Campus Tour** built from 360° images of the actual campus.

---

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Scenes](#scenes)
- [Project Structure](#project-structure)
- [Scripts](#scripts)
- [How It Works](#how-it-works)
- [Controls](#controls)
- [Requirements](#requirements)
- [How to Run](#how-to-run)
- [How to Build](#how-to-build)
- [Known Limitations](#known-limitations)
- [Authors](#authors)

---

## Project Overview

This project is a **360° virtual tour** built with Unity 6 using the **Universal Render Pipeline (URP)**. It was developed as part of the ALU (African Leadership University) curriculum to give prospective students, visitors, and the public an immersive way to explore the campus without being physically present.

The application has two tour modes:
- **Intranet Tour** — uses 360° video footage of 4 campus spaces rendered onto inverted spheres
- **Custom Campus Tour** — uses 360° images of 3 real campus locations rendered onto inverted spheres

Users navigate between rooms/locations using hotspot buttons placed inside each sphere, and can access information panels about each space.

---

## Features

- 🎥 360° video playback inside inverted spheres (Intranet Tour)
- 🖼️ 360° image-based tour (Custom Campus Tour)
- 🔀 Smooth room/location switching with optional fade animation
- 📍 Hotspot navigation buttons inside each sphere
- ℹ️ Information panels with descriptive text for each location
- 🖱️ Mouse-look camera rotation for immersive exploration
- 🏠 Main Menu with navigation to both tours and an exit button
- 🔁 In-tour navigation buttons to switch between tours or return to Main Menu

---

## Scenes

The project contains the following scenes, which must be in this order in Build Profiles:

| Index | Scene Name | Description |
|-------|-----------|-------------|
| 0 | `MainMenuScene` | Entry point — contains buttons to launch either tour or quit |
| 1 | `IntranetTour` | 360° video tour of 4 campus spaces |
| 2 | `CustomCampusTour` | 360° image tour of 3 real campus locations |

---

## Project Structure

```
Assets/
├── Animations/         # FadeIn and FadeOut animations for scene transitions
├── Audio/              # Background music and audio mixer
├── Images/             # Hotspot and info button UI sprites
├── Materials/          # Sphere materials (URP Unlit, mapped to RenderTextures)
├── Scenes/             # All Unity scene files
├── Scripts/            # All C# scripts (see Scripts section)
├── Settings/           # URP pipeline asset configurations
├── Textures/           # RenderTexture assets for video output
├── Videos/             # 360° video files (.mp4) for the Intranet Tour
└── XR/                 # XR/OpenXR settings (included from template)
```

---

## Scripts

### `MainMenuController.cs`
Handles all scene navigation throughout the application. Attach this to any GameObject in any scene.

| Method | Description |
|--------|-------------|
| `LoadMainMenu()` | Loads the Main Menu scene |
| `LoadIntranetTour()` | Loads the Intranet Tour scene |
| `LoadCustomCampusTour()` | Loads the Custom Campus Tour scene |
| `QuitGame()` | Exits the application (stops Play Mode in Editor) |

---

### `SwitchRooms.cs`
Used exclusively in the **IntranetTour** scene. Manages switching between 4 video spheres with optional fade transition.

**Inspector fields to assign:**
- `livingRoomSphere`, `cantinaSphere`, `cubeSphere`, `mezzanineSphere` — the 4 sphere GameObjects
- `cantinaHotspot`, `livingRoomHotspot`, `cubeHotspotFromLiving`, `cubeHotspotFromCantina`, `cubeHotspotFromMezzanine`, `mezzanineHotspot` — UI Buttons
- `fadeAnimator` — optional Animator for fade effect

**Public methods (usable in button OnClick):**
- `SwitchToCantina()`, `SwitchToLivingRoom()`, `SwitchToCube()`, `SwitchToMezzanine()`
- `SwitchSphere(GameObject)` — switches to any sphere directly

---

### `SwitchRoomsCampus.cs`
Used exclusively in the **CustomCampusTour** scene. Manages switching between 3 image spheres.

**Inspector fields to assign:**
- `sphere1`, `sphere2`, `sphere3` — the 3 campus sphere GameObjects
- `hotspotTo1`, `hotspotTo2`, `hotspotTo3` — UI Buttons for navigation
- `fadeAnimator` — optional Animator for fade effect

**Public methods (usable in button OnClick):**
- `SwitchToSphere1()`, `SwitchToSphere2()`, `SwitchToSphere3()`

---

### `CursorLook.cs`
Attached to the `CameraRig` or `MouseController` GameObject. Handles horizontal mouse-look rotation and raycasting for hotspot button interaction.

**Inspector fields:**
- `mouseSensitivity` — rotation speed (default: 100)
- `playerCamera` — reference to the Main Camera transform
- `rayDistance` — how far the raycast reaches (default: 100)

---

### `FlipNormals.cs`
Attached to each sphere GameObject. Flips the mesh normals on `Awake()` so the sphere is visible from the inside (where the camera sits).

**Inspector field:**
- `objectToFlip` — the sphere GameObject whose normals will be flipped

---

### `InfoButtonScript.cs`
Attached to info button GameObjects inside each sphere's Canvas. Toggles the visibility of an information panel.

**Inspector fields:**
- `infoBox` — the panel GameObject to show/hide
- `infoButton` — the Button that triggers the toggle

---

## How It Works

### 360° Video Spheres (Intranet Tour)
1. Each room is a Unity **Sphere** scaled to 30x30x30
2. `FlipNormals.cs` inverts the sphere normals so it renders from the inside
3. A **VideoPlayer** component on each sphere plays a `.mp4` file into a **RenderTexture**
4. The sphere's material (URP Unlit) uses that RenderTexture as its main texture
5. The camera sits at the center of the active sphere
6. Only one sphere is active at a time — `SwitchRooms.cs` handles activation/deactivation

### 360° Image Spheres (Custom Campus Tour)
Same setup as above but uses static 360° image textures instead of video RenderTextures, managed by `SwitchRoomsCampus.cs`.

### Navigation Flow
```
MainMenuScene
    ├── [Intranet Tour Button]  →  IntranetTour
    │                               ├── [Go to Campus Tour Button]  →  CustomCampusTour
    │                               └── [Back to Main Menu Button]  →  MainMenuScene
    ├── [Custom Campus Tour Button]  →  CustomCampusTour
    │                                    ├── [Go to Intranet Tour Button]  →  IntranetTour
    │                                    └── [Back to Main Menu Button]  →  MainMenuScene
    └── [Exit Button]  →  Quit
```

---

## Controls

| Action | Control |
|--------|---------|
| Look around | Move the mouse left/right |
| Navigate to another room | Click a hotspot button |
| View location info | Click an info button (ℹ️) |
| Switch tours | Click the tour navigation button |
| Return to Main Menu | Click the Main Menu button |

---

## Requirements

- **Unity 6** (6000.x or later)
- **Universal Render Pipeline (URP)** package
- **TextMeshPro** package
- **Input System** package
- Windows 10/11 (for standalone build)

---

## How to Run

1. Open the project in **Unity 6**
2. Go to **File → Build Profiles** and ensure all 3 scenes are listed and enabled:
   - `MainMenuScene` (index 0)
   - `IntranetTour` (index 1)
   - `CustomCampusTour` (index 2)
3. Open `MainMenuScene` in the Editor
4. Press **Play**

---

## How to Build

1. Go to **File → Build Profiles**
2. Select **Windows Standalone** as the platform
3. Ensure all 3 scenes are in the scene list
4. Click **Build And Run**
5. Choose an output folder — a `.exe` file will be generated

---

## Known Limitations

- Mouse-look only rotates horizontally (left/right). Vertical look is not implemented
- The fade transition only works if a `fadeAnimator` is assigned in the Inspector
- `Application.Quit()` only works in a built executable — in the Editor it stops Play Mode instead
- 360° videos may take a moment to start playing on first load depending on system performance

---

## Authors
- **Blandine Iradukunda** — 

Developed as part of the **ALU 0x0A Unity 360 Video Tour** project.
