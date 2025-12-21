using LiveCharts;
using LiveCharts.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Natural_Selection_Sim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public ChartData[] Data { get; set; } ={
            new("Series 1",     [ 2, 5, 4 ]),
           new("Series 2", [ 5, 4, 1 ])
        };
        public class ChartData(string name, int[] points)
        {
            public string Name { get; set; } = name;
            public int[] Values { get; set; } = points;
        }
        public SeriesCollection Series { get; set; } = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Population",
            Values = new ChartValues<int>{1,2,3,4,5}
}
        };

private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {
            
            Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Population",
            Values = new ChartValues<int>{1,2,3,4,5}
            }
        };
        }
    }
}