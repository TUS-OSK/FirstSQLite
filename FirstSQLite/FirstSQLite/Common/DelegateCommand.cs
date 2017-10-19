using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstSQLite.Common
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<bool> canExecute;
        private Action<object> execute;

        public DelegateCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => this.canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void RaiseCanExecuteChanged()
            => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
