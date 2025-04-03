using System.Collections.ObjectModel;
using llamada.Model;


namespace llamada
{
    public partial class App : Application
    {
private FileResult? fotito;
        public App()
        {
            InitializeComponent();
            contacto = new ObservableCollection<ContactoModel>();
            this.BindingContext = this;
        }
        public static void Gotopage(ContentPage page)
        {
            flyoutPage.Detail.Navigation.PushAsync(page);
            flyoutPage.IsPresented = false;
        }
        public static ObservableCollection<ContactoModel> contacto { get; set; }
        public static FlyoutPage flyoutPage { get; set; }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }
}