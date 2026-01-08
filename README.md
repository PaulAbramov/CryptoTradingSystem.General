# CryptoTradingSystem.General

This repository serves as the core foundational library for the Modular Crypto Trading Suite. It contains shared models, common utilities, and the architectural contracts used across the entire ecosystem.

### 🧩 Role in the Suite
This project acts as the "Glue" that ensures type safety and consistent data structures between the Scraper, the Indicator Calculator, and the Strategy modules.

### ✨ Key Features
* **Shared Domain Models:** Centralized definitions for Candles, Orders, Pairs, and Market Data.
* **Common Utilities:** Time-frame converters, logging wrappers, and configuration helpers.
* **Base Interfaces:** Defines the contracts for extensible trading strategies and indicators.

### 🛠 Tech Stack
* **Language:** C#
* **Framework:** .NET
* **Design Pattern:** Shared Kernel / Modular Monolith

### 🔗 Related Repositories
* [IndicatorCalculator](https://github.com/PaulAbramov/CryptoTradingSystem.IndicatorCalculator)
* [Scraper](https://github.com/PaulAbramov/CryptoTradingSystem.Scraper)
* [TestStrategy](https://github.com/PaulAbramov/CryptoTradingSystem.TestStrategy)
