using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Natural_Selection_Sim.MVVM
{
    /// <summary>
    /// Allows WPF to bind UI actions such as button clicks to code-behind methods while automatically updating UI state via the CanExecute() method.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// A method which is called when the command is executed.
        /// </summary>
        private readonly Action<object?> _execute;

        /// <summary>
        /// A method which determines if the command can execute.
        /// </summary>
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
