using System;
using System.Windows.Input;
using Core.Helpers;
using Xamarin.Forms;

namespace FluToDo.Commands
{
    public class NavigationCommand : ICommand
    {
        private readonly Page _destinationPage;
        public bool IsBusy { get; set; }
        public NavigationCommand(Page destinationPage)
        {
            _destinationPage = destinationPage;
        }
        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }

        public async void Execute(object parameter)
        {
            IsBusy = true;
            await NavigationService.CurrentNavigation.PushAsync(_destinationPage);
            IsBusy = false;
        }

        public event EventHandler CanExecuteChanged;
    }
}