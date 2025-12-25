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
    /// Interaction logic for TableHeader.xaml
    /// </summary>
    public partial class TableHeader : UserControl
    {
        public TableHeader()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderNameProperty = DependencyProperty.Register("HeaderName", typeof(string), typeof(TableHeader));
        public string HeaderName 
        {
            get => (string)GetValue(HeaderNameProperty);
            set => SetValue(HeaderNameProperty,value);
        }
    }
}
