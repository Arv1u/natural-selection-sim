using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Natural_Selection_Sim.MVVM;
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
		//private ObservableCollection< LineSeries<int>?> series;

		public ObservableCollection<LineSeries<int>?> Series { get; set; } = new();

		public ObservableCollection<Axis> XAxis { get; set; } = new();
		public List<ChartData> ChartData = new();
		public LineChartViewModel()
		{

		}
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
