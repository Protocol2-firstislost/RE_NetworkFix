# RE_NetworkFix

**A comprehensive logic and network optimization suite for R.E.P.O. (Unity game)**  
*Created by Antigravity AI*

[![GitHub issues](https://img.shields.io/github/issues/Protocol2-firstislost/RE_NetworkFix)](https://github.com/Protocol2-firstislost/RE_NetworkFix/issues)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

## 🚀 Features

- **30 Players Support** – Increased lobby limit from 4 to 30.
- **Late-Join (Join-in-Progress)** – Lobbies stay open after the game starts.
- **Weapon Loot Pool** – Pistol, Shotgun, SMG can be found as map loot.
- **Engine Optimizations**:
  - Yield Pooling – eliminates `new WaitForSeconds` allocations.
  - API Marshalling Bypass – cached `Camera.main` & `Transform` references.
  - Zero-Alloc HUD – optimized text formatting (zero GC overhead).
- **Hunter AI** – Memetic chat phrases and rage modes.
- **Advanced Culling** – Dynamic light shadow stripping beyond 15 m for GPU relief.

## ⚡ Performance Gains

- **8cm Delta-Sync** – reduced network bandwidth usage.
- **1:4 Phase Staggering** – optimized physics update calls.

## 📥 Installation

1. Install [BepInExPack](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/) for R.E.P.O.
2. Place `RepoNetworkFix.dll` into `REPO/BepInEx/plugins/`.
3. Launch the game. The mod activates automatically.

## 🐛 Reporting Issues & Contributing

**This mod is under active development. Your feedback is crucial!**

If you encounter:
- Crashes, desyncs, or performance drops
- Weapons not spawning as expected
- Late-join not working in some cases
- Any other unexpected behavior

👉 **Please open an issue on GitHub**  
[https://github.com/Protocol2-firstislost/RE_NetworkFix/issues](https://github.com/Protocol2-firstislost/RE_NetworkFix/issues)

Include:
- Game version and BepInEx version
- Steps to reproduce the problem
- Any error logs (from `BepInEx/LogOutput.log`)

Feature suggestions are also welcome – use the **Enhancement** label.

## ❤️ Support the Mod

- ⭐ Star this repository if you find it useful.
- 🔔 Watch for updates.
- 📢 Share with other R.E.P.O. players.

---

## Русская версия (кратко)

**RE_NetworkFix** – мод для игры R.E.P.O. (Unity), увеличивающий лимит игроков до 30, добавляющий оружие на карты, оптимизирующий производительность и сетевой код.

**Сообщать об ошибках и предлагать улучшения:**  
Создавайте **Issue** на GitHub (ссылка выше). Очень помогают логи и пошаговое описание проблемы. Спасибо за тестирование!
