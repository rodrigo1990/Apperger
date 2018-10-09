using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Apperger
{
	public partial class Home : ContentPage
	{
		public Home()
		{
			InitializeComponent();


            btnEscogerEmocion.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new EscogerEmocion());
            };

            btnSacarFoto.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new SacarUnaFoto());

            };
        }
         
       


    }
}