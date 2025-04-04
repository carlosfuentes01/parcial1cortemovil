using System.Linq;
using Android.App;
using llamada.Model;
using llamada.Platforms.Android;
using llamada.Servicios;
namespace llamada;

public partial class Plantilla : ContentPage
{
    PhoneCaller servicios_llamada;
	public ContactoModel contactos { get; set; }
	public Plantilla(PhoneCaller servicios_llamada_constructor)
	{
		InitializeComponent();
	
        servicios_llamada = servicios_llamada_constructor;
		this.BindingContext = this;
	}
    public Plantilla(ContactoModel contacto_constructor)
    {
        InitializeComponent();
        contactos = contacto_constructor;
        this.BindingContext = this;
       
    }

    public Plantilla()
    {
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        App.flyoutPage.Detail.Navigation.PushAsync(new View.Editar(contactos));
        
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        bool answer =  await DisplayAlert("¡ALERTA!", "¿Seguro que quiere elimitar este contacto?", "Si", "No");

        if (answer)
        {
            ContactoModel? contacto_en_observable_coleccion = App.contacto.FirstOrDefault( x=> x == this.contactos);
            // el firstOrdefault si le colocas parametros puede servir como una consulta que regresa un solo valor

            int indice_escogido = App.contacto.IndexOf(contacto_en_observable_coleccion);
            App.contacto.RemoveAt(indice_escogido);

            App.flyoutPage.Detail.Navigation.RemovePage(this);
        }




    }

    private void Button_Clicked_llamada(object sender, EventArgs e)
    {
#if ANDROID
       // DependencyService.Get<PhoneCaller>().llamar(contactos.telefono);
        servicios_llamada.llamar("111111111");
        var a = new Servicios_android();
 a.llamar("11111");
#endif
    }
}