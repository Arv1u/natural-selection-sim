using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Natural_Selection_Sim.MVVM;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Selection_Sim.ViewModels
{
    public class LineChartViewModel : PropertyChangedBase
    {

		public ObservableCollection<LineSeries<int>?> Series { get; set; } = new();

		public ObservableCollection<Axis> XAxis { get; set; } = new();
		public List<ChartData> ChartData = new();
		public LineChartViewModel()
		{

		}
		public Axis[] XAxes { get; set; } = new[]
		{
			new Axis
			{
				Name = "X Axis",
				LabelsPaint = new SolidColorPaint(SKColors.Black),
				SeparatorsPaint = new SolidColorPaint(SKColors.Gray),
				TicksPaint = new SolidColorPaint(SKColors.Gray),
				TextSize = 14
			}
		};
        public Axis[] YAxes { get; set; } = new[]
		{
			new Axis
			{
				Name = "Y Axis",
				LabelsPaint = new SolidColorPaint(SKColors.Blue),
				SeparatorsPaint = new SolidColorPaint(SKColors.Blue),
				TicksPaint = new SolidColorPaint(SKColors.Blue),
                TextSize = 14

            }
		};

        public void AddData(ChartData data)
		{
			Series?.Add(data.Series);
			
			ChartData.Add(data);
		}
		public void Reset()
		{
			Series.Clear();
			ChartData.Clear();
        }
	}
}
