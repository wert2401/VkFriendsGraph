using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VkFriendsGraph.ViewModels
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> execute;

        public RelayCommand(Action<object> action)
        {
            execute = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
