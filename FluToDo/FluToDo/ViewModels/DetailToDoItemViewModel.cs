using System.Windows.Input;
using Domain;
using FluToDo.Commands;
using FluToDo.ViewModels.Base;
using FluToDo.Views;
using Xamarin.Forms;

namespace FluToDo.ViewModels
{
    public class DetailToDoItemViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; set; } = new AddCommand<TodoItem>();
        public TodoItem SelectedItem { get; set; } = new TodoItem();
        public override Page GetView()
        {
            return new DetailTodoItemView() {BindingContext = this};
        }
    }
}