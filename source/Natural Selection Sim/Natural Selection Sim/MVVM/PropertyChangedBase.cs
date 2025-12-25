using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Selection_Sim.MVVM
{
    /// <summary>
    /// Provides a base class that implements the INotifyPropertyChanged interface to support property change
    /// notification.
    /// </summary>
    /// <remarks>Inherit from this class to enable property change notifications in derived types, typically
    /// for use with data binding in UI frameworks such as WPF, UWP, or Xamarin.Forms. The PropertyChanged event is
    /// raised whenever a property value changes, allowing observers to react accordingly.</remarks>
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <remarks>This event is typically raised by classes that implement the INotifyPropertyChanged
        /// interface to notify clients, such as data-binding frameworks, that a property value has changed.</remarks>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        ///  Notifies UI to update the bound property value.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
