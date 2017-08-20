using System;
using System.Windows.Input;
using Core.Helpers;
using Core.Interfaces;
using Domain;
using Xamarin.Forms;

namespace FluToDo.Commands
{
    public class UpdateCommand<T> : ICommand, ICrudCommand<T>
    {
        public bool IsBusy { get; set; }

        public Action<T> CustomAction { get; set; }
        public Action<T> FinallyAction { get; set; }
        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }

        public async void Execute(object parameter)
        {
            IsBusy = true;
            CustomAction?.Invoke((T)parameter);
            var apiClient = DependencyService.Get<IApiClient>();
            var result = await apiClient.PutAsync((TodoItem)parameter);
            DependencyService.Get<IOperatingSystemMethods>()
                .ShowToast(result != null && result.Value
                    ? $"{((TodoItem) parameter).Name} {GlobalLocation.SuccessPut}"
                    : $"{GlobalLocation.ErrorPut} {((TodoItem) parameter).Name}");
            IsBusy = false;
            FinallyAction?.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}