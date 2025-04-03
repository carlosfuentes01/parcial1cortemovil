using System.Collections.ObjectModel;
using llamada.Model;
using Microsoft.VisualBasic;
using static Android.Graphics.ImageDecoder;

namespace llamada;
public partial class Detalle : ContentPage
{

    public Detalle()
	{
        ContactoModel contacto_base = new ContactoModel
        {
            Nombre = "carlos",
            imagen = null,
            telefono = "+57 1231111"
        };

        InitializeComponent();
        contacto = App.contacto;
        contacto.Add(contacto_base);
        this.BindingContext = this;
    }

    public  ObservableCollection<ContactoModel> contacto { get; set; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ContactoModel? contacto_seleccionado = (ContactoModel?)e.CurrentSelection.FirstOrDefault();
         
        if (contacto_seleccionado!= null)
        {
           App.flyoutPage.Detail.Navigation.PushAsync(new Plantilla(contacto_seleccionado));
            coleccion.SelectedItem = null; 
        }
    }
}