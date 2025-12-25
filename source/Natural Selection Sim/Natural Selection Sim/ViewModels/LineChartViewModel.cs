using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace Natural_Selection_Sim.ViewModels
{
    /// <summary>
    ///	Definitions for axes, legend and linechart data.
    /// </summary>
    public class LineChartViewModel : PropertyChangedBase
    {
        /// <summary>
        /// All line series integer data to display on the chart.
        /// </summary>
        public ObservableCollection<LineSeries<int>?> Series { get; set; } = new();

        /// <summary>
        /// Defines implementation of the X-Axes.
        /// </summary>
        public Axis[] XAxes { get; set; } = new[]
        {
            new Axis
            {
                Name = "Population",
                TextSize = 14,
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        /// <summary>
        /// Defines implementation of the Y-Axes.
        /// </summary>
        public Axis[] YAxes { get; set; } = new[]
        {
            new Axis
            {
                Name = "Time",
                TextSize = 14,
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        /// <summary>
        /// Gets the paint used to render the legend text.
        /// </summary>
        public SolidColorPaint LedgendTextPaint { get; } = new SolidColorPaint
        {
            Color = SKColors.White,
        };

        /// <summary>
        /// Adds a line series of integer data to the collection of series displayed by the chart.
        /// </summary>
        /// <param name="data">The line series containing integer data points to add.</param>
        public void AddSeries(LineSeries<int> data)
        {
            Series?.Add(data);
        }

        /// <summary>
        ///	Resets all series data displayed on the chart.
        /// </summary>
        public void Reset()
        {
            Series.Clear();
        }
    }
}
