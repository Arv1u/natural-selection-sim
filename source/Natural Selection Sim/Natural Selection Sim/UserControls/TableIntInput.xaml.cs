using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;

namespace Natural_Selection_Sim.UserControls
{
    /// <summary>
    /// Interaction logic for TableIntInput.xaml
    /// </summary>
    public partial class TableIntInput : UserControl
    {
        public TableIntInput()
        {
            InitializeComponent();
            IntInput = 0;
        }


        public bool NumInputIsEnabled
        {
            get { return (bool)GetValue(NumInputIsEnabledProperty); }
            set { SetValue(NumInputIsEnabledProperty, value); }
        }
        public static DependencyProperty NumInputIsEnabledProperty = DependencyProperty.Register("NumInputIsEnabled", typeof(bool), typeof(TableIntInput));

        //https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new("[^0-9.-]+"); // only number chars
        private bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }

        public int? IntInput
        {
            get { return (int?)GetValue(IntInputProperty); }
            set 
            {
                if (value < 0)
                    return;
                SetValue(IntInputProperty, value);
                Debug.WriteLine(value);
                OnPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for IntInput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntInputProperty =
            DependencyProperty.Register("IntInput", typeof(int), typeof(TableIntInput), new PropertyMetadata());


        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IntInput++;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IntInput--;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
