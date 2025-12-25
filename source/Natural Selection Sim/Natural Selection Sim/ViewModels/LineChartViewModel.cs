using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
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
		public LineChartViewModel()
		{

        }
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
        public SolidColorPaint LedgendTextPaint { get; set; } =
        new SolidColorPaint
        {
            Color = SKColors.White,
        };


        public void AddSeries(LineSeries<int> data)
		{
			Series?.Add(data);
		}
		public void Reset()
		{
			Series.Clear();
        }
	}
}
