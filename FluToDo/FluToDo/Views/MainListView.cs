using System;
using System.Globalization;
using Domain;
using FluToDo.Behaviors;
using FluToDo.Commands;
using FluToDo.Styles;
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
            Title = GlobalLocation.FluToDoTitle;
            Icon = new FileImageSource() {File = GlobalDrawableLocation.FluToDoIcon};
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
                    Style = GlobalStyles.CellLabelStyle
                };
                toDoLabel.SetBinding(Label.TextProperty,nameof(TodoItem.Name));
                var imageCompleted = new Image()
                {
                    Source = GlobalDrawableLocation.CompletedIcon,
                    Style = GlobalStyles.CellStatusImageStyle
                };
                imageCompleted.SetBinding(IsVisibleProperty,nameof(TodoItem.IsComplete));
                var boxView = new BoxView()
                {
                   HorizontalOptions = LayoutOptions.End,
                   Color = BackgroundColor,
                   Style = GlobalStyles.CellStatusBoxViewStyle
                };
                boxView.SetBinding(IsVisibleProperty, nameof(TodoItem.IsComplete),BindingMode.Default,new InverseBoolConverter());
                var deleteMenuItem = new MenuItem
                {
                    Icon = new FileImageSource() {File = GlobalDrawableLocation.DeleteIcon},
                    Command = new DeleteCommand<TodoItem>()
                    {
                        FinallyAction = item =>
                        {
                            MessagingCenter.Send(GlobalMessagingLocation.RefreshToDoList,
                                GlobalMessagingLocation.RefreshToDoList);
                        }
                    }
                };
                deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, ".");
                var cellStackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        toDoLabel,
                        imageCompleted,
                        boxView
                    }
                };
                var cell = new ViewCell
                {
                    View = cellStackLayout
                };
                cell.ContextActions.Add(deleteMenuItem);
                MessagingCenter.Subscribe<string>(cell,GlobalMessagingLocation.CheckOnAnimation, text =>
                {
                    if (text.Equals(toDoLabel.Text))
                    {
                        cellStackLayout.Animate("AnimationOn", d =>
                        {
                            Device.BeginInvokeOnMainThread(() => cellStackLayout.BackgroundColor = Color.FromRgb(Math.Cos(d*(Math.PI / 180)),1D, Math.Cos(d* (Math.PI / 180))));
                        },-180D,180D,50U,1000U,Easing.SinIn);
                    }
                } );

                MessagingCenter.Subscribe<string>(cell,GlobalMessagingLocation.CheckOffAnimation, text =>
                {
                    if (text.Equals(toDoLabel.Text))
                    {
                        cellStackLayout.Animate("AnimationOff", d =>
                        {
                            Device.BeginInvokeOnMainThread(() => cellStackLayout.BackgroundColor = Color.White);
                        }, -180D, 180D, 50U, 750U, Easing.SinIn);
                    }
                } );
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
