using System;
using System.Windows.Input;

namespace TodoListWPF.Helpers
{
    public class RelayCommand : ICommand
    {
        private Action mAction;
        private object v;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            mAction();
        }
    }
}
