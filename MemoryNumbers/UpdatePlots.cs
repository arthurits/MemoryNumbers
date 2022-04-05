using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MemoryNumbers;
partial class FrmMain
{
    /// <summary>
    /// Cleans and updates the chartStatsNumbers with the data returned from _game.GetStats
    /// </summary>
    private void ChartStatsNumbers_Update()
    {
        _game.GetStats.Sort((x, y) => x.Number.CompareTo(y.Number));

        List<(int Number, int Total, int Correct)> FullStats = new();
        FullStats.Add(_game.GetStats[0]);
        for (int i = 1; i < _game.GetStats.Count; i++)
        {
            FullStats.InsertRange(i, Enumerable.Repeat((0, 0, 0), _game.GetStats[i].Number - _game.GetStats[i - 1].Number - 1));
            FullStats.Add(_game.GetStats[i]);
        }

        var correct = FullStats.Select(x => x.Total == 0 ? 0.0 : 100 * (double)x.Correct / x.Total).ToArray();
        var incorrect = FullStats.Select(x => x.Total == 0 ? 0.0 : (double)100).ToArray();

        // Reset plot
        StatsNumbers.Plot.Clear();
        StatsNumbers.Plot.XAxis.Label("Number");
        StatsNumbers.Plot.YAxis.Label("OK percentage");

        // Plot data
        var positions = FullStats.Select(x => (double)x.Number).ToArray();
        StatsNumbers.Plot.AddBar(incorrect, positions, Color.Red);
        StatsNumbers.Plot.AddBar(correct, positions, Color.Green);
        StatsNumbers.Plot.XAxis.ManualTickPositions(positions, positions.Select(x => $"#{x}").ToArray());
        StatsNumbers.Plot.XAxis.ManualTickSpacing(1.0);
        
        // Adjust axis limits so there is no padding below the bar graph
        StatsNumbers.Plot.SetAxisLimits(yMin: 0);
        StatsNumbers.Refresh();

    }

    /// <summary>
    /// Cleans and updates the chartStatsNumbers with the data returned from _game.GetStats
    /// </summary>
    private void ChartStatsTime_Update()
    {
        var data = new List<List<double>>(_game.GetStatsTime);

        // Get the number of rows
        int numAttempts = data.Count;
        // Get the maximum number of columns
        int numSeries = data.Aggregate(0, (max, next) => next.Count > max ? next.Count : max);

        // Pad with 0 so that each row has the same number of elements
        data.ForEach(x => x.AddRange(Enumerable.Repeat(0.0, numSeries - x.Count)));

        // Add the partial incremental sum
        var times = new List<List<double>>();
        foreach (var row in data)
        {
            times.Add(new List<double>());
            row.ForEach(i => times.Last().Add(i + times.Last().LastOrDefault()));
        }

        // Traspose to get the data ready to plot
        var plotData = times
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();

        // Reset plot
        StatsTime.Plot.Clear();
        StatsTime.Plot.XAxis.Label("Sequence");
        StatsTime.Plot.YAxis.Label("Time in seconds");

        // Plot data
        var positions = Enumerable.Range(1, data.Count).Select(x => (double)x).ToArray();
        plotData.Reverse();
        foreach (var row in plotData)
            StatsTime.Plot.AddBar(row.ToArray(), positions);

        StatsTime.Plot.XAxis.ManualTickPositions(positions, positions.Select(x => $"{x}").ToArray());
        StatsTime.Plot.XAxis.ManualTickSpacing(1);
        StatsTime.Refresh();

    }
}
