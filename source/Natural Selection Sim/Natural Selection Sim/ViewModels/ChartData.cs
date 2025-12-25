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
    public class ChartData : PropertyChangedBase
    {
        public readonly ObservableCollection<int> values = new() {};
        public LineSeries<int> Series { get; set; }
        public ChartData(string name)
        {
            Series = new()
            {
                Name = name,
                Values = values,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0,
                IsVisibleAtLegend = true,
                
            };
        }
        public void AddValue(int value)
        {
            values.Add(value);
        }
    }
}
