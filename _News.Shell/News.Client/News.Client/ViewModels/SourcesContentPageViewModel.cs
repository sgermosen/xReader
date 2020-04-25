using News.Client.Models;
using News.Client.Services;
using Prism.Commands;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace News.Client.ViewModels
{
    public class SourcesContentPageViewModel : BaseViewModel
    {
        private readonly Realm _realm;
        private readonly INavigationService _navigationService;
        private readonly IDataService _sourcesService;
        public SourcesContentPageViewModel(INavigationService navigationService, IDataService sourcesService, Realm realm) :
        base(navigationService)
        {


            _realm = realm;
            _sourcesService = sourcesService;
            _navigationService = navigationService;
            Title = "Sources";
            SourcesList = new ObservableCollection<Source>();
            _OriginalSourcesList = new ObservableCollection<Source>();
            SearchButtonCommand = new DelegateCommand(ExecuteClick, CanExecuteClick);
            ExecuteCommand = new DelegateCommand(Execute);
            CmdExecuteActionToFavorite = new DelegateCommand<Source>(ExecuteActionToFavorite);




            //NavigateCommand = new DelegateCommand(Navigate);
        }
        public ICommand SearchButtonCommand
        {
            get;
        }

        public ICommand ExecuteCommand
        {
            get;
        }

        public ICommand CmdExecuteActionToFavorite
        {
            get;
        }



        private bool _isVisibleSearchBar = false;

        public bool IsVisibleSearchBar
        {
            get { return _isVisibleSearchBar; }
            set {

                SetProperty(ref _isVisibleSearchBar, value);
                if (!value)
                {
                    SearchBarValue = "";

                    //BeersList = new ObservableCollection<Beers>(from beer in _OriginalbeersList
                    //                                            select beer);
                }

            }
        }



        private ObservableCollection<Source> sourcesList;
        private ObservableCollection<Source> _OriginalSourcesList;
        public ObservableCollection<Source> SourcesList
        {
            get { return sourcesList; }
            set { SetProperty(ref sourcesList, value); }
        }


        private string _searchBarValue; // = "Seach Beers";

        public string SearchBarValue
        {
            get { return _searchBarValue; }
            set {
                SetProperty(ref _searchBarValue, value);
                SourcesList = new ObservableCollection<Source>(from source in _OriginalSourcesList
                                                               where string.IsNullOrEmpty(_searchBarValue) ||
                                                               source.Name.IndexOf(_searchBarValue, StringComparison.OrdinalIgnoreCase) > -1
                                                               select source);
            }
        }

        async Task GetSourcesFromAPI()
        {
            try
            {
                IsBusy = true;
                IsVisibleWaitIndicator = true;
                LabelInformation = "Loading Dada...";
                IsVisibleWaitAbsoluteLayout = true;
                var result = await _sourcesService.GetSourcesAsync();
                _OriginalSourcesList = new ObservableCollection<Source>(result);

                foreach (Source s in _OriginalSourcesList)
                {
                    s.IsFavorite = isFavoriteSource(s);
                    s.ImagenFavorite = s.IsFavorite ? "ic_shortcut_favorite.png" : "ic_shortcut_favorite_border.png";
                }


                IsBusy = false;

                if (_OriginalSourcesList.Count > 0)
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

        private void ExecuteClick()
        {
            IsVisibleSearchBar = !IsVisibleSearchBar;

            //if (IsVisibleSearch)
            //{

            //    //Entry myEntry = t.FindByName<Entry>("YourEntryName");
            //    //myEntry.Focus();
            //}

        }

        private bool CanExecuteClick()
        {
            return IsNotRunning;
        }

        public string Icon
        {
            get {
                return IsVisibleSearchBar ? String.Format("{0}{1}.png", Device.OnPlatform("Icons/", "", "Assets/Icons"), "if_search_22711.png") : String.Format("{0}{1}.png", Device.OnPlatform("Icons/", "", "Assets/Icons"), "if_search_22711.png");
                //"if_search_22711.png" : "if_search_22711.png";
            }
        }

        private Source _selectedSources;
        public Source SelectedSources
        {
            get => _selectedSources;
            set => SetProperty(ref _selectedSources, value);
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

        private LanguageFull _languageFull;
        public LanguageFull LanguageFull
        {
            get { return _languageFull; }
            set { SetProperty(ref _languageFull, value); }
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {



            switch (StaticObject.MasterSelected)
            {
                case eMasterSelected.Category:
                    Category = (Category)parameters["model"];
                    Title = Category.Name.ToString();
                    await GetSourcesFromAPI();

                    if (Category.Id != "all")
                        _OriginalSourcesList = new ObservableCollection<Source>(from source in _OriginalSourcesList
                                                                                where source.Category == Category.Id
                                                                                select source);
                    //SourcesList = _OriginalSourcesList;

                    break;

                case eMasterSelected.Country:
                    Country = (Country)parameters["model"];
                    Title = Country.Name.ToString();
                    await GetSourcesFromAPI();
                    _OriginalSourcesList = new ObservableCollection<Source>(from source in _OriginalSourcesList
                                                                            where source.Country == Country.Alpha2Code.ToLower()
                                                                            select source);
                    break;
                case eMasterSelected.Language:
                    LanguageFull = (LanguageFull)parameters["model"];
                    Title = LanguageFull.Name.ToString();
                    await GetSourcesFromAPI();
                    _OriginalSourcesList = new ObservableCollection<Source>(from source in _OriginalSourcesList
                                                                            where source.Language == LanguageFull.Id.ToLower()
                                                                            select source);

                    break;





                case eMasterSelected.Source:
                case eMasterSelected.Nothing:
                default:
                    Title = "All";
                    await GetSourcesFromAPI();
                    //_OriginalSourcesList
                    //SourcesList = ;

                    //GetSourcesFromAPI()
                    //SourcesList = StaticObject.OriginalGlobalSources;
                    //_OriginalSourcesList = StaticObject.OriginalGlobalSources;
                    break;
            }



            SourcesList = _OriginalSourcesList;

            if (_OriginalSourcesList.Count > 0)
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
        async void Execute()
        {


            var navigationParams = new NavigationParameters
            {
                { "model", _selectedSources }
            };

            StaticObject.MasterSelected = eMasterSelected.Source;
            await _navigationService.NavigateAsync("CustomTabbedPage", navigationParams);
            AddRecent(_selectedSources);
        }


        public void OnSave(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //ExecuteSaveSourceToFavorite();
            //DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        async void ExecuteActionToFavorite(Source t)
        {
            if (t != null)
            {
                if (t.IsFavorite)
                {
                    RemoveFavorite(t);
                }
                else
                {
                    AddFavorite(t);
                }
            }

        }
        private void AddRecent(Source s)
        {
            try
            {
                var Reg = _realm.All<Recent>().Where(d => d.Id == s.Id);
                //var Reg = _realm.All<Recent>().First(d => d.Id == s.Id);

                if (Reg.Count() == 0)
                {
                    _realm.Write(() =>
                    {
                        _realm.Add(new Recent
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Category = s.Category,
                            Country = s.Country,
                            Description = s.Description,
                            Language = s.Language,
                            Url = s.Url,
                            OpenedDate = DateTime.Now.ToShortDateString()
                        });
                    });

                }
                else
                {
                    // Update an object with a transaction
                    //using (var trans = _realm.BeginWrite())
                    //{
                    //    //var RecentIten = _realm.All<Recent>().First(b => b.Id == s.Id);
                    //    using (var db = _realm.BeginWrite())
                    //    {
                    //    Reg.Filter()
                    //        Reg.OpenedDate = DateTime.Now.ToShortDateString();
                    //        db.Commit();
                    //    }
                    //}   
                }

            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void AddFavorite(Source s)
        {
            try
            {
                var exist = _realm.All<SourceRealm>().Where(d => d.Id == s.Id);
                if (exist.Count() == 0)
                {
                    _realm.Write(() =>
                    {
                        _realm.Add(new SourceRealm
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Category = s.Category,
                            Country = s.Country,
                            Description = s.Description,
                            Language = s.Language,
                            Url = s.Url
                        });
                    });
                    s.IsFavorite = true;
                    s.ImagenFavorite = "ic_shortcut_favorite.png";


                }
            }
            catch (Exception ex)
            {
                return;
            }

        }
        private void RemoveFavorite(Source s)
        {
            try
            {
                var reg = _realm.All<SourceRealm>().First(d => d.Id == s.Id);
                if (reg != null)
                {
                    using (var trans = _realm.BeginWrite())
                    {
                        _realm.Remove(reg);
                        trans.Commit();
                    }
                }
                s.IsFavorite = false;
                s.ImagenFavorite = "ic_shortcut_favorite_border.png";
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private bool isFavoriteSource(Source s)
        {

            var exist = _realm.All<SourceRealm>().Where(d => d.Id == s.Id);
            if (exist != null &&
                exist.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
