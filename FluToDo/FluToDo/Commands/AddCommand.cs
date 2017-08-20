using System;
using System.Windows.Input;
using Core.Helpers;
using Core.Interfaces;
using Xamarin.Forms;

namespace FluToDo.Commands
{
    public class AddCommand<T> : ICommand, ICrudCommand<T>
    {
        public bool IsBusy { get; set; }
        public Action<T> CustomAction { get; set; }
        public Action<T> FinallyAction { get; set; }
        public bool CanExecute(object parameter)
        {
            return !IsBusy && parameter != null;
        }

        public void Execute(object parameter)
        {
            IsBusy = true;
            CustomAction?.Invoke((T)parameter);
            var castedParameter = (T) parameter;
            InternalExecute(castedParameter);
            IsBusy = false;
            FinallyAction?.Invoke((T)parameter);
        }

        private async void InternalExecute(T castedParameter)
        {
            var apiClient = new ApiClient();
            var result = await apiClient.PostAsync(castedParameter);
            DependencyService.Get<IOperatingSystemMethods>()
                .ShowToast(result != null && result.Value
                    ? $"{castedParameter} {GlobalLocation.SuccessAdd}"
                    : $"{GlobalLocation.ErrorAdd} {castedParameter}");
            await NavigationService.CurrentNavigation.PopAsync();
        }

        public event EventHandler CanExecuteChanged;
        
    }
}