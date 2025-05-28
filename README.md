# Dead Island 2 External Tool

[![Dead Island 2 Memory Tool](https://github.com/lciesielski/DeadIsland2-ExternalTool/actions/workflows/build-dotnet.yml/badge.svg)](https://github.com/lciesielski/DeadIsland2-ExternalTool/actions/workflows/build-dotnet.yml)

An external tool for Dead Island 2, created as a fun exercise with Unreal Engine, offering several in-game advantages.

**Features:**

*   **Player Damage Reduction:** Significantly reduces incoming damage (0.1x modifier), offering a semi-god mode experience.
*   **Increased Player Stamina:** Boosts player stamina to 5000.
*   **Infinite Melee Weapon Durability:** Equipped melee weapons no longer lose durability.
*   **Enhanced Melee Launch Force:** Increases the force applied on impact by equipped melee weapons, sending zombies flying further (effect varies by weapon type).
*   **Guaranteed Critical Hits:** Sets equipped melee weapon critical strike chance to maximum (may require a weapon swap to take effect).

**Optional: One-Shot Kills**
A commented-out section in the source code allows you to set melee weapon damage to 99999 for one-shot kills:
```cpp
/*
externalMemory.Read(AttributesComponent, out UIntPtr DamageAttributes);
DamageAttributes += Offsets.AttributeValueSetStructOffset;
DamageAttributes += Offsets.DamageAttributeStructOffset;
Console.WriteLine($"DamageAttributes: {DamageAttributes:X2}");
SetCurrentMeeleWeaponAttributeType(DamageAttributes, externalMemory);
*/
```

# FAQ

## Usage

1.  **Install .NET 8.0 Desktop Runtime (x64)**: If you don't have it already, download and install the [.NET 8.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/8.0) from Microsoft. (Select the ".NET Desktop Runtime" x64 installer).
2.  Download the latest binary from **Releases**.
3.  Start Dead Island 2.
4.  Load your slayer and enter the game world.
5.  Run the tool's `.exe` file.

## Does it work in COOP?

Untested in COOP. It *may* work if you are the host.

## Why does it run in a loop?

Game memory pointers can change during gameplay (e.g., due to garbage collection). The loop ensures cheats are periodically reapplied to maintain their effects.

# Showcase

Watch the tool in action: [High Quality Video](https://www.youtube.com/watch?v=3rys5Cow3jY)

# Credits

*   [GSpots](https://github.com/Do0ks/GSpots) - GWorld, GNames, and GObjects offset finder.
*   [UEDumper](https://github.com/Spuckwaffel/UEDumper) - Researching internal structure of structs and classes.
*   [Reloaded.Memory](https://github.com/Reloaded-Project/Reloaded.Memory) - Memory editing library for C#.