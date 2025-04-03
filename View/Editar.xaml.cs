using System.Collections.ObjectModel;
using llamada.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace llamada.View;

public partial class Editar : ContentPage
{
    public ContactoModel contacto { get; set; }
    public Editar(ContactoModel contacto_constructor)
    {

        this.contacto = contacto_constructor;
        InitializeComponent();

        nombre.Text = this.contacto.Nombre;
        imagensita.Source = this.contacto.imagen;
        tel.Text = this.contacto.telefono.ToString();

        this.BindingContext = this;
    }
    string source;
    private FileResult? fotito;

    public bool validar_unico(string numero, ObservableCollection<ContactoModel> coleccion, ContactoModel contacto_base)
    {
        bool retorno = false;
        foreach (var item in coleccion)
        {
            if (item.telefono == numero && item.telefono != contacto_base.telefono)
            {
                retorno = true;
                break;
            }
        }
        return retorno;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {

        if (validador_numero_generico(tel.Text) && imagensita.Source!=null && validador_generico(nombre.Text))
        {

            ContactoModel nuevo_contacto = new ContactoModel
                  {
                       Nombre = nombre.Text,
                       imagen = source,
                       telefono = tel.Text
            };
            if (validar_unico(tel.Text, App.contacto, nuevo_contacto))
            {


                    DisplayAlert("Error", "ya existe ese numero telefonico", "Salir");
                    
                
            }
            else
            {
                ContactoModel? contacto_en_observable_coleccion = App.contacto.FirstOrDefault(x => x.telefono == this.contacto.telefono);
                // el firstOrdefault si le colocas parametros puede servirr como una consulta que regresa un solo valor

                int indice_escogido = App.contacto.IndexOf(contacto_en_observable_coleccion);
                //no se uso un for normal porque puede que se halla eliminado un indice y pues eso daria error

                App.contacto[indice_escogido] = nuevo_contacto;
                int tamaño = App.flyoutPage.Detail.Navigation.NavigationStack.Count;
                App.flyoutPage.Detail.Navigation.PopAsync();
                App.flyoutPage.Detail.Navigation.PopAsync();
             //   App.flyoutPage.Detail.Navigation.PushAsync(new Plantilla(nuevo_contacto));
            }

        }
        else
        {
            DisplayAlert("Error", "No se llenaron los datos o, se coloco una letra en el numero telefonico", "Cerrar");
        }
    }
    public bool validador_generico(string texto_escrito)
    {
        if (texto_escrito is not null)
        {

            if (texto_escrito.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }
    public bool validador_numero_generico(string texto)
    {
        if (texto is not null)
        {
            try

            {
                long num = long.Parse(texto);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        Media_picker(imagensita);
    }

    public async void Media_picker(Image control_imagen)
    {
        string? respuesta = await DisplayActionSheet("Desea tomar una foto o abrir una foto desde archivos",
          "Cerrar", null, "foto", "archivo");
        if (respuesta != null)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {


                //TOMAR FOTO
                if (respuesta.Equals("foto"))
                {
                    fotito = await MediaPicker.Default.CapturePhotoAsync();
                }
                else
                {
                    if (respuesta.Equals("archivo"))
                    {
                        fotito = await MediaPicker.Default.PickPhotoAsync();
                    }

                }


                if (fotito != null)
                {
                    //guarda la foto
                    string ruta_local = Path.Combine(FileSystem.CacheDirectory, fotito.FileName);
                    using Stream fuente = await fotito.OpenReadAsync();
                    using FileStream fuente_local = File.OpenWrite(ruta_local);

                    await fuente.CopyToAsync(fuente_local);

                    source = fuente_local.Name;
                    control_imagen.Source = source;

                }




            }
            else
            {
                await DisplayAlert("Error", "El tipo de archivo no es aceptado", "Cerrar");
            }

        }
    }
}