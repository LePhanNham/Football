# sixs-unity-dev-test

A small Unity football/kicking prototype focused on ball interaction, goal detection, camera handoff, and lightweight gameplay feedback.

## Overview

This project demonstrates a simple football-style loop:

- Move the player around the field
- Kick the nearest available ball manually
- Use auto-kick for assisted targeting when a valid ball is ready
- Detect goals and trigger camera, VFX, and UI feedback
- Prevent invalid kicks while the camera is transitioning

## Built With

- Unity 6000.3.10f1
- Cinemachine 3.1.6
- UGUI
- DOTween
- Visual Scripting package installed

## Key Features

- Manual kick and auto-kick actions
- Ball selection rules that ignore scored balls
- Camera handoff between player and ball during goal events
- Temporary gameplay message feedback for blocked actions
- Goal handling with delayed ball cleanup

## Project Structure

- `Assets/_GAME/Scripts/` - Core gameplay scripts
- `Assets/_GAME/Scripts/Manager/` - Game flow and camera-related managers
- `Assets/_GAME/Scripts/UI/` - In-game canvas and feedback UI
- `Assets/Plugins/` - Third-party packages and imported assets

## Getting Started

1. Open the project in Unity 6000.3.10f1 or a compatible Unity 6 editor.
2. Open the default scene defined in the project settings.
3. Press Play.
4. Use the player controls to move around and kick the ball.

## Controls

- `Horizontal` / `Vertical` - Move the player
- Kick button - Kick the closest valid ball
- Auto kick button - Auto-target the farthest valid ball that is ready
- Reset button - Reload the current scene

## Gameplay Rules

- A ball that has already scored is ignored by future kick selection.
- Manual kick and auto kick are blocked while the camera is still returning to the player.
- After a goal, the scored ball is left visible briefly, then removed from the scene.
- If no valid ball is available, the UI shows a blocked-action message.

## Technical Notes

- Ball and goal logic are driven by collision and trigger events.
- Camera transitions are coordinated through `CameraFollower`.
- Gameplay actions are dispatched through `PlayerAction` events.
- `GameManager` is responsible for selecting valid balls and targets.

## Packages

The project includes several Unity packages through the Package Manager, including:

- Cinemachine
- UGUI
- DOTween-related assets in the project
- Test Framework
- Visual Scripting

## Notes

This repository is a gameplay prototype rather than a full production game. The code is intentionally small and focused so the main interactions are easy to inspect and iterate on.

## License

No explicit license has been provided in the repository.
