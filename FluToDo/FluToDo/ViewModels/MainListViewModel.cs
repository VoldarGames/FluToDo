using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.Helpers;
using Core.Interfaces;
using Domain;
using FluToDo.Commands;
using FluToDo.Interfaces;
using FluToDo.ViewModels.Base;
using FluToDo.Views;
using Xamarin.Forms;

namespace FluToDo.ViewModels
{
    public class MainListViewModel : ViewModelBase, IHasRefreshList
    {
        private ObservableCollection<TodoItem> _todoItemList = new ObservableCollection<TodoItem>();
        private bool _isBusy;

        public ObservableCollection<TodoItem> TodoItemList
        {
            get { return _todoItemList; }
            set
            {
                _todoItemList = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public MainListViewModel()
        {
            RefreshCommand = new RefreshCommand(this);
            
            ItemTappedCommand = new UpdateCommand<TodoItem>()
            {
                CustomAction = item =>
                {
                    item.IsComplete = !item.IsComplete;
                },
                FinallyAction = item => {RefreshList();}
            };
            MessagingCenter.Subscribe<string>(this,GlobalMessagingLocation.RefreshToDoList,s => {RefreshList();});
        }

        public override Page GetView()
        {
            return new MainListView() {BindingContext = this};
        }

        public async void RefreshList()
        {
            var apiClient = DependencyService.Get<IApiClient>();
            var list = await apiClient.GetAsync<TodoItem>();
            if(list != null) TodoItemList = new ObservableCollection<TodoItem>(list);
            IsBusy = false;
        }
    }
}
