using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinDO.News.ViewModels;
using Xamarin.Forms;

namespace XamarinDO.News.Views
{
	public partial class MainView : ContentPage
	{
		public MainView()
		{
			InitializeComponent();

			BindingContext = new MainViewModel();
			((MainViewModel) BindingContext).Update();
		}
	}
}
