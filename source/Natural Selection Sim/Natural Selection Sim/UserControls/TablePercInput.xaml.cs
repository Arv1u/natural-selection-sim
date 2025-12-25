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
            PercentValue = 0.00;
            _editingText = $"{PercentValue * 100:0}";
        }

        public static readonly DependencyProperty NumInputIsEnabledProperty =
            DependencyProperty.Register(nameof(NumInputIsEnabled), typeof(bool), typeof(TablePercInput));

        public bool NumInputIsEnabled
        {
            get => (bool)GetValue(NumInputIsEnabledProperty);
            set => SetValue(NumInputIsEnabledProperty, value);
        }

        public static readonly DependencyProperty PercentValueProperty =
            DependencyProperty.Register(
                nameof(PercentValue),
                typeof(double),
                typeof(TablePercInput),
                new PropertyMetadata(0.0, OnPercentValueChanged));

        private static void OnPercentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TablePercInput control)
            {
                control._editingText = $"{control.PercentValue * 100:0}";
                control.OnPropertyChanged(nameof(DisplayValue));
            }
        }

        public double PercentValue
        {
            get => (double)GetValue(PercentValueProperty);
            set
            {
                value = Math.Round(value, 2);
                if (value < 0 || value > 1) return;
                SetValue(PercentValueProperty, value);
                OnPropertyChanged(nameof(DisplayValue));
                Debug.WriteLine(value);
            }
        }

        private string _editingText;
        public string DisplayValue
        {
            get => _editingText + "%";
            set
            {
                _editingText = value.Replace("%", "");
                OnPropertyChanged();
            }
        }

        private static readonly Regex _regex = new("[^0-9.]");

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox tb)
                tb.SelectAll();
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb && !tb.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                tb.Focus();
            }
        }

        private void CommitTextBox(TextBox tb)
        {
            if (double.TryParse(tb.Text.Replace("%", ""), out double parsed))
            {
                PercentValue = parsed / 100.0;
            }
            else
            {
                // Reset to current value if parsing fails
                _editingText = $"{PercentValue * 100:0}";
                OnPropertyChanged(nameof(DisplayValue));
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox tb)
            {
                CommitTextBox(tb);
                tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
                CommitTextBox(tb);
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            PercentValue += 0.01;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            PercentValue -= 0.01;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
