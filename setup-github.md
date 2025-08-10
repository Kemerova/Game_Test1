# 🚀 GitHub Setup Guide for SurvivorLite

## Quick Setup (5 minutes)

### 1. Create GitHub Repository
1. Go to [GitHub.com](https://github.com) and sign in
2. Click **"New repository"** (green button)
3. **Repository name**: `SurvivorLite`
4. **Description**: `🧛‍♂️ Complete Vampire Survivors-style ARPG built in Unity - Ready to play in 2 minutes!`
5. **Public** ✅ (recommended for open source)
6. **DO NOT** initialize with README (we already have one)
7. Click **"Create repository"**

### 2. Initialize Local Git Repository
Open terminal/command prompt in your SurvivorLite folder and run:

```bash
# Initialize git repository
git init

# Add all files
git add .

# Create initial commit
git commit -m "🎉 Initial release: Complete Vampire Survivors core loop

✅ Features:
- Player movement with WASD controls
- Auto-targeting weapon system
- Enemy AI with chase behavior
- XP collection and leveling
- Real-time UI (HP/XP bars, timer)
- Object pooling for performance
- CSV analytics for balance data
- Save/load system
- Pause menu with ESC key
- One-click scene setup

🎮 Ready to play immediately!
🔧 Professional development tools included
📊 Built-in analytics for data-driven balance"

# Add your GitHub repository as remote (replace YOUR_USERNAME)
git remote add origin https://github.com/YOUR_USERNAME/SurvivorLite.git

# Push to GitHub
git branch -M main
git push -u origin main
```

### 3. Configure Repository Settings

#### Enable GitHub Pages (Optional - for WebGL builds)
1. Go to your repository → **Settings** → **Pages**
2. **Source**: Deploy from a branch
3. **Branch**: `main` → `/docs` (if you add WebGL builds to docs folder)
4. **Save**

#### Set Up Branch Protection (Recommended)
1. Go to **Settings** → **Branches**
2. **Add rule** for `main` branch
3. Enable:
   - ✅ Require pull request reviews before merging
   - ✅ Require status checks to pass before merging
   - ✅ Require branches to be up to date before merging

#### Configure Secrets (For CI/CD)
1. Go to **Settings** → **Secrets and variables** → **Actions**
2. Add these secrets for Unity Cloud Build:
   - `UNITY_LICENSE` - Your Unity license file content
   - `UNITY_EMAIL` - Your Unity account email
   - `UNITY_PASSWORD` - Your Unity account password

## 🎯 Repository Features

### Automated CI/CD Pipeline ✅
- **Continuous Integration**: Tests run on every PR
- **Multi-platform builds**: Windows, macOS, WebGL
- **Automated releases**: Tag with `[release]` in commit message
- **Artifact storage**: 30-day retention for builds

### Issue Templates ✅
- **Bug reports**: Structured bug reporting with environment details
- **Feature requests**: Organized feature suggestions with priority
- **Pull request template**: Comprehensive PR checklist

### Documentation ✅
- **README.md**: Complete project overview with quick start
- **CONTRIBUTING.md**: Developer guidelines and contribution process
- **CHANGELOG.md**: Version history and release notes
- **LICENSE**: MIT license for open source distribution

### Development Workflow ✅
- **Branch protection**: Prevents direct pushes to main
- **Code review**: Required PR reviews before merging
- **Automated testing**: Unity test runner integration
- **Release automation**: GitHub Actions for builds and releases

## 📋 Next Steps

### Immediate Actions
1. ⭐ **Star your own repository** (shows up in your starred repos)
2. 📝 **Edit repository description** and add topics/tags
3. 🔗 **Add repository URL** to your Unity project (if desired)
4. 📢 **Share with the community** (Reddit, Discord, Twitter)

### Repository Topics (Add these)
Go to your repository → **About** → **Settings** → **Topics**:
```
unity, vampire-survivors, 2d-game, arpg, survival, indie-game, 
open-source, gamedev, csharp, unity2d, roguelike, action-game
```

### Community Features
- **Discussions**: Enable for community Q&A
- **Wiki**: Add for detailed documentation
- **Projects**: Track development milestones
- **Releases**: Automated via GitHub Actions

## 🎮 Sharing Your Game

### Social Media Template
```
🧛‍♂️ Just released SurvivorLite - a complete Vampire Survivors clone in Unity!

✅ Fully playable in 2 minutes
✅ Professional analytics & save system  
✅ One-click scene setup
✅ Open source & ready to extend

Perfect for learning game development or as a base for your own survival game!

🔗 https://github.com/YOUR_USERNAME/SurvivorLite
⭐ Star if you find it useful!

#gamedev #unity #vampiresurvivors #opensource #indiedev
```

### Communities to Share
- **Reddit**: r/Unity3D, r/gamedev, r/IndieGaming
- **Discord**: Unity communities, gamedev servers
- **Twitter**: #gamedev, #unity, #indiedev hashtags
- **Unity Forums**: Showcase section

## 🔧 Maintenance

### Regular Tasks
- **Update CHANGELOG.md** with each release
- **Review and merge PRs** from contributors
- **Monitor issues** and provide support
- **Update documentation** as features evolve

### Version Management
- Use semantic versioning: `v1.0.0`, `v1.1.0`, `v2.0.0`
- Tag releases with `[release]` in commit message for automation
- Keep main branch stable and deployable

---

**🎉 Congratulations! Your SurvivorLite repository is now live on GitHub with professional CI/CD, documentation, and community features!**

**Repository URL**: `https://github.com/YOUR_USERNAME/SurvivorLite`