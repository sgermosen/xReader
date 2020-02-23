using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace News.Client.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Menu
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String ViewA { get; set; }
        public ImageSource Icon { get; set; }
        public String Descripcion { get; set; }

    }
}
