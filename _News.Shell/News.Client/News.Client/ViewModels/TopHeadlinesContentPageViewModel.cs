using News.Client.Models;
using News.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace News.Client.ViewModels
{
    public class TopHeadlinesContentPageViewModel : BaseViewModel
    {


        private readonly IDataService _sourcesService;
        private readonly INavigationService _navigationService;
        public TopHeadlinesContentPageViewModel(INavigationService navigationService, IDataService sourcesService) :
        base(navigationService)
        {

            _navigationService = navigationService;
            Title = "Top Headlines";
            //IsBusy = true;
            _sourcesService = sourcesService;


            TopHeadLinesList = new ObservableCollection<TopHeadlines>();
            _OriginalLocalTopHeadLinesList = new ObservableCollection<TopHeadlines>();
            SearchButtonCommand = new DelegateCommand(ExecuteClick, CanExecuteClick);
            ExecuteCommand = new DelegateCommand(Execute);
   
            //NavigateCommand = new DelegateCommand(Navigate);





        }



        private ObservableCollection<TopHeadlines> _topHeadLinesList;
        private ObservableCollection<TopHeadlines> _OriginalLocalTopHeadLinesList;
        public ObservableCollection<TopHeadlines> TopHeadLinesList
        {
            get { return _topHeadLinesList; }
            set { SetProperty(ref _topHeadLinesList, value); }
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

        private string _searchBarValue;
        //private Item _selectedItem;

        public string SearchBarValue
        {
            get { return _searchBarValue; }
            set
            {
                SetProperty(ref _searchBarValue, value);
                TopHeadLinesList = new ObservableCollection<TopHeadlines>(from Data in _OriginalLocalTopHeadLinesList
                                                                          where string.IsNullOrEmpty(_searchBarValue) ||
                                                                     Data.Title.IndexOf(_searchBarValue, StringComparison.OrdinalIgnoreCase) > -1
                                                                          select Data);
            }
        }

        async void GetTopHeadLinesFromAPI(string SourceID, int pageSize)
        {
            try
            {
                IsBusy = true;
                IsVisibleWaitIndicator = true;
                LabelInformation = "Loading Dada...";
                IsVisibleWaitAbsoluteLayout = true;

                var result = await _sourcesService.GetTopHeadlinesAsync(SourceID, pageSize);
                _OriginalLocalTopHeadLinesList = new ObservableCollection<TopHeadlines>(result);
                TopHeadLinesList = _OriginalLocalTopHeadLinesList;
                IsBusy = false;

                if (_OriginalLocalTopHeadLinesList.Count > 0)
                {
                    Title = $"{Title} {_OriginalLocalTopHeadLinesList.Count }";
                    IsVisibleWaitAbsoluteLayout = false;
                    LabelInformation = "";
                }
                else
                {
                    Title = "Top Headlines";
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





        private void ExecuteClick()
        {
            IsVisibleSearchBar = !IsVisibleSearchBar;
        }
        private bool CanExecuteClick()
        {
            return IsNotRunning;
        }
        private TopHeadlines _selectedItem;
        public TopHeadlines SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        async void Execute()
        {
            var navigationParams = new NavigationParameters
            {
                { "model", _selectedItem }
            };
            navigationParams.Add("PageName", _selectedItem.Title);
            navigationParams.Add("UrlPage", _selectedItem.Url);
            await _navigationService.NavigateAsync("CustomWebViewContentPage", navigationParams);
        }
        private Source _source;
        public Source Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }


        private SourceRealm _sourceRealm;
        public SourceRealm SourceRealm
        {
            get { return _sourceRealm; }
            set { SetProperty(ref _sourceRealm, value); }
        }

        private Recent _recentRealm;
        public Recent RecentRealm
        {
            get { return _recentRealm; }
            set { SetProperty(ref _recentRealm, value); }
        }


        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            switch (StaticObject.MasterSelected)
            {
                case eMasterSelected.Favorite:
                    SourceRealm = (SourceRealm)parameters["model"];
                    SourceIdToRefresh = SourceRealm.Id;
                    GetTopHeadLinesFromAPI(SourceRealm.Id, 30);
                    break;

                case eMasterSelected.Source:
                    Source = (Source)parameters["model"];
                    SourceIdToRefresh = Source.Id;
                    GetTopHeadLinesFromAPI(Source.Id, 30);
                    break;

                case eMasterSelected.Recent:
                    RecentRealm = (Recent)parameters["model"];
                    SourceIdToRefresh = RecentRealm.Id;
                    GetTopHeadLinesFromAPI(RecentRealm.Id, 30);
                    break;
                default:
                    break;
            }



        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {

                SetProperty(ref _isRefreshing, value);
            }

        }

        private string _sourceIdToRefresh;
        public  string SourceIdToRefresh
        { get { return _sourceIdToRefresh; }
            set { SetProperty(ref _sourceIdToRefresh, value); }
        }

        async Task RefreshData()
        {
            if(Source != null)
            {
                var result = await _sourcesService.GetTopHeadlinesAsync(SourceIdToRefresh, 100);
                _OriginalLocalTopHeadLinesList = new ObservableCollection<TopHeadlines>(result);
                TopHeadLinesList = _OriginalLocalTopHeadLinesList;
            }
       
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await RefreshData();

                    IsRefreshing = false;
                });
            }
        }
    }

}

