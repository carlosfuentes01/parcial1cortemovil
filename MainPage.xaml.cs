

namespace llamada
{
    public partial class MainPage : FlyoutPage
    {

       
        public MainPage()
        {
            InitializeComponent();
            Flyout = new Maestro();
            NavigationPage nav = new(new Detalle())
            {
                BarBackgroundColor = Color.Parse("#9C27B3"),
                BarTextColor = Color.Parse("#ffffff")
            };

            Detail = nav;


            App.flyoutPage = this;
        }


    }

}
