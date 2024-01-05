using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayUI.Avalonia.Mvvm
{
    internal class LayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<T> _execute = null;
        public Func<T, bool> _canExecute = null;


        public LayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public LayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (canExecute != null)
            {
                this._canExecute = new Func<T, bool>(canExecute);
            }
            if (execute != null)
            {
                this._execute = new Action<T>(execute);
            }
        }

        public void RaiseCanExceuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }
            return _canExecute((T)(object)(parameter));
        }

        public void Execute(object parameter)
        {
            this._execute((T)(object)(parameter));
        }
    } 
}
