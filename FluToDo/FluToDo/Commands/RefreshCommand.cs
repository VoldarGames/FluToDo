using System;
using System.Windows.Input;
using FluToDo.Interfaces;

namespace FluToDo.Commands
{
    public class RefreshCommand : ICommand
    {
        private readonly IHasRefreshList _context;
        public bool IsBusy { get; set; }
        public RefreshCommand(IHasRefreshList context)
        {
            _context = context;
        }

        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }

        public void Execute(object parameter)
        {
            IsBusy = true;
            _context.RefreshList();
            IsBusy = false;
        }

        public event EventHandler CanExecuteChanged;
    }
}