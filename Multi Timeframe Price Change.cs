//@version=5
indicator("ETI MFT", shorttitle="ETI MFT", overlay=true)

// === Settings ===
interval = input.timeframe("15", title="Signal Interval", options=["1", "5", "15", "60", "240", "D"])  // Default to 15min

// Input to enable or disable buy signal (removes trade_flag from table if disabled)
enable_trade_signal = input.bool(true, title="Enable Trade Signal Row")

// Adjustable volume threshold for 24-hour USD volume
volume_threshold = input.int(10000, title="Volume Threshold (USD)", minval=0)  // Default to 1,000,000 USD

// Text size configuration for table display
text_size_option = input.string("Small", title="Table Text Size", options=["Tiny", "Small", "Normal", "Large", "Huge"])

// Fixed default values for inputs
tsi_long = input.int(25, title="TSI Long Length")
tsi_short = input.int(13, title="TSI Short Length")
tsi_signal = input.int(9, title="TSI Signal Length")
mfi_length = input.int(14, title="MFI Length")
mfi_buy_level = input.int(20, title="MFI Buy Level", minval=1, maxval=100)
mfi_sell_level = input.int(80, title="MFI Sell Level", minval=1, maxval=100)
macd_fast_length = input.int(12, title="MACD Fast Length")
macd_slow_length = input.int(26, title="MACD Slow Length")
macd_signal_length = input.int(9, title="MACD Signal Length")

// Volume check based on adjustable threshold
volume_24h = request.security(syminfo.tickerid, "D", close * volume)
no_liq = volume_24h < volume_threshold

// TSI (True Strength Index) calculation
tsi_val = ta.tsi(close, tsi_long, tsi_short)
tsi_signal_val = ta.sma(tsi_val, tsi_signal)
tsi_buy = ta.crossover(tsi_val, tsi_signal_val)
tsi_sell = ta.crossunder(tsi_val, tsi_signal_val)

// MFI (Money Flow Index) with selected levels
mfi_val = request.security(syminfo.tickerid, interval, ta.mfi(close, mfi_length))
mfi_buy = mfi_val < mfi_buy_level
mfi_sell = mfi_val > mfi_sell_level

// MACD calculation with fixed inputs
[macdLine, signalLine, _] = request.security(syminfo.tickerid, interval, ta.macd(close, macd_fast_length, macd_slow_length, macd_signal_length))
macd_buy = ta.crossover(macdLine, signalLine)
macd_sell = ta.crossunder(macdLine, signalLine)

// Trading signal logic
trade_flag = "Wait"  // Default state: waiting
if no_liq
    trade_flag := "No Liq"
else if (tsi_buy or mfi_buy or macd_buy)
    trade_flag := "BUY"
else if (tsi_sell or mfi_sell or macd_sell)
    trade_flag := "SELL"

// Set cell background color based on trade flag
table_bgcolor = trade_flag == "BUY" ? color.new(color.green, 80) : trade_flag == "SELL" ? color.new(color.red, 80) : trade_flag == "No Liq" ? color.black : color.new(color.gray, 80)
text_color = color.white

// === Multi-Timeframe Price Change Calculation ===
bool show15m = input(true, title="Show 15m Change")
bool show1h = input(true, title="Show 1h Change")
bool show4h = input(true, title="Show 4h Change")
bool show1d = input(true, title="Show 1d Change")
bool show1w = input(true, title="Show 1w Change")
bool show1m = input(true, title="Show 1m Change")
bool show6m = input(true, title="Show 6m Change")
bool show12m = input(true, title="Show 12m Change")

formatNumber(value, decPlaces) =>
    scale = math.pow(10, decPlaces)
    formattedValue = math.round(value * scale) / scale
    str.tostring(formattedValue)

calcPercentChange(currentPrice, historicalPrice) =>
    ((currentPrice - historicalPrice) / historicalPrice) * 100

price15mAgo = request.security(syminfo.tickerid, "1", close[15])
price1hAgo = request.security(syminfo.tickerid, "1", close[60])
price4hAgo = request.security(syminfo.tickerid, "5", close[48])
price1dAgo = request.security(syminfo.tickerid, "60", close[24])
price1wAgo = request.security(syminfo.tickerid, "60", close[168])
price1mAgo = request.security(syminfo.tickerid, "D", close[30])
price6mAgo = request.security(syminfo.tickerid, "W", close[24])
price12mAgo = request.security(syminfo.tickerid, "W", close[52])

percentChange15m = calcPercentChange(close, price15mAgo)
percentChange1h = calcPercentChange(close, price1hAgo)
percentChange4h = calcPercentChange(close, price4hAgo)
percentChange1d = calcPercentChange(close, price1dAgo)
percentChange1w = calcPercentChange(close, price1wAgo)
percentChange1m = calcPercentChange(close, price1mAgo)
percentChange6m = calcPercentChange(close, price6mAgo)
percentChange12m = calcPercentChange(close, price12mAgo)

decimalPlaces = syminfo.mintick > 1 ? 0 : int(math.abs(math.log10(syminfo.mintick)))
colorPositive = color.new(color.green, 80)
colorNegative = color.new(color.red, 80)

// Convert text size option to Pine Script size constant
text_size = text_size_option == "Tiny" ? size.tiny : text_size_option == "Small" ? size.small : text_size_option == "Normal" ? size.normal : text_size_option == "Large" ? size.large : size.huge

// === Table Setup ===
var table infoTable = table.new(position.top_right, 1, 9, bgcolor=color.new(#000000, 0))  // One column with 9 rows: 1 for trade signal + 8 for MTF data

if barstate.islast
    // Only add the trade_flag row if the trade signal is enabled
    int row = 0
    if enable_trade_signal
        table.cell(infoTable, 0, row, trade_flag, text_color=text_color, bgcolor=table_bgcolor, text_size=text_size)
        row := row + 1
    
    // Start adding MTF data below the trade_flag row
    if show15m
        table.cell(infoTable, 0, row, "15m: " + formatNumber(percentChange15m, 2) + "%", text_color=color.white, bgcolor=percentChange15m >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show1h
        table.cell(infoTable, 0, row, "1h: " + formatNumber(percentChange1h, 2) + "%", text_color=color.white, bgcolor=percentChange1h >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show4h
        table.cell(infoTable, 0, row, "4h: " + formatNumber(percentChange4h, 2) + "%", text_color=color.white, bgcolor=percentChange4h >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show1d
        table.cell(infoTable, 0, row, "1d: " + formatNumber(percentChange1d, 2) + "%", text_color=color.white, bgcolor=percentChange1d >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show1w
        table.cell(infoTable, 0, row, "1w: " + formatNumber(percentChange1w, 2) + "%", text_color=color.white, bgcolor=percentChange1w >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show1m
        table.cell(infoTable, 0, row, "1m: " + formatNumber(percentChange1m, 2) + "%", text_color=color.white, bgcolor=percentChange1m >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show6m
        table.cell(infoTable, 0, row, "6m: " + formatNumber(percentChange6m, 2) + "%", text_color=color.white, bgcolor=percentChange6m >= 0 ? colorPositive : colorNegative, text_size=text_size)
        row := row + 1
    if show12m
        table.cell(infoTable, 0, row, "12m: " + formatNumber(percentChange12m, 2) + "%", text_color=color.white, bgcolor=percentChange12m >= 0 ? colorPositive : colorNegative, text_size=text_size)

// Alerts for buy/sell signals
alertcondition(trade_flag == "BUY", title="Buy Signal", message="Good Buying Range")
alertcondition(trade_flag == "SELL", title="Sell Signal", message="Potential Sell Opportunity")
