# 🧛‍♂️ SurvivorLite - 2D Survival Arena Game

[![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20WebGL-lightgrey.svg)]()

A complete, playable Vampire Survivors-style ARPG built in Unity with auto-attacks, escalating waves, leveling & upgrades. **Ready to play in 2 minutes!**

![SurvivorLite Gameplay](https://via.placeholder.com/800x400/1a1a2e/eee?text=SurvivorLite+Gameplay+Demo)

## 🚀 Quick Start (2 Minutes)

## Quick Start

### Option 1: Clone & Play (Recommended)
```bash
git clone https://github.com/yourusername/SurvivorLite.git
cd SurvivorLite
```

1. **Open Unity Hub** → **"Open"** → Select the cloned folder
2. **Wait for import** (30-60 seconds)  
3. **Unity Menu**: `SurvivorLite → Auto Setup Complete Scene`
4. **Press Play ▶️**

### Option 2: Manual Setup (15-25 minutes)

1. **Create New Unity Project**
   - Unity Hub → New → 2D (URP optional)
   - Name: SurvivorLite

2. **Project Settings**
   - Time → Fixed Timestep = 0.02 (50 FPS physics)
   - Physics 2D → Queries Hit Triggers = ON
   - Input System: Legacy is fine
   - VSync: OFF, Target Frame Rate: 60

3. **Tags & Layers**
   - Tags: Add `Player`, `Enemy`, `Projectile`, `Pickup`
   - Sorting Layers: Background, Props, Player, Enemies, VFX, UI

### Scene Setup

1. **Create Arena Scene**
   - New Scene → Save as `sc_Arena`
   - Add large background sprite (scale 40x40) or Tilemap

2. **Player Setup**
   - Create empty `Player` GameObject
   - Add: SpriteRenderer, Rigidbody2D, Collider2D (Circle)
   - Add scripts: PlayerController2D, Health, Experience, WeaponSystem
   - Set Tag = "Player"
   - Rigidbody2D: Gravity Scale = 0, Freeze Z Rotation = true
   - Collider2D: Is Trigger = false

3. **Camera Setup**
   - Main Camera → Add CameraFollow2D
   - Assign target = Player

4. **Object Pools**
   - Create empty `ProjectilePool` → Add ObjectPool script
   - Create empty `EnemyPool` → Add ObjectPool script  
   - Create empty `XPPool` → Add ObjectPool script

5. **Prefabs** (Create in Assets/Prefabs/)
   - `pf_Projectile`: SpriteRenderer + Collider2D (trigger) + Projectile script
   - `pf_Enemy`: SpriteRenderer + Rigidbody2D + Collider2D + EnemyController + Health + DropOnDeath
   - `pf_XP`: SpriteRenderer + Collider2D (trigger) + PickupXP script
   - Set Enemy Tag = "Enemy"

6. **Spawner Setup**
   - Create empty `Spawner` → Add Spawner script
   - Assign: enemyPool, player, worldCamera, set spawnInterval=1.0, ringRadius=14

7. **UI Setup**
   - Create Canvas (Screen Space - Overlay)
   - Add two Sliders: HPBar, XPBar (remove interactable)
   - Optional: Add TextMeshPro for timer
   - Add UIHud script to Canvas, assign references

### Generate Content

1. **In Unity Menu**: `SurvivorLite → Generate Default Upgrades`
2. **Create UpgradeDatabase** in scene, drag generated upgrades to list

## Core Scripts Overview

### Movement & Combat
- `PlayerController2D` - WASD movement with Rigidbody2D
- `WeaponSystem` - Auto-targeting projectile firing
- `EnemyController` - Chase AI with contact damage
- `Health` - Damage/death system for all entities

### Progression
- `Experience` - XP tracking and level-up events
- `PlayerStats` - Centralized stat modifications
- `UpgradeDef` - ScriptableObject for upgrade definitions
- `UpgradeDatabase` - Collection of all available upgrades

### Utilities
- `ObjectPool` - Memory-efficient object recycling
- `WeightedRandom` - Rarity-based selection system
- `Spawner` - Off-screen enemy spawning

## Development Roadmap

### Week 1 ✅ - Core Loop
- [x] Player movement (WASD)
- [x] Auto-attacking weapon system
- [x] Enemy spawning and AI
- [x] Health/damage system
- [x] XP collection and leveling
- [x] Basic UI (HP/XP bars, timer)

### Analytics & Settings ✅ - Essential Systems
- [x] CSV runtime logger (minute-by-minute balance data)
- [x] Save/load system with persistent settings
- [x] Audio mixer integration with volume controls
- [x] Stats tracking (damage by source, kills, time)
- [x] Pause menu with settings access

### Week 2 - Progression System
- [ ] Level-up draft UI (choose 1 of 3 upgrades)
- [ ] Upgrade application system
- [ ] Rarity weights and reroll mechanics
- [ ] Difficulty scaling over time

### Week 3 - Content & Variety
- [ ] Multiple weapon types
- [ ] Enemy variety (6+ types)
- [ ] Boss encounters
- [ ] Visual effects and juice

### Week 4 - Polish & Ship
- [ ] Main menu and settings
- [ ] Final balance pass
- [ ] Build and deployment

## Performance Guidelines

- All projectiles/enemies use ObjectPool
- Avoid Instantiate/Destroy during combat
- Target 60 FPS on mid-range hardware
- Keep GC spikes under 2ms average

## Controls

- **WASD** - Movement
- **ESC** - Pause menu / Settings
- **Auto-attack** - Targets nearest enemy
- **Level-up** - Choose upgrade from 3 options

## Technical Notes

- Unity 2022.3+ LTS recommended
- 2D physics with top-down perspective
- Data-driven design using ScriptableObjects
- Modular upgrade system for easy balancing

---

*Target: 4 weeks part-time development for vertical slice*
#
# 📊 Analytics & Balance Tools

### CSV Runtime Logger
- **Location**: `<persistentDataPath>/logs/<timestamp>/`
- **Files**: 
  - `summary.csv` - Time, kills, enemies alive, total damage per minute
  - `damage_by_source.csv` - DPS breakdown by weapon/source
- **Usage**: Attach `RuntimeCSVLogger` to Analytics GameObject, set interval (default 60s)

### Audio System Setup
1. **Create Audio Mixer**: Assets → Create → Audio Mixer → "MasterMixer"
2. **Expose Parameters**: Right-click volume knobs → Expose:
   - `MasterVol` (Master group)
   - `MusicVol` (Music child group)  
   - `SFXVol` (SFX child group)
3. **Route Audio Sources**: Assign music/SFX sources to appropriate mixer groups
4. **Settings UI**: Create sliders (0-1 range) and bind with `SettingsMenu`
5. **Boot Audio**: Add `AudioBoot` to startup scene for immediate volume application

### Save System
- **File**: `<persistentDataPath>/survivorlite_save.json`
- **Data**: Best time, games played, volume settings
- **Usage**: `SaveSystem.I.Data` for access, `SaveSystem.I.Save()` to persist

### Stats Tracking
- **Access**: `StatsTracker.I` singleton
- **Metrics**: Run time, kills, damage by weapon source
- **Integration**: Automatic damage/kill tracking via `Projectile` hits

## 🎛️ Settings & Pause System

### Pause Menu (ESC key)
- Time scaling management
- Settings panel access
- Restart/quit options
- Preserves level-up UI state

### Settings Menu
- Master/Music/SFX volume sliders
- Fullscreen toggle
- Real-time audio preview
- Persistent across sessions

---

## 🔧 Quick Setup Checklist

### Core Systems
- [ ] Copy all scripts to `Assets/Scripts/`
- [ ] Run `SurvivorLite → Generate Default Upgrades`
- [ ] Set up Player prefab with all components
- [ ] Create object pools for projectiles/enemies/XP
- [ ] Configure spawner and UI systems

### Analytics Setup
- [ ] Add `StatsTracker` to boot scene (DontDestroyOnLoad)
- [ ] Add `SaveSystem` to boot scene (DontDestroyOnLoad)  
- [ ] Add `RuntimeCSVLogger` to gameplay scene
- [ ] Create Analytics GameObject with logger component

### Audio Setup
- [ ] Create MasterMixer with exposed parameters
- [ ] Set up Music/SFX child groups
- [ ] Route AudioSources to correct groups
- [ ] Add `AudioBoot` to startup scene
- [ ] Create Settings UI with sliders and `SettingsMenu`

### UI Setup
- [ ] Create pause panel with `PauseMenu` component
- [ ] Link settings panel to pause menu
- [ ] Set up volume sliders (0-1 range)
- [ ] Test ESC key pause functionality
#
# 🎮 Features

### ✅ Complete Core Loop
- **Smooth WASD movement** with physics-based controls
- **Auto-targeting weapon system** - shoots nearest enemies
- **Intelligent enemy AI** - spawns off-screen, chases player
- **XP collection system** - magnetic pickup with level progression
- **Real-time UI** - HP/XP bars, survival timer
- **Pause system** - ESC key with settings access

### 📊 Professional Analytics
- **CSV data logging** - minute-by-minute balance metrics
- **Performance tracking** - damage by source, kill rates, survival times
- **Save/load system** - persistent best times and settings
- **Memory optimization** - object pooling for 60 FPS performance

### 🛠️ Developer-Friendly
- **One-click scene setup** - auto-generates complete playable scene
- **Modular architecture** - easy to extend with new weapons/enemies
- **Data-driven design** - ScriptableObject-based upgrades
- **Balance tuning tools** - real-time CSV export for tweaking

## 🎯 Gameplay

Experience the addictive Vampire Survivors formula:
- Survive waves of increasingly difficult enemies
- Auto-attacking weapons target threats automatically  
- Collect XP gems to level up and grow stronger
- Strategic positioning and movement are key to survival
- Built-in analytics help you understand player behavior

## 🔧 Technical Highlights

- **Unity 2022.3+ LTS** compatible
- **2D Physics** with optimized collision detection
- **Object Pooling** prevents garbage collection spikes
- **Modular upgrade system** for easy content expansion
- **CSV analytics** for data-driven balance decisions
- **Cross-platform** ready (Windows, macOS, WebGL)

## 📈 Development Roadmap

### Week 1 ✅ - Core Loop (COMPLETE)
- [x] Player movement and controls
- [x] Auto-attacking weapon system  
- [x] Enemy spawning and AI
- [x] Health/damage mechanics
- [x] XP collection and progression
- [x] UI and analytics systems

### Week 2 - Progression System
- [ ] Level-up draft UI (choose 1 of 3 upgrades)
- [ ] Upgrade application system
- [ ] Rarity weights and reroll mechanics
- [ ] Difficulty scaling over time

### Week 3 - Content & Variety
- [ ] Multiple weapon types (6+ weapons)
- [ ] Enemy variety (6+ enemy types)
- [ ] Boss encounters and elite enemies
- [ ] Visual effects and game juice

### Week 4 - Polish & Ship
- [ ] Main menu and settings
- [ ] Final balance pass using CSV data
- [ ] Build pipeline and deployment
- [ ] Steam/itch.io release preparation

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Setup
1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Inspired by **Vampire Survivors** by Luca Galante
- Built with **Unity Engine**
- Uses data-driven design patterns for scalability
- Community feedback and contributions welcome

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/SurvivorLite/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/SurvivorLite/discussions)
- **Documentation**: Check the [Wiki](https://github.com/yourusername/SurvivorLite/wiki)

---

**⭐ Star this repo if you found it helpful!**