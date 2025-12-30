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
using Natural_Selection_Sim.MVVM;

namespace Natural_Selection_Sim.UserControls
{
    /// <summary>
    /// Interaction logic for PlayPauseButton.xaml
    /// </summary>
    
    public partial class PlayPauseButton : UserControl
    {



        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(PlayPauseButton));


        public RelayCommand PlayCommand
        {
            get { return (RelayCommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.Register("PlayCommand", typeof(RelayCommand), typeof(PlayPauseButton));



        public RelayCommand PauseCommand
        {
            get { return (RelayCommand)GetValue(PauseCommandProperty); }
            set { SetValue(PauseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PauseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PauseCommandProperty =
            DependencyProperty.Register("PauseCommand", typeof(RelayCommand), typeof(PlayPauseButton));



        public PlayPauseButton()
        {
            InitializeComponent();
        }
    }
}
