using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataBinding2.Data;

namespace Apperger
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EscogerEmocion : ContentPage
	{
		public EscogerEmocion ()
		{
            ListaImagenes lista = new ListaImagenes();

			InitializeComponent ();

            Content = lista.GetListView();
		}
	}
}