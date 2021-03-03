using System;
using System.Windows.Input;

namespace ObjectDetection.ViewModel.Command
{
    public class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public string Name { get; }

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public RelayCommand(Action<object> execute, string name)
        {
            Name = name;
            this._canExecute = (s) => { return true; };
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
