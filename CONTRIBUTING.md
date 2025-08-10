# Contributing to SurvivorLite

Thank you for your interest in contributing to SurvivorLite! This document provides guidelines and information for contributors.

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3+ LTS
- Git
- Basic knowledge of C# and Unity development

### Setup Development Environment
1. Fork the repository
2. Clone your fork: `git clone https://github.com/yourusername/SurvivorLite.git`
3. Open in Unity Hub
4. Run `SurvivorLite → Auto Setup Complete Scene` to test

## 🎯 How to Contribute

### Reporting Bugs
- Use the [GitHub Issues](https://github.com/yourusername/SurvivorLite/issues) page
- Include Unity version, platform, and steps to reproduce
- Attach screenshots or videos if helpful

### Suggesting Features
- Open a [GitHub Discussion](https://github.com/yourusername/SurvivorLite/discussions) first
- Describe the feature and its benefits
- Consider implementation complexity and scope

### Code Contributions

#### Branch Naming
- `feature/weapon-chainlightning` - New features
- `fix/projectile-pooling` - Bug fixes  
- `docs/setup-guide` - Documentation
- `refactor/enemy-ai` - Code improvements

#### Code Style
- Follow existing C# conventions
- Use XML documentation comments for public methods
- Keep classes focused and single-responsibility
- Prefer composition over inheritance

#### Pull Request Process
1. Create a feature branch from `main`
2. Make your changes with clear, descriptive commits
3. Test thoroughly (run the game for 10+ minutes)
4. Update documentation if needed
5. Submit PR with detailed description

## 📋 Development Guidelines

### Architecture Principles
- **Data-driven design**: Use ScriptableObjects for configuration
- **Object pooling**: Avoid Instantiate/Destroy during gameplay
- **Modular systems**: Keep components loosely coupled
- **Performance first**: Target 60 FPS on mid-range hardware

### Testing Checklist
- [ ] Game runs without errors for 10+ minutes
- [ ] No memory leaks (check Profiler)
- [ ] All UI elements function correctly
- [ ] Pause/resume works properly
- [ ] CSV logging generates valid data

### Code Areas

#### High Priority
- **Weapon variety** - New weapon types and behaviors
- **Enemy types** - Different AI patterns and abilities
- **Upgrade system** - Level-up UI and progression
- **Visual effects** - Particle systems and juice

#### Medium Priority
- **Audio system** - Music and sound effects
- **Menu systems** - Main menu, settings, game over
- **Balance tuning** - Using CSV analytics data
- **Performance optimization** - Profiling and improvements

#### Low Priority
- **Platform support** - Mobile, console adaptations
- **Localization** - Multi-language support
- **Advanced features** - Achievements, leaderboards

## 🎨 Asset Guidelines

### Code Assets
- Place scripts in appropriate `Assets/Scripts/` subfolders
- Use consistent naming: `WeaponChainLightning.cs`
- Include XML documentation for public APIs

### Art Assets
- Use placeholder art (colored shapes) for prototypes
- Keep file sizes reasonable (<1MB per asset)
- Organize in `Assets/Art/` with clear folder structure

### Audio Assets
- Use royalty-free or original audio only
- Compress appropriately for web deployment
- Organize in `Assets/Audio/` by type (Music, SFX)

## 🔍 Review Process

### What We Look For
- **Functionality**: Does it work as intended?
- **Performance**: No significant FPS drops or memory issues
- **Code quality**: Clean, readable, well-documented
- **Testing**: Thoroughly tested edge cases
- **Scope**: Focused changes that don't break existing features

### Review Timeline
- Small fixes: 1-2 days
- New features: 3-7 days
- Major changes: 1-2 weeks

## 🏆 Recognition

Contributors will be:
- Listed in the game credits
- Mentioned in release notes
- Given collaborator access for significant contributions
- Featured in community showcases

## 📞 Getting Help

- **Questions**: Use [GitHub Discussions](https://github.com/yourusername/SurvivorLite/discussions)
- **Real-time chat**: Join our Discord (link in README)
- **Documentation**: Check the [Wiki](https://github.com/yourusername/SurvivorLite/wiki)

## 📄 License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

**Happy coding! 🎮✨**