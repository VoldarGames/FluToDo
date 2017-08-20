using FluToDo.Commands;
using FluToDo.Views;
using Xamarin.Forms;

namespace FluToDo.ToolbarItems
{
    public class AddToolbarItem : ToolbarItem
    {
        public AddToolbarItem(Page destinationPage)
        {
            Icon = new FileImageSource() {File = GlobalDrawableLocation.AddIcon };
            Text = GlobalLocation.AddText;
            Command = new NavigationCommand(destinationPage);
        }
    }
}