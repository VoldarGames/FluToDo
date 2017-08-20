using System;
using System.Windows.Input;
using Core.Utils;
using Domain;
using FluToDo.Commands;
using FluToDo.ViewModels.Base;
using Xamarin.Forms;

namespace FluToDo.Views
{
    public class DetailTodoItemView : ContentPage
    {
        public DetailTodoItemView()
        {
            SetBindings();
            Content = new StackLayout()
            {
                Children =
                {
                    ToDoHeaderLabel,
                    ToDoEntry,
                    SubmitButton
                }
            };
        }

        private void SetBindings()
        {
            ToDoEntry.SetBinding(Entry.TextProperty,BindingMaker.Make(nameof(DetailToDoItemViewModel.SelectedItem), nameof(TodoItem.Name)));
            SubmitButton.SetBinding(Button.CommandProperty, nameof(DetailToDoItemViewModel.AddCommand));
            SubmitButton.SetBinding(Button.CommandParameterProperty,nameof(DetailToDoItemViewModel.SelectedItem));
        }

        public Button SubmitButton { get; set; } = new Button();

        public Entry ToDoEntry { get; set; } = new Entry() {Placeholder = "Enter ToDo name..."};

        public Label ToDoHeaderLabel { get; set; } = new Label() {Text = "ToDo Item Name:" };
    }

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