using News.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace News.Client.ViewModels
{
	public class FavoriteContentPageViewModel : BaseViewModel
    {

        private readonly Realm _realm;
        private readonly INavigationService _navigationService;
        public FavoriteContentPageViewModel(INavigationService navigationService, Realm realm) :
        base(navigationService)
        {

            _navigationService = navigationService;

            Title = "Favorite";
            _realm = realm;
            SourcesLocalDataList = new ObservableCollection<SourceRealm>();
            ExecuteCommand = new DelegateCommand(Execute);
            LoadSourcesFromLocalDatabase();

          
            //IsBusy = true;
        }


        public ICommand ExecuteCommand
        {
            get;
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        async Task LoadSourcesFromLocalDatabase()
        {
            IsBusy = true;
            var result = _realm.All<SourceRealm>();
            SourcesLocalDataList = new ObservableCollection<SourceRealm>(result);
            IsBusy = false;
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
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    await LoadSourcesFromLocalDatabase();
                    IsRefreshing = false;
                });
            }
        }


        private SourceRealm _selectedSources;
        public SourceRealm SelectedSources
        {
            get => _selectedSources;
            set => SetProperty(ref _selectedSources, value);
        }


        private ObservableCollection<SourceRealm> _sourcesLocalDataList;
        public ObservableCollection<SourceRealm> SourcesLocalDataList
        {
            get { return _sourcesLocalDataList; }
            set { SetProperty(ref _sourcesLocalDataList, value); }
        }


        async void Execute()
        {


            var navigationParams = new NavigationParameters
            {
                { "model", _selectedSources }
            };

            StaticObject.MasterSelected = eMasterSelected.Favorite;
            await _navigationService.NavigateAsync("CustomTabbedPage", navigationParams);
            AddRecent(_selectedSources);
        }

        private void AddRecent(SourceRealm s)
        {
            try
            {
                var Reg = _realm.All<Recent>().First(d => d.Id == s.Id);

                if (Reg == null)
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
                    //s.isFavorite = true;
                    //s.ImagenFavorite = "ic_shortcut_favorite.png";
                }
                //else
                //{
                //    // Update an object with a transaction
                //    using (var trans = _realm.BeginWrite())
                //    {
                //        //var RecentIten = _realm.All<Recent>().First(b => b.Id == s.Id);
                //        using (var db = _realm.BeginWrite())
                //        {
                //            Reg.OpenedDate = DateTime.Now.ToShortDateString();
                //            db.Commit();
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                return;
            }

        }

    }
}
