using Android.Content;
using Android.Widget;
using Core.Interfaces;
using Xamarin.Forms;

namespace FluToDo.Droid
{
    public class OperatingSystemMethods : IOperatingSystemMethods
    {
        public static Context CurrentContext;
        public void ShowToast(string text)
        {
            Device.BeginInvokeOnMainThread(() => Toast.MakeText(CurrentContext, text, ToastLength.Long).Show());
        }
    }
}