using System;
using System.Globalization;
using Domain;
using FluToDo.Behaviors;
using FluToDo.Commands;
using FluToDo.ToolbarItems;
using FluToDo.ViewModels;
using Xamarin.Forms;

namespace FluToDo.Views
{
    public class MainListView : ContentPage
    {
        public ListView ToDoItemsListView { get; set; } = new ListView(ListViewCachingStrategy.RecycleElement);

        public MainListView()
        {
            BuildUserInterface();
            WireEvents();
            SetBindings();
            AddBehaviors();
        }

        private void WireEvents()
        {
            BindingContextChanged += (sender, args) =>
            {
                ConfigureListView();
            };
        }

        private void AddBehaviors()
        {
            var listViewItemTappedBehavior = new ListViewItemTappedBehavior();
            listViewItemTappedBehavior.SetBinding(ListViewItemTappedBehavior.CommandProperty, nameof(MainListViewModel.ItemTappedCommand));
            ToDoItemsListView.Behaviors.Add(listViewItemTappedBehavior);
        }

        private void SetBindings()
        {
            ToDoItemsListView.SetBinding(ListView.ItemsSourceProperty,nameof(MainListViewModel.TodoItemList));
            ToDoItemsListView.SetBinding(ListView.RefreshCommandProperty, nameof(MainListViewModel.RefreshCommand));
            ToDoItemsListView.SetBinding(ListView.IsRefreshingProperty,nameof(MainListViewModel.IsBusy));
        }

        private void BuildUserInterface()
        {
            ToolbarItems.Add(new AddToolbarItem(new DetailToDoItemViewModel().GetView()));

            Content = new StackLayout()
            {
                Children =
                {
                    ToDoItemsListView
                }
            };
        }

        private void ConfigureListView()
        {
            ToDoItemsListView.IsPullToRefreshEnabled = true;
            

            ToDoItemsListView.ItemTemplate = new DataTemplate(() =>
            {
                var toDoLabel = new Label()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14,
                    TextColor = Color.Black
                };
                toDoLabel.SetBinding(Label.TextProperty,nameof(TodoItem.Name));
                var imageCompleted = new Image()
                {
                    Source = GlobalDrawableLocation.CompletedIcon,
                    HorizontalOptions = LayoutOptions.End
                };
                imageCompleted.SetBinding(IsVisibleProperty,nameof(TodoItem.IsComplete));
                var boxView = new BoxView()
                {
                   HorizontalOptions = LayoutOptions.End,
                   Color = BackgroundColor,
                   WidthRequest = 32,
                   HeightRequest = 32
                };
                boxView.SetBinding(IsVisibleProperty, nameof(TodoItem.IsComplete),BindingMode.Default,new InverseBoolConverter());
                var deleteMenuItem = new MenuItem()
                {
                    Icon = new FileImageSource() { File = GlobalDrawableLocation.DeleteIcon }
                };
                deleteMenuItem.Command = new DeleteCommand<TodoItem>()
                {
                    FinallyAction = item => { MessagingCenter.Send(GlobalMessagingLocation.RefreshToDoList,GlobalMessagingLocation.RefreshToDoList);}
                };
                deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, ".");
                var cell = new ViewCell
                {
                    View = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            toDoLabel,
                            imageCompleted,
                            boxView
                        }
                    }
                };
                cell.ContextActions.Add(deleteMenuItem);
                return cell;
            });
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(GlobalMessagingLocation.RefreshToDoList, GlobalMessagingLocation.RefreshToDoList);
            base.OnAppearing();
        }
    }

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
