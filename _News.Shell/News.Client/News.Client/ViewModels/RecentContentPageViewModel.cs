using News.Client.Models;
using News.Client.Services;
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
	public class RecentContentPageViewModel : BaseViewModel
	{
        private readonly Realm _realm;
        //private DbRealmServices RealmServices;
        private readonly INavigationService _navigationService;
        public RecentContentPageViewModel(INavigationService navigationService, Realm realm) :
        base(navigationService)
        {
            try
            {
                Title = "Recents";
                ExecuteCommand = new DelegateCommand(Execute);
                CmdRemoveItemSelected = new DelegateCommand(ExecuteRemoveItemSelect);
                _realm = realm;
                _navigationService = navigationService;
                //RealmServices = new DbRealmServices();
                SourcesLocalDataList = new ObservableCollection<Recent>();

                LoadSourcesFromLocalDatabase();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            //IsBusy = true;
        }
       async Task LoadSourcesFromLocalDatabase()
        {
            IsBusy = true;
            //DeleteRecent();
            //var allLocations = _realm.All<Recent>().OrderByDescending(s=> Convert.ToDateTime(s.OpenedDate));       
            var result = _realm.All<Recent>();
            SourcesLocalDataList = new ObservableCollection<Recent>(result);
            //foreach (var item in allLocations)
            //{
                //SourcesLocalDataList.Add(item);
            //}
            IsBusy = false;
            if(SourcesLocalDataList.Count==0)
            {

            }

        }

        public ICommand ExecuteCommand
        {
            get;
        }
        public ICommand CmdRemoveItemSelected
        {
            get;
        }

        private Recent _selectedSources;
        public Recent SelectedSources
        {
            get => _selectedSources;
            set => SetProperty(ref _selectedSources, value);
        }



        private ObservableCollection<Recent> _sourcesLocalDataList;
        public ObservableCollection<Recent> SourcesLocalDataList
        {
            get { return _sourcesLocalDataList; }
            set { SetProperty(ref _sourcesLocalDataList, value); }
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

        async void Execute()
        {


            var navigationParams = new NavigationParameters
            {
                { "model", _selectedSources }
            };

            StaticObject.MasterSelected = eMasterSelected.Recent;
            await _navigationService.NavigateAsync("CustomTabbedPage", navigationParams);

            
        }

        public void ExecuteRemoveItemSelect()
        {
            try
            {
                if (!RemoveItem(SelectedSources))
                {

                    if (SelectedSources != null)
                    {

                        SourcesLocalDataList.Remove(SelectedSources);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return;
            }
        }
               
        


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
         
                 //LoadSourcesFromLocalDatabase();
        }



        private bool RemoveItem(Recent r)
        {
            try
            {
                var reg = _realm.All<Recent>().First(d => d.Id == r.Id);
                if (reg != null)
                {
                    using (var trans = _realm.BeginWrite())
                    {
                        _realm.Remove(reg);
                        trans.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        private bool DeleteOldRecent()
        {
            try
            {
                var allItems = _realm.All<Recent>().Where(b => Convert.ToDateTime(b.OpenedDate) < DateTime.Today.AddDays(-10));
                if (allItems != null && allItems.Count() > 0)
                {
                    foreach (var item in allItems)
                    {
                        using (var trans = _realm.BeginWrite())
                        {
                            _realm.Remove(item);
                            trans.Commit();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
