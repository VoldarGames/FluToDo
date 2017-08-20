using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Helpers;
using Core.Interfaces;
using Domain;
using Xamarin.Forms;

namespace FluToDo.Commands
{
    public class DeleteCommand<T> : ICommand, ICrudCommand<T> where T : EntityBase
    {
        public bool IsBusy { get; set; }
        public Action<T> CustomAction { get; set; }
        public Action<T> FinallyAction { get; set; }
        public bool CanExecute(object parameter)
        {
            return !IsBusy && parameter != null;
        }

        public async void Execute(object parameter)
        {
            IsBusy = true;
            CustomAction?.Invoke((T)parameter);
            var castedParameter = (T) parameter;
            await InternalExecute(castedParameter);
            IsBusy = false;
            FinallyAction?.Invoke((T)parameter);
        }

        private async Task InternalExecute(T castedParameter)
        {
            var apiClient = DependencyService.Get<IApiClient>();
            var result = await apiClient.DeleteAsync(castedParameter);
            DependencyService.Get<IOperatingSystemMethods>()
                .ShowToast(result != null && result.Value
                    ? $"{castedParameter} {GlobalLocation.SuccessDelete}"
                    : $"{GlobalLocation.ErrorDelete} {castedParameter}");
            await NavigationService.CurrentNavigation.PopAsync();
        }

        public event EventHandler CanExecuteChanged;
        
    }
}