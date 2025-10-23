//@version=5 
// Exchange Comparison Indicator - Candles
indicator("ECI - Binance USDT Arb v3", shorttitle="ECI - Binance Arb v3", overlay=true)

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
    priceDiffLabel := label.new(x=bar_index, y=binanceClose, text="", color=color.white, style=label.style_label_left, size=size.small, xloc=xloc.bar_index, yloc=yloc.price)

decPlaces = decimals()
labelText = "" + formatNumber(percentDifference, 2) + "%"

// Adjust label position to be to the right of the current candle
label.set_xy(priceDiffLabel, bar_index + 1, binanceClose) // Shift the label to the right by one bar
label.set_text(priceDiffLabel, labelText)

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=binanceClose, x2=na, y2=binanceClose, width=1, color=color.new(color.orange, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, binanceClose)
    line.set_y2(currentPriceLine, binanceClose)

// Create horizontal lines for the highest high and lowest low of the visible range
var line highLine = na
var line lowLine = na

visibleBars = input.int(30, title="Number of Bars for Visible Range")

visibleHigh = ta.highest(binanceHigh, visibleBars)
visibleLow = ta.lowest(binanceLow, visibleBars)

if na(highLine)
    highLine := line.new(x1=na, y1=visibleHigh, x2=na, y2=visibleHigh, width=1, color=#91919140, extend=extend.both)
else
    line.set_y1(highLine, visibleHigh)
    line.set_y2(highLine, visibleHigh)

if na(lowLine)
    lowLine := line.new(x1=na, y1=visibleLow, x2=na, y2=visibleLow, width=1, color=#91919140, extend=extend.both)
else
    line.set_y1(lowLine, visibleLow)
    line.set_y2(lowLine, visibleLow)
