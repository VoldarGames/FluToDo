using System;
using Core.Utils;
using Domain;
using FluToDo.Styles;
using FluToDo.ViewModels;
using Xamarin.Forms;

namespace FluToDo.Views
{
    public class DetailTodoItemView : ContentPage
    {
        public Button SubmitButton { get; set; } = new Button()
        {
            Text = GlobalLocation.AddText,
            Style = GlobalStyles.SubmitButton
        };

        public Entry ToDoEntry { get; set; } = new Entry() { Placeholder = GlobalLocation.ToDoEntryPlaceHolder };
        public Label ToDoHeaderLabel { get; set; } = new Label() { Text = GlobalLocation.ToDoHeader, Style = GlobalStyles.HeaderLabel };

        public DetailTodoItemView()
        {
            SetBindings();
            BuildUserInterface();
        }

        private void BuildUserInterface()
        {
            Title = GlobalLocation.FluToDoDetailTitle;
            Icon = new FileImageSource() {File = GlobalDrawableLocation.FluToDoIcon};
            Content = new Frame()
            {
                Style = GlobalStyles.FrameStyle,
                Content = new StackLayout()
                {
                    Children =
                    {
                        ToDoHeaderLabel,
                        ToDoEntry,
                        SubmitButton
                    }
                }
            };
        }

        private void SetBindings()
        {
            ToDoEntry.SetBinding(Entry.TextProperty,BindingMaker.Make(nameof(DetailToDoItemViewModel.SelectedItem), nameof(TodoItem.Name)),BindingMode.TwoWay);
            SubmitButton.SetBinding(Button.CommandProperty, nameof(DetailToDoItemViewModel.AddCommand));
            SubmitButton.SetBinding(Button.CommandParameterProperty,nameof(DetailToDoItemViewModel.SelectedItem));
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(GlobalMessagingLocation.ClearDetailTodoView, GlobalMessagingLocation.ClearDetailTodoView);
            base.OnAppearing();
        }
    }
}