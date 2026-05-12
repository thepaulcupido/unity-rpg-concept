
# unity-rpg-concept

A minimal Unity prototype for a 2D RPG concept. This repository contains a small player controller, an Input System actions asset and a set of 2D/URP related packages in Packages/manifest.json for rapid iteration.

Contents
- Assets/Scripts/PlayerController.cs — simple 2D player movement using a Rigidbody2D.
- Assets/InputSystem_Actions.inputactions — Input System action map (Player and UI maps).
- Assets/Settings/Lit2DSceneTemplate.scenetemplate — editor scene template asset used by the project.
- Packages/manifest.json — package list required by the project.

Quick start
1. Open this folder in the Unity Editor (recommended: use the Unity Hub to open the project). The project expects the packages declared in Packages/manifest.json and will prompt to resolve them on first load.
2. Open the sample scene or create a new 2D scene and add a GameObject with a Rigidbody2D and the PlayerController component.
3. Assign the Rigidbody2D reference on the PlayerController and adjust Movement Speed as needed.

Controls
- The PlayerController currently reads input with the legacy Input API (Input.GetAxisRaw for Horizontal/Vertical) and applies velocity to the Rigidbody2D.
- An Input System actions asset is included (Assets/InputSystem_Actions.inputactions) with a Player map (Move, Look, Attack, Interact, Crouch, Jump, etc.). The project contains the Input System package in Packages/manifest.json if you prefer to migrate to the newer API.

Notes
- This repository is a concept/prototype and intentionally minimal — use it as a starting point for a 2D RPG prototype.

Contributing
- Make changes on a branch and open pull requests. Keep changes small and focused.

Related files to inspect
- Assets/Scripts/PlayerController.cs
- Assets/InputSystem_Actions.inputactions
- Packages/manifest.json

License
- This project is licensed under the MIT License. See the LICENSE file in the repository root for full terms.
