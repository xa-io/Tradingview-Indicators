# TradingView Indicators

A collection of my TradingView indicators for multi-exchange price monitoring, arbitrage detection, and advanced technical analysis.

Created by: [https://github.com/xa-io](https://github.com/xa-io)

---

## 📋 Table of Contents

- [Overview](#overview)
- [Indicators](#indicators)
  - [24hr Volume Monitor](#24hr-volume-monitor)
  - [Binance Arb](#binance-arb)
  - [Candle Percentage Range](#candle-percentage-range)
  - [Cross Exchange Candles and Volume](#cross-exchange-candles-and-volume)
  - [Multi Exchange Candles](#multi-exchange-candles)
  - [Multi Timeframe Price Change](#multi-timeframe-price-change)
  - [Premiums](#premiums)
- [Installation](#installation)
- [Usage](#usage)
- [Support](#support)

---

## Overview

This repository contains a suite of custom TradingView indicators designed for traders who monitor multiple exchanges, analyze cross-exchange arbitrage opportunities, and track price movements across different timeframes and venues.

**Key Features:**
- 24-hour volume monitoring with customizable thresholds
- Multi-exchange price comparison
- Real-time arbitrage detection
- Cross-exchange volume analysis
- Multi-timeframe price change tracking
- Premium/discount monitoring across exchanges

---

## Indicators

### 24hr Volume Monitor

**Description:**
Displays the current trading pair's 24-hour volume in USD with real-time color-coded alerts based on customizable volume thresholds.

**Features:**
- Real-time 24-hour volume calculation in USD
- Color-coded table display (green above threshold, red below)
- Smart volume formatting with K/M/B suffixes
- Configurable minimum volume threshold
- Alert system for volume threshold crossovers
- Fully customizable table position and text size

**Example:**

![24hr Volume Monitor Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/24hr%20Volume.png?raw=true)

*Screenshot showing 24hr volume display with color-coded indicator*

**Configuration:**
- Table Position: Choose from 9 placement options (default: Bottom Left)
- Table Text Size: Select from Tiny, Small, Normal, Large, or Huge
- Minimum Volume Threshold: Set volume level for color coding (default: $500,000)
- Spacing Columns: Adjust table spacing for better visibility (0-10 columns)

---

### Binance Arb

**Description:** 
Monitors arbitrage opportunities between Binance and other major exchanges in real-time. Detects price discrepancies that can be exploited for profit.

**Features:**
- Real-time price comparison between exchanges
- Configurable arbitrage threshold alerts
- Visual highlighting of arbitrage opportunities
- Customizable exchange pairs

**Example:**

![Binance Arb Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Binance%20Arb.png?raw=true)

*Screenshot showing arbitrage opportunities between Binance and other exchanges with highlighted price discrepancies*

**Configuration:**
- Threshold: Set minimum percentage difference for alerts
- Exchanges: Select which exchanges to compare
- Alert Settings: Configure notifications for arbitrage opportunities

---

### Candle Percentage Range

**Description:**
Calculates and displays the percentage range of each candle, helping identify volatility and potential breakout opportunities.

**Features:**
- Real-time candle range calculation
- Percentage display on chart
- Volatility highlighting
- Historical range comparison

**Example:**

![Candle Percentage Range Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Candle%20Percentage%20Range.png?raw=true)

*Screenshot showing percentage ranges displayed on each candle with color-coded volatility levels*

**Configuration:**
- Display Style: Choose between labels, plots, or overlay
- Color Scheme: Customize colors for different range levels
- Thresholds: Set percentage levels for highlighting

---

### Cross Exchange Candles and Volume

**Description:**
Overlays candles and volume data from multiple exchanges on a single chart for comprehensive cross-exchange analysis.

**Features:**
- Multi-exchange candle overlay
- Combined volume analysis
- Synchronized price action comparison
- Volume divergence detection

**Example:**

![Cross Exchange Candles Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Cross%20Exchange%20Candles.png?raw=true)

*Screenshot showing overlaid candles from multiple exchanges*

![Cross Exchange Volume Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Cross%20Exchange%20Volume.png?raw=true)

*Screenshot showing combined volume analysis across exchanges*

**Configuration:**
- Exchanges: Select which exchanges to display
- Volume Display: Toggle combined or individual volume
- Opacity: Adjust transparency for overlaid candles
- Color Coding: Customize colors per exchange

---

### Multi Exchange Candles

**Description:**
Displays price action from multiple exchanges simultaneously, allowing traders to spot divergences and confirm price movements across venues.

**Features:**
- Side-by-side exchange comparison
- Price divergence highlighting
- Customizable exchange selection
- Synchronized time frames

**Example:**

![Multi Exchange Candles Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Multi%20Exchange%20Candles.png?raw=true)

*Screenshot showing multiple exchange candles side-by-side with divergence indicators*

**Configuration:**
- Exchange List: Choose exchanges to monitor
- Layout: Select grid or overlay display
- Divergence Alerts: Enable notifications for price differences
- Styling: Customize colors and line thickness

---

### Multi Timeframe Price Change

**Description:**
Tracks and displays price changes across multiple timeframes in a single view, providing comprehensive trend analysis.

**Features:**
- Simultaneous multi-timeframe analysis
- Percentage change display for each timeframe
- Trend direction indicators
- Customizable timeframe selection

**Example:**

![Multi Timeframe Price Change Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Multi%20Timeframe%20Price%20Change.png?raw=true)

*Screenshot showing price change percentages across 1m, 5m, 15m, 1h, 4h, and 1d timeframes*

**Configuration:**
- Timeframes: Select which periods to display
- Reference Point: Choose starting point for calculations
- Display Format: Table or overlay on chart
- Color Coding: Positive/negative change colors

---

### Premiums

**Description:**
Monitors and visualizes price premiums or discounts between different exchanges relative to a reference exchange.

**Features:**
- Real-time premium/discount calculation
- Historical premium tracking
- Alert system for extreme premiums
- Configurable reference exchange

**Example:**

![Premiums Example](https://github.com/xa-io/Tradingview-Indicators/blob/main/images/Premiums.png?raw=true)

*Screenshot showing premium/discount percentages plotted as a line chart with alert zones*

*This is visually best used when also using the Bitfinex Candles Indicator*

**Configuration:**
- Reference Exchange: Set the baseline for comparison
- Alert Levels: Configure thresholds for notifications
- Display Style: Line chart, histogram, or table
- Update Frequency: Set refresh interval

---

## Installation

### Method 1: Pine Script Editor

1. Open TradingView and navigate to the Pine Editor
2. Copy the code from the desired indicator file (`.cs` file)
3. Paste into the Pine Editor
4. Click "Add to Chart"

### Method 2: Direct Import

1. Download the indicator file from this repository
2. In TradingView, click "Indicators" → "My Scripts"
3. Click "Import" and select the downloaded file
4. The indicator will appear in your indicators list

---

## Usage

### Basic Setup

1. **Add Indicator to Chart:**
   - Click the "Indicators" button on your TradingView chart
   - Search for the indicator name or find it in "My Scripts"
   - Click to add to chart

2. **Configure Settings:**
   - Click the gear icon next to the indicator name
   - Adjust parameters based on your trading strategy
   - Apply changes

3. **Set Alerts:**
   - Right-click on the chart
   - Select "Add Alert"
   - Choose the indicator and configure conditions
   - Set notification preferences

### Best Practices

- **Multiple Timeframes:** Use indicators across different timeframes for confluence
- **Exchange Selection:** Focus on high-liquidity exchanges for accurate signals
- **Alert Management:** Set realistic thresholds to avoid alert fatigue
- **Combine Indicators:** Use multiple indicators together for comprehensive analysis

---

## Support

For questions, issues, or feature requests:

- **Find me on discord:** `@_xa_`
- **Author:** [https://github.com/xa-io](https://github.com/xa-io)

---

## Changelog

### Latest Updates

- Added 24hr Volume Monitor with customizable threshold alerts
- Added comprehensive multi-exchange monitoring capabilities
- Enhanced arbitrage detection algorithms
- Improved cross-exchange volume analysis
- Added multi-timeframe price change tracking

---

## Disclaimer

These indicators are provided for educational and informational purposes only. Trading cryptocurrencies and other financial instruments involves risk. Always do your own research and consider seeking advice from qualified financial advisors before making trading decisions.

**Important Notes:**
- Past performance does not guarantee future results
- Arbitrage opportunities may close quickly
- Exchange delays can affect real-time accuracy
- Always verify signals with multiple sources

---
