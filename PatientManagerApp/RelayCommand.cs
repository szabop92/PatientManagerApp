using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PatientManagerApp
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            _execute(parameters);
        }

        #endregion // ICommand Members
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        //
        // Summary:
        //     Initializes a new instance of the RelayCommand class that can always execute.
        //
        // Parameters:
        //   execute:
        //     The execution logic.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     If the execute argument is null.
        public RelayCommand(Action<T> execute)
         : this(execute, null)
        {
            _execute = execute;
        }

        //
        // Summary:
        //     Initializes a new instance of the RelayCommand class.
        //
        // Parameters:
        //   execute:
        //     The execution logic.
        //
        //   canExecute:
        //     The execution status logic.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     If the execute argument is null.
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        //
        // Summary:
        //     Occurs when changes occur that affect whether the command should execute.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //
        // Summary:
        //     Defines the method that determines whether the command can execute in its current
        //     state.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to a null reference
        //
        // Returns:
        //     true if this command can be executed; otherwise, false.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        //
        // Summary:
        //     Defines the method to be called when the command is invoked.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to a null reference
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
