//@version=5
indicator("Premiums Monitor", shorttitle="Premiums", overlay=true)

// === Input Settings ===
bool showBitfinexName = input(true, title="Show Bitfinex Label")
bool showCoinbaseName = input(true, title="Show Coinbase Label")
int spacingColumns = input.int(2, title="Spacing Columns", minval=0, maxval=10)
string tablePosition = input.string("Top Right", title="Table Position", options=["Top Left", "Top Center", "Top Right", "Middle Left", "Middle Center", "Middle Right", "Bottom Left", "Bottom Center", "Bottom Right"])

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for each exchange
binanceSymbol = baseCurrency + "USDT"
coinbaseSymbol = baseCurrency + "USD"
bitfinexSymbol = baseCurrency + "USD"
bitgetSymbol = baseCurrency + "USDT"
bybitSymbol = baseCurrency + "USDT"
coinexSymbol = baseCurrency + "USDT"
gateioSymbol = baseCurrency + "USDT"
krakenSymbol = baseCurrency + "USD"
kucoinSymbol = baseCurrency + "USDT"

// Fetch the close price from each exchange
binanceClose = request.security("BINANCE:" + binanceSymbol, timeframe.period, close)
coinbaseClose = request.security("COINBASE:" + coinbaseSymbol, timeframe.period, close)
bitfinexClose = request.security("BITFINEX:" + bitfinexSymbol, timeframe.period, close)
bitgetClose = request.security("BITGET:" + bitgetSymbol, timeframe.period, close)
bybitClose = request.security("BYBIT:" + bybitSymbol, timeframe.period, close)
coinexClose = request.security("COINEX:" + coinexSymbol, timeframe.period, close)
gateioClose = request.security("GATEIO:" + gateioSymbol, timeframe.period, close)
krakenClose = request.security("KRAKEN:" + krakenSymbol, timeframe.period, close)
kucoinClose = request.security("KUCOIN:" + kucoinSymbol, timeframe.period, close)

// Calculate average of all other exchanges (excluding Bitfinex and Coinbase)
otherExchangesAvg = (binanceClose + bitgetClose + bybitClose + coinexClose + gateioClose + krakenClose + kucoinClose) / 7

// Calculate premiums
bitfinexPremium = bitfinexClose - coinbaseClose
coinbasePremium = coinbaseClose - otherExchangesAvg

// Format numbers with 2 decimal places
formatNumber(value) =>
    str.tostring(value, "#.##")

// Determine colors based on premium (positive = green, negative = red)
bitfinexColor = bitfinexPremium >= 0 ? color.new(color.green, 80) : color.new(color.red, 80)
coinbaseColor = coinbasePremium >= 0 ? color.new(color.green, 80) : color.new(color.red, 80)

// Format premium display with $ sign
bitfinexText = (bitfinexPremium >= 0 ? "+" : "") + "$" + formatNumber(math.abs(bitfinexPremium))
coinbaseText = (coinbasePremium >= 0 ? "+" : "") + "$" + formatNumber(math.abs(coinbasePremium))

// === Table Setup ===
// Convert string position to Pine Script position constant
var string pos = tablePosition == "Top Left" ? position.top_left : 
                 tablePosition == "Top Center" ? position.top_center : 
                 tablePosition == "Top Right" ? position.top_right : 
                 tablePosition == "Middle Left" ? position.middle_left : 
                 tablePosition == "Middle Center" ? position.middle_center : 
                 tablePosition == "Middle Right" ? position.middle_right : 
                 tablePosition == "Bottom Left" ? position.bottom_left : 
                 tablePosition == "Bottom Center" ? position.bottom_center : 
                 position.bottom_right

// Calculate total columns (2 data columns + spacing columns)
int totalColumns = 2 + spacingColumns

// Create table with dynamic position and columns
var table premiumTable = table.new(pos, totalColumns, 2, bgcolor=color.new(#000000, 100))

if barstate.islast
    // Row 1: Bitfinex vs Coinbase - premium always shows, name conditional
    table.cell(premiumTable, 0, 0, showBitfinexName ? "Bitfinex" : "", text_color=color.white, bgcolor=showBitfinexName ? color.new(color.gray, 0) : na, text_size=size.normal)
    table.cell(premiumTable, 1, 0, bitfinexText, text_color=color.white, bgcolor=bitfinexColor, text_size=size.normal)
    
    // Row 2: Coinbase vs Other Exchanges Average - premium always shows, name conditional
    table.cell(premiumTable, 0, 1, showCoinbaseName ? "Coinbase" : "", text_color=color.white, bgcolor=showCoinbaseName ? color.new(color.gray, 0) : na, text_size=size.normal)
    table.cell(premiumTable, 1, 1, coinbaseText, text_color=color.white, bgcolor=coinbaseColor, text_size=size.normal)
    
    // Add dynamic spacing columns on the right (only if spacing > 0)
    if spacingColumns > 0
        for i = 0 to spacingColumns - 1
            table.cell(premiumTable, 2 + i, 0, "          ", text_color=color.white, bgcolor=color.new(#787b86, 100), text_size=size.small)
            table.cell(premiumTable, 2 + i, 1, "          ", text_color=color.white, bgcolor=color.new(#787b86, 100), text_size=size.small)

// === Alert Conditions ===
// Bitfinex premium alerts
alertcondition(ta.crossunder(bitfinexPremium, 0), title="Bitfinex Goes Negative", message="Bitfinex premium crossed below $0 (now negative)")
alertcondition(ta.crossover(bitfinexPremium, 0), title="Bitfinex Goes Positive", message="Bitfinex premium crossed above $0 (now positive)")

// Coinbase premium alerts
alertcondition(ta.crossunder(coinbasePremium, 0), title="Coinbase Goes Negative", message="Coinbase premium crossed below $0 (now negative)")
alertcondition(ta.crossover(coinbasePremium, 0), title="Coinbase Goes Positive", message="Coinbase premium crossed above $0 (now positive)")
