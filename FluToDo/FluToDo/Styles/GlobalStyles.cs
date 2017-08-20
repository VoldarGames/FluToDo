using Xamarin.Forms;

namespace FluToDo.Styles
{
    public static class GlobalStyles
    {
        public static Style CellLabelStyle => new Style(typeof(Label))
        {
             Setters =
             {
                 new Setter()
                 {
                     Property = View.HorizontalOptionsProperty,
                     Value = LayoutOptions.CenterAndExpand
                 },
                 new Setter()
                 {
                     Property = View.VerticalOptionsProperty,
                     Value = LayoutOptions.Center
                 },
                 new Setter()
                 {
                     Property = Label.FontSizeProperty,
                     Value = 16
                 },
                 new Setter()
                 {
                     Property = Label.TextColorProperty,
                     Value = Color.Black
                 },
             }
        };

        public static Style HeaderLabel => new Style(typeof(Label))
        {
             Setters =
             {
                 new Setter()
                 {
                     Property = View.HorizontalOptionsProperty,
                     Value = LayoutOptions.StartAndExpand
                 },
                 new Setter()
                 {
                     Property = Label.FontSizeProperty,
                     Value = 18
                 },
                 new Setter()
                 {
                     Property = Label.TextColorProperty,
                     Value = Color.Black
                 },
             }
        };

        public static Style CellStatusImageStyle => new Style(typeof(Image))
        {
             Setters =
             {
                 new Setter()
                 {
                     Property = View.HorizontalOptionsProperty,
                     Value = LayoutOptions.End
                 },
                 new Setter()
                 {
                     Property = View.VerticalOptionsProperty,
                     Value = LayoutOptions.Center
                 },
                 new Setter()
                 {
                     Property = VisualElement.HeightRequestProperty,
                     Value = 32
                 },
                 new Setter()
                 {
                     Property = VisualElement.WidthRequestProperty,
                     Value = 32
                 },
             }
        };

        public static Style CellStatusBoxViewStyle => new Style(typeof(BoxView))
        {
             Setters =
             {
                 new Setter()
                 {
                     Property = View.HorizontalOptionsProperty,
                     Value = LayoutOptions.End
                 },
                 new Setter()
                 {
                     Property = View.VerticalOptionsProperty,
                     Value = LayoutOptions.Center
                 },
                 new Setter()
                 {
                     Property = VisualElement.HeightRequestProperty,
                     Value = 32
                 },
                 new Setter()
                 {
                     Property = VisualElement.WidthRequestProperty,
                     Value = 32
                 },
             }
        };

        public static Style FrameStyle => new Style(typeof(Frame))
        {
            Setters =
            {
                new Setter()
                {
                    Property = View.HorizontalOptionsProperty,
                    Value = LayoutOptions.FillAndExpand
                },
                new Setter()
                {
                    Property = View.VerticalOptionsProperty,
                    Value = LayoutOptions.StartAndExpand
                },
                new Setter()
                {
                    Property = Frame.CornerRadiusProperty,
                    Value = 5
                },
                new Setter()
                {
                    Property = Frame.HasShadowProperty,
                    Value = true
                },
            }
        };

        public static Style SubmitButton => new Style(typeof(Button))
        {
            Setters =
            {
                new Setter()
                {
                    Property = View.HorizontalOptionsProperty,
                    Value = LayoutOptions.FillAndExpand
                },
                new Setter()
                {
                    Property = View.VerticalOptionsProperty,
                    Value = LayoutOptions.End
                },
                new Setter()
                {
                    Property = VisualElement.BackgroundColorProperty,
                    Value = Color.AliceBlue
                },
                new Setter()
                {
                    Property = Button.FontSizeProperty,
                    Value = 14
                },
            }
        };
    }
}
