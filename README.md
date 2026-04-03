Please Send me issues to upgrade my mod
# RE_NetworkFix
Mod For Unity Game R.E.P.O

# RepoNetworkFix (v1.0.0)

**A comprehensive logic and network optimization suite for REPO.**
Created by Antigravity AI to enhance multiplayer experience and frame stability.

## ❄️ Features
- **30 Players Support**: Increased lobby limit to 30.
- **Late-Join (Join-in-Progress)**: Lobbies stay open after the game starts.
- **Weapon Loot Pool**: Weapons (Pistol, Shotgun, SMG) can be found as map loot.
- **Engine Optimization**:
    - **Yield Pooling**: 100% elimination of `new WaitForSeconds` allocations.
    - **API Marshalling Bypass**: Cached `Camera.main` and `Transform` references.
    - **Zero-Alloc HUD**: Optimized text formatting for zero GC overhead.
- **Hunter (Hunter)**: Memetic chat phrases and rage modes.
- **Advanced Culling**: Dynamic light shadow stripping (>15m) for GPU relief.

## 🚀 Performance
- **8cm Delta-Sync**: Reduced network bandwidth.
- **1:4 Phase Staggering**: Optimized physics update calls.

---

### RU: Описание на русском
**RepoNetworkFix (v1.0.0)** — Комплексный пакет оптимизации сети и производительности для REPO.
Создано при помощи Antigravity AI.

**Основные возможности:**
- **Поддержка 30 игроков**: Лимит лобби расширен до 30.
- **Заход во время игры**: Лобби не закрывается после старта раунда.
- **Оружие на карте**: Пистолеты, ружья и ПП можно найти в интерьере уровней.
- **Глубокая оптимизация**:
    - Кэширование Unity API для мгновенного доступа к камере и трансформам.
    - Пул объектов для исключения микро-фризов памяти.
    - Оптимизированный HUD без нагрузки на ОЗУ.
- **Охотник (Hunter)**: Улучшенная логика поведения и фразы в чате.

---

## Installation
1. Install [BepInExPack](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/).
2. Place `RepoNetworkFix.dll` into `REPO/BepInEx/plugins/`.
3. Experience the peak of Unity performance!
