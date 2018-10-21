using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Apperger.Data;
using Apperger.Dao;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            var uri = new Uri(string.Format("https://apiapperger.azurewebsites.net/api/Login?username="+usuario.email+"&password="+usuario.password));
            HttpResponseMessage response; ;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await client.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                /*var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
                {
                '"'
                });
                Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();*/

                await Navigation.PushAsync(new Home());
            }
            else
            {
                /* var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
                 {
                 '"'
                 });
                 Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();*/

                await DisplayAlert("Msj", "No estas regitrado en la aplicacion", "Ok");
            }
        }
    }
}
