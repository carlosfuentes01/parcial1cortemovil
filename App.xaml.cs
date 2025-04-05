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
            navegacion.Navigation.PushAsync(page);
    
        }
        public static ObservableCollection<ContactoModel> contacto { get; set; }
        public static NavigationPage navegacion { get; set; }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}