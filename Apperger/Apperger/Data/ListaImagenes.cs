using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DataBinding2.Data
{
    public class ListaImagenes
    {
        ListView listView = new ListView();
        public ListaImagenes()
        {
            var strings = new[]
            {
                "Rodrigo",
                "Tincho",
                "Alan"
            };


            this.listView.ItemsSource = strings;
        }

        public  ListView  GetListView()
        {
            return this.listView;
        }

    }
}
