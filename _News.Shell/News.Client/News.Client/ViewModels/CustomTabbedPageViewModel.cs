using News.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace News.Client.ViewModels
{
	public class CustomTabbedPageViewModel : BaseViewModel
	{
        private readonly INavigationService _navigationService;
        public CustomTabbedPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Articles";
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
           
           

        }

        private Source _source;
        public Source Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private Country _country;
        public Country Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private SourceRealm _sourceRealm;
        public SourceRealm SourceRealm
        {
            get { return _sourceRealm; }
            set { SetProperty(ref _sourceRealm, value); }
        }

        private Recent _recentSource;
        public Recent RecentSource
        {
            get { return _recentSource; }
            set { SetProperty(ref _recentSource, value); }
        }
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            switch (StaticObject.MasterSelected)
            {
                case eMasterSelected.Category:               
                    Category = (Category)parameters["model"];
                    Title = Category.Name.ToString();
                    break;
                case eMasterSelected.Source:
                    Source = (Source)parameters["model"];
                    Title = Source.Name;
                    break;

                case eMasterSelected.Country:
                    Country = (Country)parameters["model"];
                    Title = Source.Name;      
                    break;

                case eMasterSelected.Favorite:
                    SourceRealm = (SourceRealm)parameters["model"];
                    Title = SourceRealm.Name; 
                    break;
                case eMasterSelected.Recent:
                    RecentSource = (Recent)parameters["model"];
                    Title = RecentSource.Name;
                    break;
            }
        }

    }
}
