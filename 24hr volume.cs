// ###############################################################################################
// ########## 24 Hour Volume Monitor - Displays current pair 24hr volume with alert ###########
// ########## Shows green if above minimum threshold, red if below minimum threshold ###########
// ###############################################################################################

//@version=6
indicator("24hr Volume Monitor", shorttitle="24hr Vol", overlay=true)

// === Input Settings ===
string tablePosition = input.string("Bottom Left", title="Table Position", options=["Top Left", "Top Center", "Top Right", "Middle Left", "Middle Center", "Middle Right", "Bottom Left", "Bottom Center", "Bottom Right"])
string text_size_option = input.string("Normal", title="Table Text Size", options=["Tiny", "Small", "Normal", "Large", "Huge"])
float minVolume = input.float(500000, title="Minimum Volume Threshold", minval=0, step=50000, tooltip="Volume threshold for color coding (green if above, red if below)")
int spacingColumns = input.int(2, title="Spacing Columns", minval=0, maxval=10, tooltip="Number of spacing columns on the right side of the table")

// Get 24-hour volume
var float volume24h = na
volume24h := request.security(syminfo.tickerid, "D", volume[1], barmerge.gaps_off, barmerge.lookahead_on)

// Calculate current candle volume accumulation for the current day
float currentDayVolume = volume

// Get the total 24hr volume (previous day completed + current day so far)
float totalVolume24h = volume24h + currentDayVolume

// Get the current price to calculate dollar volume
float currentPrice = close

// Calculate 24hr volume in USD
float volumeUSD = totalVolume24h * currentPrice

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

// Determine color based on volume vs minimum threshold
color volumeColor = volumeUSD >= minVolume ? color.new(color.green, 80) : color.new(color.red, 80)

// Format the display text
string volumeText = "24 hr: $" + formatVolume(volumeUSD)

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

// Create table with dynamic position and columns
var table volumeTable = table.new(pos, totalColumns, 1, bgcolor=color.new(#000000, 100))

if barstate.islast
    // Display the 24hr volume
    table.cell(volumeTable, 0, 0, volumeText, text_color=color.white, bgcolor=volumeColor, text_size=text_size)
    
    // Add dynamic spacing columns on the right (only if spacing > 0)
    if spacingColumns > 0
        for i = 0 to spacingColumns - 1
            table.cell(volumeTable, 1 + i, 0, " ", text_color=color.white, bgcolor=color.new(#787b86, 100), text_size=text_size)

// === Alert Conditions ===
// Alert when volume crosses above minimum threshold
alertcondition(ta.crossover(volumeUSD, minVolume), title="Volume Above Threshold", message="24hr volume crossed above minimum threshold")

// Alert when volume crosses below minimum threshold
alertcondition(ta.crossunder(volumeUSD, minVolume), title="Volume Below Threshold", message="24hr volume crossed below minimum threshold")
