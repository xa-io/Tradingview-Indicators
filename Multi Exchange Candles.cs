// ###############################################################################################
// ## This will pull cross-exchange data for all base currencies for the listed exchanges ########
// ########## Keep in mind this will only work if the pair is traded on all exchanges. ###########
// ######## This script will fail, if a single exchange doesn't trade the looked up pair #########
// ###############################################################################################

//@version=5 
indicator("Multi Exchange Candles", shorttitle="MEC", overlay=true)

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

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[binanceOpen, binanceHigh, binanceLow, binanceClose] = getOhlcData("BINANCE:" + binanceSymbol)
[coinbaseOpen, coinbaseHigh, coinbaseLow, coinbaseClose] = getOhlcData("COINBASE:" + coinbaseSymbol)
[bitfinexOpen, bitfinexHigh, bitfinexLow, bitfinexClose] = getOhlcData("BITFINEX:" + bitfinexSymbol)
[bitgetOpen, bitgetHigh, bitgetLow, bitgetClose] = getOhlcData("BITGET:" + bitgetSymbol)
[bybitOpen, bybitHigh, bybitLow, bybitClose] = getOhlcData("BYBIT:" + bybitSymbol)
[coinexOpen, coinexHigh, coinexLow, coinexClose] = getOhlcData("COINEX:" + coinexSymbol)
[gateioOpen, gateioHigh, gateioLow, gateioClose] = getOhlcData("GATEIO:" + gateioSymbol)
[krakenOpen, krakenHigh, krakenLow, krakenClose] = getOhlcData("KRAKEN:" + krakenSymbol)
[kucoinOpen, kucoinHigh, kucoinLow, kucoinClose] = getOhlcData("KUCOIN:" + kucoinSymbol)

// Plotting the candles for each exchange
plotcandle(binanceOpen, binanceHigh, binanceLow, binanceClose, color=color.new(color.orange, 100), bordercolor=color.new(color.orange, 60), wickcolor=color.new(color.orange, 60))
plotcandle(coinbaseOpen, coinbaseHigh, coinbaseLow, coinbaseClose, color=color.new(color.fuchsia, 100), bordercolor=color.new(color.fuchsia, 60), wickcolor=color.new(color.fuchsia, 60))
plotcandle(bitfinexOpen, bitfinexHigh, bitfinexLow, bitfinexClose, color=color.new(color.lime, 100), bordercolor=color.new(color.lime, 60), wickcolor=color.new(color.lime, 60))
plotcandle(bitgetOpen, bitgetHigh, bitgetLow, bitgetClose, color=color.rgb(84, 198, 147, 100), bordercolor=color.rgb(84, 198, 147, 85), wickcolor=color.rgb(84, 198, 147, 85))
plotcandle(bybitOpen, bybitHigh, bybitLow, bybitClose, color=color.new(color.gray, 100), bordercolor=color.new(color.gray, 60), wickcolor=color.new(color.gray, 60))
plotcandle(coinexOpen, coinexHigh, coinexLow, coinexClose, color=color.rgb(149, 78, 137, 100), bordercolor=color.rgb(149, 78, 137, 40), wickcolor=color.rgb(149, 78, 137, 40))
plotcandle(gateioOpen, gateioHigh, gateioLow, gateioClose, color=color.new(color.teal, 100), bordercolor=color.new(color.teal, 60), wickcolor=color.new(color.teal, 60))
plotcandle(krakenOpen, krakenHigh, krakenLow, krakenClose, color=color.new(color.red, 100), bordercolor=color.new(color.red, 60), wickcolor=color.new(color.red, 60))
plotcandle(kucoinOpen, kucoinHigh, kucoinLow, kucoinClose, color=color.new(color.aqua, 100), bordercolor=color.new(color.aqua, 60), wickcolor=color.new(color.aqua, 60))

// Function to create or update a line
createOrUpdateLine(price, lineRef, lineColor) =>
    if na(lineRef)
        line.new(x1=na, y1=price, x2=na, y2=price, width=1, color=color.new(lineColor, 75), extend=extend.both)
    else
        line.set_y1(lineRef, price)
        line.set_y2(lineRef, price)
        lineRef

// Initialize lines for each exchange
var line binancePriceLine = na
var line coinbasePriceLine = na
var line bitfinexPriceLine = na
var line bitgetPriceLine = na
var line bybitPriceLine = na
var line coinexPriceLine = na
var line gateioPriceLine = na
var line krakenPriceLine = na
var line kucoinPriceLine = na

// Update or create lines for each exchange
binancePriceLine := createOrUpdateLine(binanceClose, binancePriceLine, color.orange)
coinbasePriceLine := createOrUpdateLine(coinbaseClose, coinbasePriceLine, color.fuchsia)
bitfinexPriceLine := createOrUpdateLine(bitfinexClose, bitfinexPriceLine, color.lime)
bitgetPriceLine := createOrUpdateLine(bitgetClose, bitgetPriceLine, color.rgb(84, 198, 147))
bybitPriceLine := createOrUpdateLine(bybitClose, bybitPriceLine, color.gray)
coinexPriceLine := createOrUpdateLine(coinexClose, coinexPriceLine, color.rgb(149, 78, 137))
gateioPriceLine := createOrUpdateLine(gateioClose, gateioPriceLine, color.teal)
krakenPriceLine := createOrUpdateLine(krakenClose, krakenPriceLine, color.red)
kucoinPriceLine := createOrUpdateLine(kucoinClose, kucoinPriceLine, color.aqua)
