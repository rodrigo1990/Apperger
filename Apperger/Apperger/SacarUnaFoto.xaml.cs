using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Common.Contract;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Apperger.Data;

namespace Apperger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SacarUnaFoto : ContentPage
    {
        static Stream streamCopy;
        double felicidad;
        double enojo;
        double asco;
        double miedo;
        double neutral;
        double tristeza;
        double sorpresa;
        double desprecio;
        string mensaje;

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

            if (foto == null)
                return;

            MiImagen.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                foto.Dispose();
                return stream;
            });
            
            string  json = await ServicioEmocion.MakeAnalysisRequest(foto.AlbumPath);
           
            List<Emociones> emociones = JsonConvert.DeserializeObject<List<Emociones>>(json);
            
                foreach (var lista in emociones)
                {
                    felicidad = lista.faceAttributes.emotion.happiness;
                    enojo = lista.faceAttributes.emotion.anger;
                    desprecio = lista.faceAttributes.emotion.contempt;
                    asco = lista.faceAttributes.emotion.disgust;
                    miedo = lista.faceAttributes.emotion.fear;
                    neutral = lista.faceAttributes.emotion.neutral;
                    tristeza = lista.faceAttributes.emotion.sadness;
                    sorpresa = lista.faceAttributes.emotion.surprise;
                }

                if (felicidad > 0.5)
                {
                    mensaje = "Muy bien, estas feliz!!";
                }

                if (enojo > 0.5)
                {
                    mensaje = "¿Porque estas enojado?";
                }

                if (desprecio > 0.5)
                {
                    mensaje = "No se te ve muy contento!!";
                }

                if (asco > 0.5)
                {
                    mensaje = "¿No te gusta algo?";
                }

                if (miedo > 0.5)
                {
                    mensaje = "¿Que te asusta?";
                }

                if (neutral > 0.5)
                {
                    mensaje = "Estas Normal";
                }

                if (tristeza > 0.5)
                {
                    mensaje = "¿Porque estas triste?";
                }

                if (sorpresa > 0.5)
                {
                    mensaje = "Te veo muy soprendido!";
                }

               
                await DisplayAlert("Msj", mensaje, "Ok");
            
                

           
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