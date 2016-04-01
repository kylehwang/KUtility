using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KUtility.WPF
{
    /// <summary>
    /// Base command class to be inherited to create commands for WPF
    /// </summary>
    public class CommandBase : ICommand
    {

        private Func<Boolean> canExecute;
        private bool _canExecute = false;
        private Action execute_NoParams;
        private Action<object> execute_WithParams;

        public event EventHandler CanExecuteChanged;

        public CommandBase(Action execute)
            : this(execute, () => true)
        {
        }

        public CommandBase(Action execute, Func<Boolean> canExecute)
        {
            this.execute_NoParams = execute;
            this.canExecute = canExecute;
        }

        public CommandBase(Action<object> execute) 
            : this(execute, () => true) 
        {
        }

        public CommandBase(Action<object> execute, Func<Boolean> canExecute)
        {
            this.execute_WithParams = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecute != canExecute())
            {
                _canExecute = canExecute();
            }
            return _canExecute;
        }


        public void Execute(object parameter)
        {
            if (execute_NoParams != null) { execute_NoParams(); }
            else { execute_WithParams(parameter); }
        }

        public void TriggerCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
