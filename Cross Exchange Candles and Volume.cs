// ####################################################################################
// #####                                                                          #####
// ##### HERE ARE MY AUTO-COMPARE INDICATORS FOR CROSS-EXCHANGE CHARTING WITHOUT  #####
// ##### NEEDING TO ADD COMPARE NONSENSE AND ADJUST EVERY NEW PAIR YOU LOOK AT    #####
// #####                                                                          #####
// ##### YOU SHOULD BE ADDING EACH 'INDICATOR' AS IT'S OWN, NOT COMBINING ALL     #####
// ##### UNLESS YOU KNOW WHAT YOU'RE DOING.... I DO NOT. :<                       #####
// #####                                                                          #####
// ##### DONT GET REKT, NEWBS   ^_^                                               #####
// #####                                                                          #####
// ####################################################################################


// ############################################################################ 
// ##### BINANCE CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ############################################################################ 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5 
// Exchange Comparison Indicator - Candles
indicator("ECI - Binance USDT", shorttitle="ECI - Binance", overlay=true)

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
plotcandle(binanceOpen, binanceHigh, binanceLow, binanceClose, color=color.new(color.orange, 100), bordercolor=color.new(color.orange, 60), wickcolor=color.new(color.orange, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=binanceClose, x2=na, y2=binanceClose, width=1, color=color.new(color.orange, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, binanceClose)
    line.set_y2(currentPriceLine, binanceClose)


// ############################################################################################################################################# 
// ##### BINANCE VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################# 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Binance USDT", shorttitle="ECI Vol - Binance", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Binance
binanceSymbol = baseCurrency + "USDT"

// Fetch the volume data from Binance
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

binanceVolume = getVolumeData("BINANCE:" + binanceSymbol)

// Plotting the volume for Binance
plot(binanceVolume, title="Binance Volume", color=color.new(color.orange, 80), style=plot.style_columns, linewidth=1)














// ########################################################################### 
// ##### KUCOIN CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ########################################################################### 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - KuCoin USDT", shorttitle="ECI - KuCoin", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for KuCoin
kucoinSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[kucoinOpen, kucoinHigh, kucoinLow, kucoinClose] = getOhlcData("KUCOIN:" + kucoinSymbol)

// Plotting the candles for each exchange
plotcandle(kucoinOpen, kucoinHigh, kucoinLow, kucoinClose, color=color.new(color.aqua, 100), bordercolor=color.new(color.aqua, 60), wickcolor=color.new(color.aqua, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=kucoinClose, x2=na, y2=kucoinClose, width=1, color=color.new(color.aqua, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, kucoinClose)
    line.set_y2(currentPriceLine, kucoinClose)


// ############################################################################################################################################ 
// ##### KUCOIN VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################ 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - KuCoin USDT", shorttitle="ECI Vol - KuCoin", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for KuCoin
kucoinSymbol = baseCurrency + "USDT"

// Fetch the volume data from KuCoin
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

kucoinVolume = getVolumeData("KUCOIN:" + kucoinSymbol)

// Plotting the volume for KuCoin
plot(kucoinVolume, title="KuCoin Volume", color=color.new(color.aqua, 80), style=plot.style_columns, linewidth=1)














// ########################################################################## 
// ##### BYBIT CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) ##### 
// ########################################################################## 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Bybit USDT", shorttitle="ECI - Bybit", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Bybit
bybitSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[bybitOpen, bybitHigh, bybitLow, bybitClose] = getOhlcData("BYBIT:" + bybitSymbol)

// Plotting the candles for each exchange
plotcandle(bybitOpen, bybitHigh, bybitLow, bybitClose, color=color.new(color.gray, 100), bordercolor=color.new(color.gray, 60), wickcolor=color.new(color.gray, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=bybitClose, x2=na, y2=bybitClose, width=1, color=color.new(color.gray, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, bybitClose)
    line.set_y2(currentPriceLine, bybitClose)


// ########################################################################################################################################### 
// ##### BYBIT VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ########################################################################################################################################### 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Bybit USDT", shorttitle="ECI Vol - Bybit", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Bybit
bybitSymbol = baseCurrency + "USDT"

// Fetch the volume data from Bybit
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

bybitVolume = getVolumeData("BYBIT:" + bybitSymbol)

// Plotting the volume for Bybit
plot(bybitVolume, title="Bybit Volume", color=color.new(color.gray, 80), style=plot.style_columns, linewidth=1)














// ############################################################################# 
// ##### BITFINEX CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ############################################################################# 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Bitfinex USD", shorttitle="ECI - Bitfinex", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Bitfinex
bitfinexSymbol = baseCurrency + "USD"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[bitfinexOpen, bitfinexHigh, bitfinexLow, bitfinexClose] = getOhlcData("BITFINEX:" + bitfinexSymbol)

// Plotting the candles for each exchange
plotcandle(bitfinexOpen, bitfinexHigh, bitfinexLow, bitfinexClose, color=color.new(color.lime, 100), bordercolor=color.new(color.lime, 60), wickcolor=color.new(color.lime, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=bitfinexClose, x2=na, y2=bitfinexClose, width=1, color=color.new(color.lime, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, bitfinexClose)
    line.set_y2(currentPriceLine, bitfinexClose)


// ############################################################################################################################################## 
// ##### BITFINEX VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################## 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Bitfinex USDT", shorttitle="ECI Vol - Bitfinex", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Bitfinex
bitfinexSymbol = baseCurrency + "USD"

// Fetch the volume data from Bitfinex
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

bitfinexVolume = getVolumeData("BITFINEX:" + bitfinexSymbol)

// Plotting the volume for Bitfinex
plot(bitfinexVolume, title="Bitfinex Volume", color=color.new(color.orange, 80), style=plot.style_columns, linewidth=1)














// ############################################################################# 
// ##### COINBASE CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ############################################################################# 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Coinbase USD", shorttitle="ECI - Coinbase", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Coinbase
coinbaseSymbol = baseCurrency + "USD"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[coinbaseOpen, coinbaseHigh, coinbaseLow, coinbaseClose] = getOhlcData("COINBASE:" + coinbaseSymbol)

// Plotting the candles for each exchange
plotcandle(coinbaseOpen, coinbaseHigh, coinbaseLow, coinbaseClose, color=color.new(color.fuchsia, 100), bordercolor=color.new(color.fuchsia, 60), wickcolor=color.new(color.fuchsia, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=coinbaseClose, x2=na, y2=coinbaseClose, width=1, color=color.new(color.fuchsia, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, coinbaseClose)
    line.set_y2(currentPriceLine, coinbaseClose)


// ############################################################################################################################################## 
// ##### COINBASE VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################## 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Coinbase USD", shorttitle="ECI Vol - Coinbase", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Coinbase
coinbaseSymbol = baseCurrency + "USD"

// Fetch the volume data from Coinbase
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

coinbaseVolume = getVolumeData("COINBASE:" + coinbaseSymbol)

// Plotting the volume for Coinbase
plot(coinbaseVolume, title="Coinbase Volume", color=color.new(color.fuchsia, 80), style=plot.style_columns, linewidth=1)














// ########################################################################### 
// ##### GATEIO CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ########################################################################### 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - GateIO USDT", shorttitle="ECI - GateIO", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for GateIO
gateioSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[gateioOpen, gateioHigh, gateioLow, gateioClose] = getOhlcData("GATEIO:" + gateioSymbol)

// Plotting the candles for each exchange
plotcandle(gateioOpen, gateioHigh, gateioLow, gateioClose, color=color.new(color.teal, 100), bordercolor=color.new(color.teal, 60), wickcolor=color.new(color.teal, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=gateioClose, x2=na, y2=gateioClose, width=1, color=color.new(color.teal, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, gateioClose)
    line.set_y2(currentPriceLine, gateioClose)


// ############################################################################################################################################ 
// ##### GATEIO VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################ 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - GateIO USDT", shorttitle="ECI Vol - GateIO", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for GateIO
gateioSymbol = baseCurrency + "USDT"

// Fetch the volume data from GateIO
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

gateioVolume = getVolumeData("GateIO:" + gateioSymbol)

// Plotting the volume for GateIO
plot(gateioVolume, title="GateIO Volume", color=color.new(color.teal, 80), style=plot.style_columns, linewidth=1)














// ########################################################################### 
// ##### KRAKEN CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ########################################################################### 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Kraken USD", shorttitle="ECI - Kraken", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Kraken
krakenSymbol = baseCurrency + "USD"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[krakenOpen, krakenHigh, krakenLow, krakenClose] = getOhlcData("KRAKEN:" + krakenSymbol)

// Plotting the candles for each exchange
plotcandle(krakenOpen, krakenHigh, krakenLow, krakenClose, color=color.new(color.red, 100), bordercolor=color.new(color.red, 60), wickcolor=color.new(color.red, 60))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=krakenClose, x2=na, y2=krakenClose, width=1, color=color.new(color.red, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, krakenClose)
    line.set_y2(currentPriceLine, krakenClose)


// ############################################################################################################################################ 
// ##### KRAKEN VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################ 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Kraken USD", shorttitle="ECI Vol - Kraken", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Kraken
krakenSymbol = baseCurrency + "USD"

// Fetch the volume data from Kraken
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

krakenVolume = getVolumeData("Kraken:" + krakenSymbol)

// Plotting the volume for Kraken
plot(krakenVolume, title="Kraken Volume", color=color.new(color.red, 80), style=plot.style_columns, linewidth=1)














// ########################################################################### 
// ##### COINEX CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ########################################################################### 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Coinex USDT", shorttitle="ECI - Coinex", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Coinex
coinexSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[coinexOpen, coinexHigh, coinexLow, coinexClose] = getOhlcData("COINEX:" + coinexSymbol)

// Plotting the candles for each exchange
plotcandle(coinexOpen, coinexHigh, coinexLow, coinexClose, color=color.rgb(149, 78, 137, 100), bordercolor=color.rgb(149, 78, 137, 40), wickcolor=color.rgb(149, 78, 137, 40))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=coinexClose, x2=na, y2=coinexClose, width=1, color=color.new(color.red, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, coinexClose)
    line.set_y2(currentPriceLine, coinexClose)


// ############################################################################################################################################ 
// ##### COINEX VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################ 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Coinex USDT", shorttitle="ECI Vol - Coinex", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Coinex
coinexSymbol = baseCurrency + "USDT"

// Fetch the volume data from Coinex
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

coinexVolume = getVolumeData("Coinex:" + coinexSymbol)

// Plotting the volume for Coinex
plot(coinexVolume, title="Coinex Volume", color=color.rgb(149, 78, 137, 80), style=plot.style_columns, linewidth=1)














// ########################################################################### 
// ##### BITGET CANDLE COMPARE INDICATOR (UNCHECK VALUES IN STATUS LINE) #####
// ########################################################################### 

// Don't get rekt trading like a newb when other exchanges are trading lower. -XA-

//@version=5
// Exchange Comparison Indicator - Candles
indicator("ECI - Bitget USDT", shorttitle="ECI - Bitget", overlay=true)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pairs for Bitget
bitgetSymbol = baseCurrency + "USDT"

// Fetch the OHLC data from each exchange
getOhlcData(exchangeSymbol) =>
    o = request.security(exchangeSymbol, timeframe.period, open)
    h = request.security(exchangeSymbol, timeframe.period, high)
    l = request.security(exchangeSymbol, timeframe.period, low)
    c = request.security(exchangeSymbol, timeframe.period, close)
    [o, h, l, c]

[bitgetOpen, bitgetHigh, bitgetLow, bitgetClose] = getOhlcData("BITGET:" + bitgetSymbol)

// Plotting the candles for each exchange
plotcandle(bitgetOpen, bitgetHigh, bitgetLow, bitgetClose, color=color.rgb(84, 198, 147, 100), bordercolor=color.rgb(84, 198, 147, 85), wickcolor=color.rgb(84, 198, 147, 85))

// Create a dynamic horizontal line for the current price
var line currentPriceLine = na
if na(currentPriceLine)
    currentPriceLine := line.new(x1=na, y1=bitgetClose, x2=na, y2=bitgetClose, width=1, color=color.new(color.red, 75), extend=extend.both)
else
    line.set_y1(currentPriceLine, bitgetClose)
    line.set_y2(currentPriceLine, bitgetClose)


// ############################################################################################################################################ 
// ##### COINEX VOLUME COMPARE INDICATOR (MERGE WITH MAIN CHART, THEN SET OPACITY TO 0% ON STYLE, THEN PIN TO SCALE FULL SCREEN NO SCALE) #####
// ############################################################################################################################################ 

// Check how much volume is being traded on other exchanges without needing to pull up that chart. -XA-

//@version=5
// Exchange Comparison Indicator - Volume
indicator("ECI Vol - Bitget USDT", shorttitle="ECI Vol - Bitget", overlay=true, scale=scale.none)

// Extract the base currency from the current symbol
baseCurrency = syminfo.basecurrency

// Define the pair for Bitget
bitgetSymbol = baseCurrency + "USDT"

// Fetch the volume data from Bitget
getVolumeData(exchangeSymbol) =>
    v = request.security(exchangeSymbol, timeframe.period, volume)
    v

bitgetVolume = getVolumeData("Bitget:" + bitgetSymbol)

// Plotting the volume for Bitget
plot(bitgetVolume, title="Bitget Volume", color=color.rgb(75, 35, 80, 100), style=plot.style_columns, linewidth=1)
