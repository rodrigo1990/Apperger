using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Apperger
{
    public partial class MainPage : ContentPage
    {
        public Empleado emp { get; set; }
        public MainPage()
        {

            emp = new Empleado();
            emp.nombre = "Rodrigo reynoso";
            emp.email = "mcd77.1990@gmail.com";
            BindingContext = emp;
            InitializeComponent();


            btnIrAOtraPagina.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Home());
            };

        }
    }
}
