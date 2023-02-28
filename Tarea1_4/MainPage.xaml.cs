using System;
using Tarea1_4.Modelo;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Tarea1_4
{
    public partial class MainPage : ContentPage
    {
        MediaFile foto = null;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnVerLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vista.Lista());
        }

        private async void txtGuardarFoto_Clicked(object sender, EventArgs e)
        {
            if (foto != null)
            {
                if (!validar(txtDescripcion) && !validar(txtNombre))
                {
                    var persona = new Persona
                    {
                        descripcion = txtDescripcion.Text.Trim(),
                        nombre = txtNombre.Text.Trim(),
                        foto = foto.Path
                    };

                    int result = await App.Database.guardarPersona(persona);
                    if (result > 0)
                    {
                        mensaje("Ingreso exitoso", "Los datos se guardaron exitosamente");
                        limpiarCampos();
                    }
                    else mensaje("Error al guardar", "Al parecer los datos no se han guardado");
                }
                else mensaje("Error", "Debe llenar todos los campos solicitados.");
            }
            else mensaje("Error", "Debe tomar una foto");
        }

        public void limpiarCampos()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
        }

        private async void txtTomarFoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    mensaje("Error", "No se han otorgado los permisos para suar la camara o la camara no esta disponible.");
                    return;
                }

                foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                if (foto != null)
                {
                    txtImagen.Source = ImageSource.FromStream(() =>
                    {
                        return foto.GetStream();
                    });
                }
                else return;
            }
            catch (Exception ex)
            {
                mensaje("Error", "Error: " + ex.Message.ToString());
            }
        }

        public bool validar(Entry campo)
        {
            return String.IsNullOrEmpty(campo.Text);
        }

        public async void mensaje(String tittulo, String mensaje)
        {
            await DisplayAlert(tittulo, mensaje, "Ok");
        }
    }
}
 
