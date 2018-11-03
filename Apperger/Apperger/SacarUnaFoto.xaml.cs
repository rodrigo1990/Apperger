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
using Apperger.Class;

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
                /*streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                foto.Dispose();*/
                return stream;
            });

            Cargando.IsVisible = true;
            lblResult.Text = "Analizando la imagen...";

            string  json = await ServicioEmocion.MakeAnalysisRequest(foto.AlbumPath);
            if (json != null)
            {
                Cargando.IsVisible = false;
                lblResult.Text = "";

                List<Emociones> emociones = JsonConvert.DeserializeObject<List<Emociones>>(json);
                analisisEmociones(emociones);
                /*Emo se = new Emo();

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
                    se.idEmocionReal = 1;
                    mensaje = "Muy bien, estas feliz!!";
                   
                }

                if (enojo > 0.5)
                {
                    mensaje = "¿Porque estas enojado?";
                    se.idEmocionReal = 2;
                }

                if (desprecio > 0.5)
                {
                    mensaje = "No se te ve muy contento!!";
                    se.idEmocionReal = 3;
                }

                if (asco > 0.5)
                {
                    mensaje = "¿No te gusta algo?";
                    se.idEmocionReal = 4;
                }

                if (miedo > 0.5)
                {
                    mensaje = "¿Que te asusta?";
                    se.idEmocionReal = 5;
                }

                if (neutral > 0.5)
                {
                    mensaje = "Estas Normal";
                    se.idEmocionReal = 6;
                }

                if (tristeza > 0.5)
                {
                    mensaje = "¿Porque estas triste?";
                    se.idEmocionReal = 7;
                }

                if (sorpresa > 0.5)
                {
                    mensaje = "Te veo muy soprendido!";
                    se.idEmocionReal = 9;
                }

                

                /*HttpClient client = new HttpClient();
                string url = "https://apiapperger.azurewebsites.net/api/Emociones";
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                var json2 = JsonConvert.SerializeObject(se);
                var content = new StringContent(json2, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await DisplayAlert("Msj", mensaje, "Ok");
                }
                else
                {
                    await DisplayAlert("Msj", "No se pudo insertar emocion", "Ok");
                }*/



            }


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

                Cargando.IsVisible = true;
                lblResult.Text = "Analizando la imagen...";
                string json = await ServicioEmocion.MakeAnalysisRequest(imagen.Path);
                if(json != null)
                {
                    Cargando.IsVisible = false;
                    lblResult.Text = "";
                    List<Emociones> emociones = JsonConvert.DeserializeObject<List<Emociones>>(json);

                    analisisEmociones(emociones);
                    /*Emo se = new Emo();

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
                        se.idEmocionReal = 1;
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
                    }*/

                    /*HttpClient client = new HttpClient();
                    string url = "https://apiapperger.azurewebsites.net/api/Emociones?idEmocion="+se.idEmocionReal;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response;
                    var json2 = JsonConvert.SerializeObject(se);
                    var content = new StringContent(json2, Encoding.UTF8, "application/json");
                    response = await client.GetAsync(uri);
                    if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        await DisplayAlert("Msj", mensaje, "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Msj", "No se pudo insertar emocion", "Ok");
                    }*/
                    //await DisplayAlert("Msj", mensaje, "Ok");


                }

            }

            


        }


        public async void analisisEmociones(List<Emociones> emocion)
        {
            Emo se = new Emo();

            foreach (var lista in emocion)
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
                se.idEmocionReal = 1;
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

            HttpClient client = new HttpClient();
            string url = "https://apiapperger.azurewebsites.net/api/Emociones?idEmocion=" + se.idEmocionReal;
            var uri = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var json2 = JsonConvert.SerializeObject(se);
            var content = new StringContent(json2, Encoding.UTF8, "application/json");
            response = await client.GetAsync(uri);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                await DisplayAlert("Msj", mensaje, "Ok");
            }
            else
            {
                await DisplayAlert("Msj", "No se pudo insertar emocion", "Ok");
            }
        }


    }
}