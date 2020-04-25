using News.Client.Models;
using News.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace News.Client.ViewModels
{
	public class CountryContentPageViewModel : BaseViewModel
	{
        private readonly INavigationService _navigationService;
        private readonly IDataService _sourcesService;
        public CountryContentPageViewModel(INavigationService navigationService, IDataService sourcesService) :
        base(navigationService)
        {
          
            _sourcesService = sourcesService;
            _navigationService = navigationService;
            Title = "Country";
            countryList = new ObservableCollection<Country>();
            _OriginalCountryList = new ObservableCollection<Country>();
            LanguageList = new ObservableCollection<Language>();
            SearchButtonCommand = new DelegateCommand(ExecuteClick, CanExecuteClick);
            ExecuteCommand = new DelegateCommand(Execute);
            GetCountryFromAPI();
        }
        private ObservableCollection<Country> countryList;
        private ObservableCollection<Language> LanguageList;
        private ObservableCollection<Country> _OriginalCountryList;
        public ObservableCollection<Country> CountryList
        {
            get { return countryList; }
            set { SetProperty(ref countryList, value); }
        }


        private string _searchBarValue; // = "Seach Beers";
        private Item _selectedItem;

        public string SearchBarValue
        {
            get { return _searchBarValue; }
            set
            {
                SetProperty(ref _searchBarValue, value);
                CountryList = new ObservableCollection<Country>(from c in _OriginalCountryList
                                                               where string.IsNullOrEmpty(_searchBarValue) ||
                                                               c.Name.IndexOf(_searchBarValue, StringComparison.OrdinalIgnoreCase) > -1
                                                               select c);
            }
        }


      
        async void GetCountryFromAPI()
        {
            try
            {

                IsBusy = true;
                IsVisibleWaitIndicator = true;
                LabelInformation = "Loading Dada...";
                IsVisibleWaitAbsoluteLayout = true;

                var result = await _sourcesService.GetCountriesAsync();
                IsBusy = false;
                _OriginalCountryList = new ObservableCollection<Country>(result);
                CountryList = _OriginalCountryList;

             

                IsBusy = false;
                if (_OriginalCountryList.Count > 0)
                {
                    IsVisibleWaitAbsoluteLayout = false;
                    LabelInformation = "";
                }
                else
                {
                    IsVisibleWaitAbsoluteLayout = true;
                    IsVisibleWaitIndicator = false;
                    LabelInformation = "There is no data to show";
                }
            }
            catch (Exception)
            {
                IsVisibleWaitAbsoluteLayout = true;
                IsBusy = false;
                IsVisibleWaitIndicator = false;
                LabelInformation = "Error getting the data";
            }
           ((DelegateCommand)SearchButtonCommand).RaiseCanExecuteChanged();

        }

        public ICommand SearchButtonCommand
        {
            get;
        }

        public ICommand ExecuteCommand
        {
            get;
        }


        private bool _isVisibleSearchBar = false;

        public bool IsVisibleSearchBar
        {
            get { return _isVisibleSearchBar; }
            set
            {

                SetProperty(ref _isVisibleSearchBar, value);
                if (!value)
                {
                    SearchBarValue = "";

                    //BeersList = new ObservableCollection<Beers>(from beer in _OriginalbeersList
                    //                                            select beer);
                }

            }
        }

        private void ExecuteClick()
        {
            IsVisibleSearchBar = !IsVisibleSearchBar;

        }

        private bool CanExecuteClick()
        {
            return IsNotRunning;
        }

        private Country _itemSelected;
        public Country ItemSelected
        {
            get => _itemSelected;
            set => SetProperty(ref _itemSelected, value);
        }


        async void Execute()
        {


            var navigationParams = new NavigationParameters
            {
                { "model", _itemSelected }
            };

            StaticObject.MasterSelected = eMasterSelected.Country;
            await _navigationService.NavigateAsync("SourcesContentPage", navigationParams);
        }

    }
}
