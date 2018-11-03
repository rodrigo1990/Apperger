using Apperger.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Apperger.Dao;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Apperger
{
    public partial class MainPage : ContentPage
    {
        public Empleado emp { get; set; }
        private static readonly UsuarioDao usuarioDao = new UsuarioDao();

        public MainPage()
        {

            emp = new Empleado();
            emp.nombre = "Rodrigo reynoso";
            emp.email = "mcd77.1990@gmail.com";
            BindingContext = emp;
            InitializeComponent();


            btnIrAOtraPagina.Clicked += (sender, e) =>
            {


                if (email.Text == null)
                    email.Text = "";
                else if (password.Text == null)
                    password.Text = "";


                if (email.Text != "" && password.Text != "")
                {

                    Btnsign_Click(new Usuario(email.Text, password.Text));

                }
                else
                {
                    DisplayAlert("Msj", "¡Ingrese sus datos!", "Ok");
                }

            };

        }

        private async void Btnsign_Click(Usuario usuario)
        {

            HttpClient client = new HttpClient();
            string url = "https://apiapperger.azurewebsites.net/api/Login?username="+usuario.email+"&password="+usuario.password;
            var uri = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application / json"));
            HttpResponseMessage response;

            response = await client.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                //lblResult.Text = "Usuario Correcto";
                await Navigation.PushAsync(new Home());
            }
            else
            {
                await DisplayAlert("Msj", "No estas regitrado en la aplicacion", "Ok");
            }
        }
    }
}
