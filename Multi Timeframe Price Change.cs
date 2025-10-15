//@version=5
indicator("Multi Timeframe Price Change", shorttitle="MTF", overlay=true)

// User options to show/hide rows
bool show15m = input(true, title="Show 15m Change")
bool show1h = input(true, title="Show 1h Change")
bool show4h = input(true, title="Show 4h Change")
bool show1d = input(true, title="Show 1d Change")
bool show1w = input(true, title="Show 1w Change")
bool show1m = input(true, title="Show 1m Change")
bool show6m = input(true, title="Show 6m Change")
bool show12m = input(true, title="Show 12m Change")

// Function to format a number with a specified number of decimal places
formatNumber(value, decPlaces) =>
    scale = math.pow(10, decPlaces)
    formattedValue = math.round(value * scale) / scale
    str.tostring(formattedValue)

// Function to calculate percentage change
calcPercentChange(currentPrice, historicalPrice) =>
    ((currentPrice - historicalPrice) / historicalPrice) * 100

// Calculate the price
price15mAgo = request.security(syminfo.tickerid, "1", close[15])
price1hAgo = request.security(syminfo.tickerid, "1", close[60])
price4hAgo = request.security(syminfo.tickerid, "5", close[48])
price1dAgo = request.security(syminfo.tickerid, "60", close[24])
price1wAgo = request.security(syminfo.tickerid, "60", close[168])
price1mAgo = request.security(syminfo.tickerid, "D", close[30])
price6mAgo = request.security(syminfo.tickerid, "W", close[24])
price12mAgo = request.security(syminfo.tickerid, "W", close[52])

// Calculate the percentage changes
percentChange15m = calcPercentChange(close, price15mAgo)
percentChange1h = calcPercentChange(close, price1hAgo)
percentChange4h = calcPercentChange(close, price4hAgo)
percentChange1d = calcPercentChange(close, price1dAgo)
percentChange1w = calcPercentChange(close, price1wAgo)
percentChange1m = calcPercentChange(close, price1mAgo)
percentChange6m = calcPercentChange(close, price6mAgo)
percentChange12m = calcPercentChange(close, price12mAgo)

// Determine the number of decimal places for formatting
decimalPlaces = syminfo.mintick > 1 ? 0 : int(math.abs(math.log10(syminfo.mintick)))

// Define colors for positive and negative changes
colorPositive = color.new(color.green, 80)
colorNegative = color.new(color.red, 80)

// Create a table
var table infoTable = table.new(position.top_right, 1, 8, bgcolor=color.new(#000000, 0))
whiteTextColor = color.white
smallTextSize = size.small

if barstate.islast
    int row = 0
    if show15m
        table.cell(infoTable, 0, row, "15m: " + formatNumber(percentChange15m, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange15m >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show1h
        table.cell(infoTable, 0, row, "1h: " + formatNumber(percentChange1h, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange1h >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show4h
        table.cell(infoTable, 0, row, "4h: " + formatNumber(percentChange4h, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange4h >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show1d
        table.cell(infoTable, 0, row, "1d: " + formatNumber(percentChange1d, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange1d >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show1w
        table.cell(infoTable, 0, row, "1w: " + formatNumber(percentChange1w, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange1w >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show1m
        table.cell(infoTable, 0, row, "1m: " + formatNumber(percentChange1m, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange1m >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show6m
        table.cell(infoTable, 0, row, "6m: " + formatNumber(percentChange6m, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange6m >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
        row := row + 1
    if show12m
        table.cell(infoTable, 0, row, "12m: " + formatNumber(percentChange12m, 2) + "%", text_color=whiteTextColor, bgcolor=percentChange12m >= 0 ? colorPositive : colorNegative, text_size=smallTextSize)
