//@version=5
indicator("Binance USDT Arb", shorttitle="ECI - Binance Arb", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Binance
binanceSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[binanceOpen, binanceHigh, binanceLow, binanceClose] = getOhlcData("BINANCE:" + binanceSymbol)

// Plotting the candles for each exchange
plotcandle(binanceOpen, binanceHigh, binanceLow, binanceClose, color=color.new(color.orange, 97), bordercolor=color.new(color.orange, 60), wickcolor=color.new(color.orange, 60))

// Function to determine the number of decimal places
decimals() =>
    var int dec = 0
    var float tickSize = syminfo.mintick
    while (tickSize < 1)
        tickSize := tickSize * 10
        dec := dec + 1
    dec

// Custom function to format a number as a string with a specific number of decimal places
formatNumber(value, decPlaces) =>
    multiplier = math.pow(10, decPlaces)
    roundedValue = math.round(value * multiplier) / multiplier
    str.tostring(roundedValue)

// Calculate and display the price and percentage difference
priceDifference = close - binanceClose
percentDifference = (priceDifference / binanceClose) * 100

var label priceDiffLabel = na
if na(priceDiffLabel)
    priceDiffLabel := label.new(bar_index, high, text="", color=color.white, style=label.style_label_down, size=size.small)
decPlaces = decimals()
labelText = formatNumber(priceDifference, decPlaces) + " (" + formatNumber(percentDifference, 2) + "%)"
label.set_text(priceDiffLabel, labelText)
label.set_xy(priceDiffLabel, bar_index, high)

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=binanceClose, x2=na, y2=binanceClose, width=1, color=color.new(color.orange, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, binanceClose)
    line.set_y2(currentPriceLine, binanceClose)
