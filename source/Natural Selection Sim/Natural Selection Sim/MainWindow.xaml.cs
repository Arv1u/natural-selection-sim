using LiveChartsCore.SkiaSharpView.WPF;
using LiveChartsCore.Themes;
using Natural_Selection_Sim.ViewModels;
using System.Windows;

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
            DataContext = new SimulationViewModel();
        }


    }   
}