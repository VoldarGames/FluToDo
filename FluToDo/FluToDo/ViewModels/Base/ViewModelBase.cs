using Xamarin.Forms;

namespace FluToDo.ViewModels.Base
{
    public abstract class ViewModelBase : BindableObject
    {
        public abstract Page GetView();
    }
}