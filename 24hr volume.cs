// ###############################################################################################
// ########## Multi-Timeframe Volume Monitor - Displays 15m, 1hr, 4hr, 24hr volumes ###########
// ########## Shows green if above minimum threshold, red if below minimum threshold ###########
// ###############################################################################################

//@version=6
indicator("Multi-Timeframe Volume", shorttitle="MTF Vol", overlay=true)

// === Input Settings ===
string tablePosition = input.string("Bottom Left", title="Table Position", options=["Top Left", "Top Center", "Top Right", "Middle Left", "Middle Center", "Middle Right", "Bottom Left", "Bottom Center", "Bottom Right"])
string text_size_option = input.string("Normal", title="Table Text Size", options=["Tiny", "Small", "Normal", "Large", "Huge"])
float minVolume = input.float(500000, title="Minimum Volume Threshold", minval=0, step=50000, tooltip="Volume threshold for color coding (green if above, red if below)")
int spacingColumns = input.int(2, title="Spacing Columns", minval=0, maxval=10, tooltip="Number of spacing columns on the right side of the table")

// Timeframe visibility options
bool show15m = input.bool(true, title="Show 15m Volume", group="Timeframes")
bool show1h = input.bool(true, title="Show 1hr Volume", group="Timeframes")
bool show4h = input.bool(true, title="Show 4hr Volume", group="Timeframes")
bool show24h = input.bool(true, title="Show 24hr Volume", group="Timeframes")

