using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace MemoryNumbers;

public class ClassSettings
{
    /// <summary>
    /// Stores the settings file name
    /// </summary>
    [JsonIgnore]
    public string FileName { get; set; } = "configuration.json";

    /// <summary>
    /// Window top-left x coordinate
    /// </summary>
    [JsonPropertyName("Window left")]
    public int WindowLeft { get; set; } = 0;
    /// <summary>
    /// Window top-left y coordinate
    /// </summary>
    [JsonPropertyName("Window top")]
    public int WindowTop { get; set; } = 0;
    /// <summary>
    /// Window width
    /// </summary>
    [JsonPropertyName("Window width")]
    public int WindowWidth { get; set; } = 644;
    /// <summary>
    /// Window height
    /// </summary>
    [JsonPropertyName("Window height")]
    public int WindowHeight { get; set; } = 461;

    /// <summary>
    /// Time
    /// </summary>
    [JsonPropertyName("Time")]
    public int Time { get; set; } = 700;
    /// <summary>
    /// Time increment
    /// </summary>
    [JsonPropertyName("Time increment")]
    public int TimeIncrement { get; set; } = 0;
    /// <summary>
    /// Maximum digit
    /// </summary>
    [JsonPropertyName("Maximum digit")]
    public int MaximumDigit { get; set; } = 9;
    /// <summary>
    /// Minimum digit
    /// </summary>
    [JsonPropertyName("Minimum digit")]
    public int MinimumDigit { get; set; } = 0;
    /// <summary>
    /// Maximum number of attempts
    /// </summary>
    [JsonPropertyName("Maximum attempts")]
    public int MaximumAttempts { get; set; } = 10;
    /// <summary>
    /// Minimum length
    /// </summary>
    [JsonPropertyName("Minimum length")]
    public int MinimumLength { get; set; } = 2;
    /// <summary>
    /// CountDown ratio
    /// </summary>
    [JsonPropertyName("Countdown ratio")]
    public float CountDownRatio { get; set; } = 0.37f;
    /// <summary>
    /// Numbers ratio
    /// </summary>
    [JsonPropertyName("Numbers ratio")]
    public float NumbersRatio { get; set; } = 0.25f;
    /// <summary>
    /// Border ratio
    /// </summary>
    [JsonPropertyName("Border ratio")]
    public float BorderRatio { get; set; } = 0.12f;
    /// <summary>
    /// Font ratio
    /// </summary>
    [JsonPropertyName("Font ratio")]
    public float FontRatio { get; set; } = 0.55f;
    /// <summary>
    /// Results ratio
    /// </summary>
    [JsonPropertyName("Results ratio")]
    public float ResultsRatio { get; set; } = 0.56f;
    /// <summary>
    /// Font family name
    /// </summary>
    [JsonPropertyName("Font family")]
    public string FontFamilyName { get; set; } = "Microsoft Sans Serif";
    /// <summary>
    /// Background color
    /// </summary>
    [JsonPropertyName("Back color")]
    public int BackColor { get; set; } = System.Drawing.Color.White.ToArgb();
    /// <summary>
    /// Default splitter distance
    /// </summary>
    [JsonPropertyName("Splitter distance")]
    public int SplitterDistance { get; set; } = 265;
    /// <summary>
    /// Playing mode settings
    /// </summary>
    [JsonPropertyName("Playing mode")]
    public int PlayMode { get; set; } = 9;     //Fixed time (1) & random sequence (8)
    /// <summary>
    /// Remember window position on start up
    /// </summary>
    [JsonPropertyName("Window position")]
    public bool WindowPosition { get; set; } = true;   // Remember windows position
    /// <summary>
    /// Sound ToolStrip button cheched?
    /// </summary>
    [JsonPropertyName("Sound checked")]
    public bool Sound { get; set; } = false;        // Soundoff unchecked
    /// <summary>
    /// Stats ToolStrip button cheched?
    /// </summary>
    [JsonPropertyName("Stats checked")]
    public bool Stats { get; set; } = false;        // Stats unchecked
    

    /// <summary>
    /// Culture used throughout the app
    /// </summary>
    [JsonIgnore]
    public System.Globalization.CultureInfo AppCulture { get; set; } = System.Globalization.CultureInfo.CurrentCulture;
    /// <summary>
    /// Define the culture used throughout the app by asigning a culture string name
    /// </summary>
    [JsonPropertyName("Culture name")]
    public string AppCultureName
    {
        get { return AppCulture.Name; }
        set { AppCulture = new System.Globalization.CultureInfo(value); }
    }
    [JsonIgnore]
    public string? AppPath { get; set; } = Path.GetDirectoryName(Environment.ProcessPath);

    public ClassSettings()
    {
    }
}