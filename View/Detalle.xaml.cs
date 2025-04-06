using System.Collections.ObjectModel;
using llamada.Model;
using llamada.View;
using Microsoft.VisualBasic;
using Syncfusion.Maui.Toolkit.BottomSheet;
using The49.Maui.BottomSheet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Android.Graphics.ImageDecoder;
using FileSystem = Microsoft.Maui.Storage.FileSystem;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Graphics.Text;




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
           App.navegacion.Navigation.PushAsync(new Plantilla(contacto_seleccionado));
           coleccion.SelectedItem = null; 
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("titulo", "me presionastes ", "cerrar");







    }
    private void OpenBottomSheet(object sender, EventArgs e)
    {
      
        
        bottomsheet.Show();
    }
    private void CloseBottomSheet(object sender, EventArgs e)
    {
        bottomsheet.Close();
    }




    /////// crear
    private FileResult fotito;
    private string source;


    public bool validar_unico(string numero, ObservableCollection<ContactoModel> coleccion)
    {
        foreach (var item in coleccion)
        {
            if (item.telefono == numero)
            {
                return true;
            }
        }
        return false;
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
    private void crear(object sender, EventArgs e)
    {
        if (validador_numero_generico(tel.Text) && imagensita.Source != null && validador_generico(nombre.Text))
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
                bottomsheet.Close();
                tel.Text = "";
                imagensita.Source = null;
                source = "";
                nombre.Text = "";
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;



  

                var toast = Toast.Make("New contact added.", ToastDuration.Long, fontSize);

                toast.Show(cancellationTokenSource.Token);
                

            }
        }
        else
        {
            DisplayAlert("Error", "Falta algun campo por rellenar o coloco algo diferente a un digito en el telefono", "Cerrar");
        }
    }


    private void cerrado(Microsoft.Maui.Controls.Entry entrada)
    {
        if (entrada.IsSoftKeyboardShowing())
        {
            entrada.HideKeyboardAsync(CancellationToken.None);
        }
    }
    

    private async void escogerfoto(object sender, EventArgs e)
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

    private void cerrar_teclado(object sender, EventArgs e)
    {
      var entrada= (Microsoft.Maui.Controls.Entry)sender;
        entrada.HideKeyboardAsync(CancellationToken.None);
    }
}