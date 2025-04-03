

namespace llamada
{
    public partial class MainPage : FlyoutPage
    {

       
        public MainPage()
        {
            InitializeComponent();
            Flyout = new Maestro();
            Detail = new NavigationPage(new Detalle());

            App.flyoutPage = this;
        }


    }

}
