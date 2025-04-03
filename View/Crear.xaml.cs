using System.Collections.ObjectModel;
using System.Threading.Tasks;
using llamada.Model;


namespace llamada;

public partial class Crear : ContentPage
{
   private FileResult fotito;
   private string source;
	public Crear()
	{
		InitializeComponent();
        
    }
   
    public bool validar_unico(string numero, ObservableCollection<ContactoModel> coleccion)
    {
        foreach (var item in coleccion)
        {
            if (item.telefono== numero)
            {
                return true;
            }
        }
        return false;
    }
    public bool validador_generico(String texto_escrito)
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
    public bool validador_numero_generico(String texto)
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
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (validador_numero_generico(tel.Text) && imagensita.Source!=null && validador_generico(nombre.Text))
        {
            if (validar_unico(tel.Text, App.contacto))
            {
                DisplayAlert("Error", "ya existe ese numero telefonico", "Salir");
            }
            else
            {
                ContactoModel contacto = new ContactoModel
                {
                    Nombre = nombre.Text,
                    imagen = source,
                    telefono = tel.Text
                };
                if (App.contacto is null)
                {
                    ObservableCollection<ContactoModel> contacts = new ObservableCollection<ContactoModel>();
                    contacts.Add(contacto);
                    App.contacto = contacts;
                }
                else
                {
                    App.contacto.Add(contacto);
                }
                App.flyoutPage.Detail.Navigation.RemovePage(this);
            }
        }
        else
        {
            DisplayAlert("Error", "Falta algun campo por rellenar o coloco algo diferente a un digito en el telefono", "Cerrar");
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
         Media_picker(imagensita);
       // fotito = await MediaPicker.Default.CapturePhotoAsync();
        //if (MediaPicker.Default.IsCaptureSupported)
        //{
        //    if (fotito != null)
        //    {
        //        //guarda la foto
        //        string ruta_local = Path.Combine(FileSystem.CacheDirectory, fotito.FileName);
        //        using Stream fuente = await fotito.OpenReadAsync();
        //        using FileStream fuente_local = File.OpenWrite(ruta_local);

        //        await fuente.CopyToAsync(fuente_local);

        //        source = fuente_local.Name;
        //        imagensita.Source = source;

        //    }
        //}
        //else
        //{
        //    await DisplayAlert("Error", "El tipo de archivo no es aceptado", "Cerrar");
        //}
    }
    public async void Media_picker(Image control_imagen)
    {

        string? respuesta = await DisplayActionSheet("Desea tomar una foto o abrir una foto desde archivos",
          "Cerrar", null, "foto", "archivo");
        if (respuesta!=null)
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