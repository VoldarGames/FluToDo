using Core.Helpers;
using FluToDo.ViewModels;
using FluToDo.Views;
using Xamarin.Forms;

namespace FluToDo
{
    public partial class FluToDoApp : Application
    {
        public FluToDoApp()
        {
            InitializeComponent();
            var navigationPage = new NavigationPage(new MainListViewModel().GetView());
            NavigationService.CurrentNavigation = navigationPage.Navigation;
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
