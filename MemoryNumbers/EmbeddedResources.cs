namespace MemoryNumbers;

/// <summary>
/// Load graphics resources from disk
/// </summary>
public class Resources
{
	public static string AppLogo { get; set; } = @"images\logo.ico";
	public static string AppLogo256 { get; set; } = @"images\logo@256.png";
	public static string IconExit { get; set; } = @"images\exit.ico";
	public static string IconStart { get; set; } = @"images\start.ico";
	public static string IconStop { get; set; } = @"images\stop.ico";
	public static string IconSound { get; set; } = @"images\soundoff.ico";
	public static string IconStats { get; set; } = @"images\graph.ico";
	public static string IconSettings { get; set; } = @"images\settings.ico";
	public static string IconAbout { get; set; } = @"images\about.ico";

	public static string SVGCorrect { get; set; } = @"images\Sequence correct.svg";
	public static string SVGWrong { get; set; } = @"images\Sequence wrong.svg";

	public static string WavCorrectNum { get; set; } = @"audio\Correct number.wav";
	public static string WavWrongNum { get; set; } = @"audio\Wrong number.wav";
	public static string WavCorrectSeq { get; set; } = @"audio\Correct sequence.wav";
	public static string WavWrongSeq { get; set; } = @"audio\Wrong sequence.wav";
	public static string WavCountDown { get; set; } = @"audio\Count down.wav";
	public static string WavEnd { get; set; } = @"audio\End game.wav";

	/// <summary>
	/// Loads a graphics resource from a disk location
	/// </summary>
	/// <typeparam name="T">Type of resource to be loaded</typeparam>
	/// <param name="fileName">File name (absolute or relative to the working directory) to load resource from</param>
	/// <returns>The graphics resource</returns>
	public static T? Load<T> (string fileName)
    {
		T? resource = default;
		try
		{
			if (File.Exists(fileName))
			{
				if (typeof(T).Equals(typeof(System.Drawing.Image)))
					resource = (T)(object)Image.FromFile(fileName);
				else if (typeof(T).Equals(typeof(Icon)))
					resource = (T)(object)new Icon(fileName);
				else if (typeof(T).Equals(typeof(Cursor)))
					resource = (T)(object)new Cursor(fileName);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				$"Unexpected error while loading the {fileName} graphics resource.{Environment.NewLine}{ex.Message}",
				"Loading error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
		return resource;
	}
}