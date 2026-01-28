# Roleplay Overhaul Mod (Mega Edition)

A comprehensive, persistent, and feature-rich Single Player Roleplay mod for GTA V, built using ScriptHookVDotNet.
This mod integrates over 20,000 lines of code and data to transform GTA V into a true Life RPG.

## Features

### 🎒 Grid-Based Inventory (vHUD Inspired)
- **Interactive:** Click items to eat, equip, or use them.
- **HUD:** Real-time bars for Hunger (Orange), Thirst (Blue), and Fatigue (Gray).
- **Speedometer:** Displays speed and fuel when driving.

### 💼 30+ Unique Jobs
Experience a variety of careers with specialized mechanics:
- **Delivery:** Pizza, Courier, Trucking, Mail (PostOp).
- **Emergency:** Police (Arrests/Scanning), Paramedic (Reviving), Firefighter (Extinguishing).
- **Services:** Taxi (Fare Meter), Tow Truck (Hooking), Mechanic.
- **Manual Labor:** Garbage Collector (Routes), Miner, Lumberjack.
- **Criminal:** Drug Dealing, Heists (Fleeca Bank).

### 🏛️ RealBank Economy
- **Banking App:** Press **B** or use an ATM to manage finances.
- **Auto-Income:** Salary and business profits are deposited automatically.
- **Bills & Debt:** Pay rent and utilities or face mounting debt.
- **Businesses:** Buy properties like the Nightclub or Weed Farm for passive income.

### 👮 Advanced Police AI (LSR Style)
- **Heat System:** A 0-100% heat scale separate from vanilla stars.
- **Tactics:** Police use Roadblocks, Spike Strips, and Air Support at high heat.
- **Dispatch:** Regional units (Sheriff in Blaine County, LSPD in City).

### 🍔 Survival & Persistence
- **Needs:** Maintain Hunger, Thirst, and Sleep levels.
- **Cooking:** Combine ingredients to create better food.
- **Saving:** Player position, health, vehicle damage, and wardrobe are saved automatically.
- **Vehicles:** Owned cars persist in the world with exact damage and fuel levels.

### 🌍 Living World
- **Traffic:** Dynamic density and speed limits based on 2,000+ data nodes.
- **Population:** 5,000+ unique citizens with persistent names, jobs, and wealth.
- **Properties:** Buy or rent hundreds of generated houses and apartments.

## Installation

1. Ensure you have **ScriptHookV** and **ScriptHookVDotNet (v3 Nightly)** installed.
2. Copy the contents of `src/RoleplayOverhaul` into your `GTA V/scripts/` directory.
   > **Important:** Delete `src/Dependencies/SHVDN_Stubs.cs` before running the game. This file is for development only.
3. Launch the game.

## Controls
- **F5**: Open/Close Inventory (Backpack)
- **E**: Interact (ATM, Banker, Job Objective)
- **B**: Open Banking App
- **L**: Surrender to Police (When Wanted)
- **H**: Start Fleeca Heist (Debug)
- **Z**: Start Zombie Survival (Minigame)

## Developer Notes
This mod is structured to be modular.
- `Core/`: Global systems (Business, Career, Wardrobe).
- `Data/`: Massive generated databases for World/Items.
- `Jobs/`: Specific logic for all 30+ jobs.
- `Police/`: AI Tactics and Dispatch.
- `UI/`: Custom rendered menus.
