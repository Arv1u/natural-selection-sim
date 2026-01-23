using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Natural_Selection_Sim.UserControls
{
    /// <summary>
    /// Interaction logic for StartingValuesControl.xaml
    /// </summary>
    public partial class StartingValuesControl : UserControl
    {
        public StartingValuesControl()
        {
            InitializeComponent();
            StartingValuesIsEnabled = true;
        }
        public static DependencyProperty PopulationEnabledProperty = DependencyProperty.Register("PopulationEnabled", typeof(bool), typeof(StartingValuesControl));
        public bool PopulationEnabled 
        { 
            get => (bool)GetValue(PopulationEnabledProperty);
            set => SetValue(PopulationEnabledProperty, value); 
        }
        public static DependencyProperty PopulationNameProperty = DependencyProperty.Register("PopulationName", typeof(string), typeof(StartingValuesControl));
        public string PopulationName
        {
            get => (string)GetValue(PopulationNameProperty);
            set => SetValue(PopulationNameProperty, value);
        }


        public bool StartingValuesIsEnabled 
        {
            get { return (bool)GetValue(StartingValuesIsEnabledProperty); }
            set { SetValue(StartingValuesIsEnabledProperty, value); }
        }

        public static readonly DependencyProperty StartingValuesIsEnabledProperty =
            DependencyProperty.Register("StartingValuesIsEnabled", typeof(bool), typeof(StartingValuesControl));

   
    }
}
