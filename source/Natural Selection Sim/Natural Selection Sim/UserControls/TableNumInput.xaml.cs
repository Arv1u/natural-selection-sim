using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TableNumInput.xaml
    /// </summary>
    public partial class TableNumInput : UserControl
    {
        public TableNumInput()
        {
            InitializeComponent();
            IntInput = 0;
        }

        public static DependencyProperty NumInputIsEnabledProperty = DependencyProperty.Register("NumInputIsEnabled", typeof(bool), typeof(TableNumInput));

        public bool NumInputIsEnabled
        {
            get { return (bool)GetValue(NumInputIsEnabledProperty); }
            set { SetValue(NumInputIsEnabledProperty, value); }
        }
        //https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new("[^0-9.-]+");
        private bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }



        public int IntInput
        {
            get { return (int)GetValue(IntInputProperty); }
            set 
            {
                if (value < 0)
                    return;
                SetValue(IntInputProperty, value);
                OnPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for IntInput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntInputProperty =
            DependencyProperty.Register("IntInput", typeof(int), typeof(TableNumInput), new PropertyMetadata());


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IntInput++;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IntInput--;
        }
    }
}
