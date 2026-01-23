using Natural_Selection_Sim.MVVM;
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
    /// Interaction logic for ResetButton.xaml
    /// </summary>
    public partial class ResetButton : UserControl
    {




        public RelayCommand ResetCommand
        {
            get { return (RelayCommand)GetValue(ResetCommandProperty); }
            set { SetValue(ResetCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResetCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResetCommandProperty =
            DependencyProperty.Register("ResetCommand", typeof(RelayCommand), typeof(ResetButton));



        public ResetButton()
        {
            InitializeComponent();
        }
    }
}
