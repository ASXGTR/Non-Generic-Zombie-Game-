# Non-Generic-Zombie-Game-
Game Project 01 - First Game Project - Solo Beginner Dev 
# ☣️ Modular Survival Game for Android

Solo-developed in Unity by John — architected from the ground up for immersive mobile portrait play, gear-driven inventory, and abuse-resistant systems.

---

## 🧠 Vision

A tactical survival experience where every item counts, every slot matters, and fairness is non-negotiable. Inspired by the depth of classic games like Fallout and the grit of zombie fiction, this project blends modular UI systems with tightly scoped player agency.

---

## 🧩 Core Systems

- **📦 Gear & Inventory**
  - Slot-based gear system with persistent HUD overlays
  - Item placement logic with anti-nesting protection (no backpack-in-backpack abuse)
  - Dynamic slot types for future gear expansions (e.g. belt holsters, tactical rigs)

- **🖼️ UI Architecture**
  - Portrait-mode responsive UI with layered panels and safe area support
  - CanvasGroup & prefab-based visibility management across scenes
  - Polished transitions with stat overlays and potential animated effects

- **🛠️ Editor Tooling**
  - Custom `ItemDataValidator` scans and fixes missing icon/equippedSprite references
  - Auto-labeler for item categorization during import (e.g. "Clothing", "Consumable", "Gear")
  - Asset reserialization utilities for ghost field cleanup and inspector consistency

---

## 🐛 Debugging Philosophy

Every bug is a breadcrumb. Problem-solving through stepwise elimination, log scrutiny, and visual cues has been core to development. Serialization ghosts, prefab miswiring, and UI layering issues — all tackled with precision and patience.

---

## 📚 Roadmap

- [ ] Stat overlay system with contextual effects (e.g. hunger fade, damage flash)
- [ ] Tactical gear slot expansions (holsters, rig layers)
- [ ] Enhanced editor validators (YAML inspection, orphan reference cleanup)
- [ ] Story integration with player-driven narrative triggers

---

## 🧾 Notes

- Unity version: **[specify version]**
- Developed entirely with **manual layout** for pixel-perfect portrait scaling
- Game logic built modular-first for future extensibility

---

## ⚠️ Licensing & Sharing

This is a private, in-development project. Please do not fork or redistribute code, assets, or tooling without permission. Future open-source modules may be released separately under permissive licenses.

---

## 🤝 Contact

Curious about the systems? Want to collaborate or offer feedback? Reach out via GitHub or [insert preferred contact method].

---
