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
    /// Interaction logic for TableData.xaml
    /// </summary>
    public partial class TableData : UserControl
    {
        public TableData()
        {
            InitializeComponent();
        }
        public static DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(TableData));
        public string Data 
        {
            get => (string)GetValue(DataProperty); 
            set => SetValue(DataProperty,value); 
        }
    }
}