// Color configuration options
color aboveThresholdColor = input.color(color.green, title="Above Threshold Color", group="Colors", tooltip="Color when volume is above minimum threshold")
color belowThresholdColor = input.color(color.red, title="Below Threshold Color", group="Colors", tooltip="Color when volume is below minimum threshold")
int bgTransparency = input.int(80, title="Background Transparency", minval=0, maxval=100, group="Colors", tooltip="Transparency level (0 = opaque, 100 = fully transparent)")
color tableBackgroundColor = input.color(color.new(color.black, 100), title="Table Background Color", group="Colors", tooltip="Base background color for the table (usually fully transparent)")
color spacingColumnColor = input.color(color.new(#787b86, 100), title="Spacing Column Color", group="Colors", tooltip="Color for spacing columns on the right side")
bool useAggregateColor = input.bool(false, title="Use Aggregate Coloring", group="Colors", tooltip="When enabled, all rows share the same color based on average volume of enabled timeframes")

// Get current price for dollar volume calculations
float currentPrice = close

// Get volume for different timeframes
var float volume15m = na
var float volume1h = na
var float volume4h = na
var float volume24h = na

volume15m := request.security(syminfo.tickerid, "15", volume, barmerge.gaps_off, barmerge.lookahead_off)
volume1h := request.security(syminfo.tickerid, "60", volume, barmerge.gaps_off, barmerge.lookahead_off)
volume4h := request.security(syminfo.tickerid, "240", volume, barmerge.gaps_off, barmerge.lookahead_off)
volume24h := request.security(syminfo.tickerid, "D", volume[1], barmerge.gaps_off, barmerge.lookahead_on)

// Calculate current day volume for 24hr total
float currentDayVolume = volume
float totalVolume24h = volume24h + currentDayVolume

// Calculate USD volume for each timeframe
float volume15mUSD = volume15m * currentPrice
float volume1hUSD = volume1h * currentPrice
float volume4hUSD = volume4h * currentPrice
float volume24hUSD = totalVolume24h * currentPrice

// Calculate aggregate volume (average of enabled timeframes)
float aggregateVolume = 0.0
int enabledTimeframeCount = (show15m ? 1 : 0) + (show1h ? 1 : 0) + (show4h ? 1 : 0) + (show24h ? 1 : 0)
if enabledTimeframeCount > 0
    float totalEnabledVolume = (show15m ? volume15mUSD : 0) + (show1h ? volume1hUSD : 0) + (show4h ? volume4hUSD : 0) + (show24h ? volume24hUSD : 0)
    aggregateVolume := totalEnabledVolume / enabledTimeframeCount

// Format volume with appropriate suffix (K, M, B)
formatVolume(float vol) =>
    string result = ""
    if vol >= 1000000000
        result := str.tostring(vol / 1000000000, "#.##") + "B"
    else if vol >= 1000000
        result := str.tostring(vol / 1000000, "#.##") + "M"
    else if vol >= 1000
        result := str.tostring(vol / 1000, "#.##") + "K"
    else
        result := str.tostring(vol, "#.##")
    result

// Determine aggregate color based on average volume
color aggregateColor = aggregateVolume >= minVolume ? color.new(aboveThresholdColor, bgTransparency) : color.new(belowThresholdColor, bgTransparency)

// Determine colors based on volume vs minimum threshold for each timeframe
// When aggregate mode is on, all rows use the same color; otherwise individual colors
color volume15mColor = useAggregateColor ? aggregateColor : (volume15mUSD >= minVolume ? color.new(aboveThresholdColor, bgTransparency) : color.new(belowThresholdColor, bgTransparency))
color volume1hColor = useAggregateColor ? aggregateColor : (volume1hUSD >= minVolume ? color.new(aboveThresholdColor, bgTransparency) : color.new(belowThresholdColor, bgTransparency))
color volume4hColor = useAggregateColor ? aggregateColor : (volume4hUSD >= minVolume ? color.new(aboveThresholdColor, bgTransparency) : color.new(belowThresholdColor, bgTransparency))
color volume24hColor = useAggregateColor ? aggregateColor : (volume24hUSD >= minVolume ? color.new(aboveThresholdColor, bgTransparency) : color.new(belowThresholdColor, bgTransparency))

// Format the display text for each timeframe
string volume15mText = "15m: $" + formatVolume(volume15mUSD)
string volume1hText = "1hr: $" + formatVolume(volume1hUSD)
string volume4hText = "4hr: $" + formatVolume(volume4hUSD)
string volume24hText = "24hr: $" + formatVolume(volume24hUSD)

// Convert text size option to Pine Script size constant
string text_size = text_size_option == "Tiny" ? size.tiny : 
                   text_size_option == "Small" ? size.small : 
                   text_size_option == "Normal" ? size.normal : 
                   text_size_option == "Large" ? size.large : 
                   size.huge

// === Table Setup ===
// Convert string position to Pine Script position constant
string pos = tablePosition == "Top Left" ? position.top_left : 
             tablePosition == "Top Center" ? position.top_center : 
             tablePosition == "Top Right" ? position.top_right : 
             tablePosition == "Middle Left" ? position.middle_left : 
             tablePosition == "Middle Center" ? position.middle_center : 
             tablePosition == "Middle Right" ? position.middle_right : 
             tablePosition == "Bottom Left" ? position.bottom_left : 
             tablePosition == "Bottom Center" ? position.bottom_center : 
             position.bottom_right

// Calculate total columns (1 data column + spacing columns)
int totalColumns = 1 + spacingColumns

// Calculate how many timeframes are enabled
int enabledCount = (show15m ? 1 : 0) + (show1h ? 1 : 0) + (show4h ? 1 : 0) + (show24h ? 1 : 0)

// Ensure at least 1 row (in case all are disabled)
int totalRows = math.max(1, enabledCount)

// Create table with dynamic number of rows based on enabled timeframes
var table volumeTable = table.new(pos, totalColumns, totalRows, bgcolor=tableBackgroundColor)

if barstate.islast
    // Track current row index for dynamic placement
    int currentRow = 0
    
    // Row for 15m volume (only if enabled)
    if show15m
        table.cell(volumeTable, 0, currentRow, volume15mText, text_color=color.white, bgcolor=volume15mColor, text_size=text_size)
        currentRow += 1
    
    // Row for 1hr volume (only if enabled)
    if show1h
        table.cell(volumeTable, 0, currentRow, volume1hText, text_color=color.white, bgcolor=volume1hColor, text_size=text_size)
        currentRow += 1
    
    // Row for 4hr volume (only if enabled)
    if show4h
        table.cell(volumeTable, 0, currentRow, volume4hText, text_color=color.white, bgcolor=volume4hColor, text_size=text_size)
        currentRow += 1
    
    // Row for 24hr volume (only if enabled)
    if show24h
        table.cell(volumeTable, 0, currentRow, volume24hText, text_color=color.white, bgcolor=volume24hColor, text_size=text_size)
        currentRow += 1
    
    // Add dynamic spacing columns on the right for all enabled rows (only if spacing > 0)
    if spacingColumns > 0 and enabledCount > 0
        for row = 0 to enabledCount - 1
            for i = 0 to spacingColumns - 1
                table.cell(volumeTable, 1 + i, row, " ", text_color=color.white, bgcolor=spacingColumnColor, text_size=text_size)

// === Alert Conditions ===
// Alert when 24hr volume crosses above minimum threshold
alertcondition(ta.crossover(volume24hUSD, minVolume), title="24hr Volume Above Threshold", message="24hr volume crossed above minimum threshold")

// Alert when 24hr volume crosses below minimum threshold
alertcondition(ta.crossunder(volume24hUSD, minVolume), title="24hr Volume Below Threshold", message="24hr volume crossed below minimum threshold")
