using Xamarin.Forms;

namespace Apperger
{
    public partial class Cards : ContentPage
    {
        public Cards()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }
    }
}