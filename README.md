# Project Nebula - Development README

## Welcome to the Project Nebula Development Repository!

This repository houses the codebase and assets for **Project Nebula**, a game currently in development for [The Escape 2024 Game Jam](https://itch.io/jam/the-2024-escape-jam). Below, you'll find guidelines and resources for contributing to the project, setting up your development environment, and understanding the structure of the codebase.
The target platform for this project is the web browser, and thus it will be built for `WebGL`.

## Table of Contents
1. [Getting Started](#getting-started)
2. [Project Structure](#project-structure)
3. [Development Guidelines](#development-guidelines)
4. [Branching Strategy](#branching-strategy)
5. [Coding Standards](#coding-standards)
6. [Assets and Resources](#assets-and-resources)
7. [Testing](#testing)
8. [Issue Tracking](#issue-tracking)
9. [Continuous Integration](#continuous-integration)

## Getting Started

### Prerequisites
- **Unity Version:** Make sure you have the `2023.2.^` version of Unity installed. The project uses the **Universal Render Pipeline (URP)** with 2D Renderer, so ensure your Unity setup supports it.
- **Version Control:** We use Git for version control. Ensure you have Git installed and are familiar with basic commands.

### Cloning the Repository
To get started, clone the repository to your local machine:

```bash
git clone https://github.com/flowmotion-bbd/game-jam-2024-bbd.git
cd game-jam-2024-bbd
```

### Setting Up Unity
1. Open Unity Hub.
2. Click "Add" and navigate to the directory where you cloned the repository.
3. Select the project and open it.

### Installing Dependencies
Upon opening the project, Unity will automatically resolve any package dependencies via the Unity Package Manager. Ensure all packages are installed without errors.

## Project Structure (subject to change)

```
/Assets
    /Art
        /Sprites            # 2D sprite assets
        /Animations         # Animation assets
    /Audio
        /Music              # Sick beats for the game's `vaabs`
        /SFX                # Well... SFX xD
    /Scripts
        /Core               # Core game mechanics and logic
        /UI                 # UI scripts and components
        /Systems            # Game systems like input, saving, etc.
    /Scenes
        /MainMenu           # Main menu scene
        /Level1             # Level (e.g.)
    /Prefabs
        /...                # Sub-directories for different prefab groups
        /UIElements         # Prefabs for UI components
```

## Development Guidelines

### Branching Strategy
We follow the `Trunk` branching model:
- **main**: Contains the main consolodated code base.
- **release/release-name**: Release branches for new releases.
- **feature/feature-name**: Feature branches for new features and enhancements.
- **bugfix/bugfix-name**: Bugfix branches for resolving issues.

### Creating a New Branch
```bash
git checkout main
git pull
git checkout -b feature/your-feature-name
```

### Merging Branches
Once your work is complete and tested, open a pull request to merge your feature branch into `main`. Ensure your branch is up to date with `main` before requesting a merge.

## Coding Standards

### Language and Style
- **Language:** The primary programming language for this project is **C#**.
- **Style Guide:** Follow the [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions) for consistency.

### Naming Conventions
- **Classes and Methods:** Use PascalCase (e.g., `PlayerController`).
- **Variables:** Use camelCase (e.g., `playerHealth`).
- **Constants:** Use SCREAMING_SNAKE_CASE (e.g., `MAX_HEALTH`).

### Comments
- Comment only on complex sections of code to explain the logic if it is not straight forward.

## Assets and Resources

### Art and Audio
- **Art Assets:** Store all sprite assets in the `/Assets/Art/Sprites` directory.
- **Audio Files:** Store all audio assets in `/Assets/Audio`. Ensure audio files are appropriately compressed for optimal performance.

### Asset Import Settings
- **Sprites:** Ensure sprites are imported with appropriate compression and pixel settings to maintain performance and visual quality.
- **Audio:** Compress audio files and set import settings to optimize for platform performance.

## Testing

### Playtesting
Regular playtesting is essential. All new features should be playtested locally before merging into the `dev` branch.

## Issue Tracking

### Reporting Bugs
Please report bugs using the issue tracker on GitHub. Include detailed steps to reproduce the issue, and tag the issue appropriately (e.g., `bug`, `high-priority`).

### Feature Requests
Feature requests can be submitted via the issue tracker. Tag requests with `enhancement` for visibility.

## Continuous Integration

The project is (will be) set up with continuous integration (CI) to run tests and build the game automatically. Ensure your code passes all tests before pushing to the repository.

Happy coding!

**The Project Nebula Development Team**
