using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinDO.News.Models;
using XamarinDO.News.Rss;
using Xamarin.Forms;

namespace XamarinDO.News.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
	    //Make sure that your datasouse have the same estructure of the "file.xml" that is submited with the project
        //you could replase this with RRS datasource
        private const string FeedUri = "http://praysoft.net/news/file.xml"; 

		private bool _isActivityIndicatorVisible;

		public bool IsActivityIndicatorVisible
		{
			get { return _isActivityIndicatorVisible; }
			set { Set(ref _isActivityIndicatorVisible, value); }
		}

		private ObservableCollection<RssFeedItem> _feed;

		public ObservableCollection<RssFeedItem> Feed
		{
			get { return _feed; }
			set { Set(ref _feed, value); }
		}

		private RssFeedItem _selectedItem;

		public RssFeedItem SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				OnItemSelected(value);
				Set(ref _selectedItem, value);
			}
		}

		private void OnItemSelected(RssFeedItem item)
		{
			if(item != null)
			{
				Device.OpenUri(item.Link);
			}
		}

		public async void Update()
		{
			IsActivityIndicatorVisible = true;
			Feed = new ObservableCollection<RssFeedItem>(await RssClient.Load(new Uri(FeedUri)));
			IsActivityIndicatorVisible = false;
		}
	}
}
