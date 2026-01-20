using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Natural_Selection_Sim.UserControls
{
    public partial class TablePercInput : UserControl, INotifyPropertyChanged
    {
        public TablePercInput()
        {
            InitializeComponent();
        }
        // the actual value the SpeciesData starting value binds to
        public static readonly DependencyProperty PercentValueProperty =
            DependencyProperty.Register(
                nameof(PercentValue),
                typeof(double),
                typeof(TablePercInput),
                new PropertyMetadata(OnPercentValueChanged));
        private static void OnPercentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) // called whenever PercentValue is changed
        {
            var control = (TablePercInput)d;
            control.OnPropertyChanged(nameof(DisplayValue)); // updates the display value 
        }
        public double PercentValue
        {
            get => (double)GetValue(PercentValueProperty);
            set
            {
                value = Math.Round(value, 2);

                if (value < 0 || value > 1) return; // percentage can't be negative / higher than 1

                SetValue(PercentValueProperty, value);
                OnPropertyChanged(nameof(DisplayValue));
                Debug.WriteLine(value);
            }
        }
        public int DisplayValue // actual text being displayed
        {
            get
            {
                return (int)(PercentValue*100);
            }
            set
            {
                PercentValue = (double)value / 100;
            }
        }

        //https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private static readonly Regex _regex = new("[^0-9.]");

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            PercentValue += 0.01;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            PercentValue -= 0.01;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(tb.Text)) // prevents empty text
            {
                tb.Text = Convert.ToString(DisplayValue);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) // selects all text on tbx focus
        {
            var tb = (TextBox)sender;

            tb.SelectAll();
        }
        //https://stackoverflow.com/questions/660554/how-to-automatically-select-all-text-on-focus-in-wpf-textbox
        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) // prevents leftclick unselecting text on click
        {
            var tb = sender as TextBox;
            if (!tb.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                tb.Focus();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
