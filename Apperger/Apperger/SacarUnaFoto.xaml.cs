using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Apperger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SacarUnaFoto : ContentPage
    {
        public SacarUnaFoto()
        {
            InitializeComponent();
        }

        private async void HacerFoto(object sender, EventArgs e)
        {

            var opciones_almacenamiento = new StoreCameraMediaOptions()
            {

                SaveToAlbum = true,
                Directory = "Sample",
                Name = "Mi foto"
            };

            var foto = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);


        }//hacerfoto

        private async void ElegirFoto(Object sender, EventArgs e)
        {

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var imagen = await CrossMedia.Current.PickPhotoAsync();
                if (imagen != null)
                {
                    MiImagen.Source = ImageSource.FromStream(() =>
                    {
                        var stream = imagen.GetStream();

                        return stream;
                    });
                }
            }
        }


    }
}