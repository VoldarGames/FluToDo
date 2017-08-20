using System.Windows.Input;
using Domain;
using Domain.Factories;
using FluToDo.Commands;
using FluToDo.ViewModels.Base;
using FluToDo.Views;
using Xamarin.Forms;

namespace FluToDo.ViewModels
{
    public class DetailToDoItemViewModel : ViewModelBase
    {
        private TodoItem _selectedItem = ToDoItemFactory.CreateNew();
        public ICommand AddCommand { get; set; } = new AddCommand<TodoItem>();

        public TodoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public DetailToDoItemViewModel()
        {
            MessagingCenter.Subscribe<string>(this,GlobalMessagingLocation.ClearDetailTodoView,s => ClearSelectedItem());
        }
        public override Page GetView()
        {
            return new DetailTodoItemView() {BindingContext = this};
        }

        public void ClearSelectedItem()
        {
            SelectedItem = ToDoItemFactory.CreateNew();
        }
    }
}