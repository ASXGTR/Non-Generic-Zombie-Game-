@@ -1,67 +1,125 @@
# Non-Generic-Zombie-Game-
Game Project 01 - First Game Project - Solo Beginner Dev 
# ☣️ Modular Survival Game for Android
# ☣️ Non-Generic-Zombie-Game

Solo-developed in Unity by John — architected from the ground up for immersive mobile portrait play, gear-driven inventory, and abuse-resistant systems.
Post-apocalyptic survival in your pocket. Built for clarity. Played in chaos.

![Unity 2022.3 LTS](https://img.shields.io/badge/Unity-2022.3_LTS-blue)
![Secrets Clean](https://img.shields.io/badge/Gitleaks-Passed-brightgreen)
![Platform](https://img.shields.io/badge/Platform-Android-green)
![Dev Stage](https://img.shields.io/badge/Status-Pre-Pre-Alpha-orange)

---

## 🛠️ Development Status

**This project is in active development — currently in a _pre-pre-alpha_ build stage.**  
Core systems are being tested, foundational tooling is in place, and gameplay loops are still under construction. Expect major changes, breakage, and lots of evolution.

---

## 🧠 Vision

A tactical survival experience where every item counts, every slot matters, and fairness is non-negotiable. Inspired by the depth of classic games like Fallout and the grit of zombie fiction, this project blends modular UI systems with tightly scoped player agency.
A tactical survival experience where every item counts, every slot matters, and fairness is non-negotiable. Inspired by Fallout, minimalist UI design, and the brutal charm of zombie fiction — this game blends modular inventory, custom editor tooling, and immersive storytelling.

Built for mobile portrait play, players navigate a crumbling world through responsive UI and dynamic gear. Beneath the interface, a **text-based narrative engine** drives the story — tying player actions, hunger states, and environment triggers into branching tension and reactive events.

---

## 🧩 Core Systems

- **📦 Gear & Inventory**
  - Slot-based gear system with persistent HUD overlays
  - Item placement logic with anti-nesting protection (no backpack-in-backpack abuse)
  - Dynamic slot types for future gear expansions (e.g. belt holsters, tactical rigs)
### 📦 Gear & Inventory
- Slot-based gear system with persistent HUD overlays
- Backpack abuse protection (no nesting allowed)
- Expandable slot types for rigs, holsters, belt layers

- **🖼️ UI Architecture**
  - Portrait-mode responsive UI with layered panels and safe area support
  - CanvasGroup & prefab-based visibility management across scenes
  - Polished transitions with stat overlays and potential animated effects
### 🖼️ UI Architecture
- Portrait-mode layout with safe area support
- CanvasGroup visibility across stacked scene layers
- Animated transitions, stat overlays, and feedback effects

- **🛠️ Editor Tooling**
  - Custom `ItemDataValidator` scans and fixes missing icon/equippedSprite references
  - Auto-labeler for item categorization during import (e.g. "Clothing", "Consumable", "Gear")
  - Asset reserialization utilities for ghost field cleanup and inspector consistency
### 📚 Interactive Narrative System
- Text-based events triggered by movement, inventory, or survival status
- Inline dialogue and environmental responses
- Future support for branching logic and story modules

### 🛠️ Editor Tooling
- `ItemDataValidator`: auto-fixes missing icons and equipped sprites
- Auto-labeler: classifies assets by type at import
- Serialization ghost cleanup for inspector consistency
- Prefab scanner and asset hygiene repair scripts

---

## 🐛 Debugging Philosophy

Every bug is a breadcrumb. Problem-solving through stepwise elimination, log scrutiny, and visual cues has been core to development. Serialization ghosts, prefab miswiring, and UI layering issues — all tackled with precision and patience.
Every bug is a breadcrumb. Issues tracked through logs, scene behavior, and rollback analysis. Serialization ghosts, prefab miswires, and visual artifacts are logged, addressed, and revalidated.

---

## 🧪 Validation & Hygiene

- Baseline tagged: `baseline-clean`
- Secrets audit passed via Gitleaks v8.28.0
- Manual YAML reviews for serialization consistency
- Hardened `.gitignore` protecting sensitive and platform-specific files
- Dual Git remotes (GitHub + GitLab) with tag-based rollback support

---

## 📚 Roadmap

- [ ] Stat overlay system with contextual effects (e.g. hunger fade, damage flash)
- [ ] Tactical gear slot expansions (holsters, rig layers)
- [ ] Enhanced editor validators (YAML inspection, orphan reference cleanup)
- [ ] Story integration with player-driven narrative triggers
- [ ] Stat overlays: hunger fades, damage flashes, healing pulses
- [ ] Gear slot expansion: tactical layers, rig stacking
- [ ] Validator upgrades: orphan reference finder, batch asset scans
- [ ] Narrative system: branching triggers, dialogue UI, event scripting

---

## 🧾 Notes
## 🧾 Project Notes

- Unity version: **[specify version]**
- Developed entirely with **manual layout** for pixel-perfect portrait scaling
- Game logic built modular-first for future extensibility
- Unity Version: **2022.3.9f1 LTS**
- Target Platform: **Android (portrait mode)**
- UI: Manual layout for pixel precision
- Architecture: Modular from day one, no frameworks

---

## ⚠️ Licensing & Sharing

This is a private, in-development project. Please do not fork or redistribute code, assets, or tooling without permission. Future open-source modules may be released separately under permissive licenses.
All game code, assets, and content are protected under a **Custom Solo Dev License**.  
Future monetization options — including ad-supported or paid builds — are reserved by the developer.  
Editor tooling may be released under MIT in a separate repository once marked open-source.

See `LICENSE.md` for full terms.

---

## 🤝 Contact
## 🛡️ Security Practices

- Gitleaks scan passed — secrets-free repo and commit history
- Editor tools built with field isolation and prefab safety in mind
- All variables and asset references validated pre-release
- Dual remote Git setup for audit trails and rollback

---

## 👤 About the Developer

Built solo by **John**, a self-taught Unity developer based in Liverpool, England.

Curious about the systems? Want to collaborate or offer feedback? Reach out via GitHub or [insert preferred contact method].
This is his **first-ever coding project**, begun with zero programming knowledge — less than two weeks ago. From empty folders to full validation pipelines, John designed, built, and documented every system manually, learning Unity, Git, CLI tools, and DevOps as he went.

- Proficient now in Unity editor scripting, serialization, and hygiene workflows  
- Passionate about clean code, resilient systems, and abuse-resistant design  
- Developed on modest hardware using local scans, backups, and safe tagging strategies  
- A systems thinker first, building tools that build the game

This project reflects what solo developers can do when the goal is clarity, integrity, and long-term scalability.

---

## 🤝 Contact

Curious about the game?  
Want to suggest story events, test gear logic, or collaborate?

Open a **GitHub Issue** in this repo — feedback and ideas welcome.