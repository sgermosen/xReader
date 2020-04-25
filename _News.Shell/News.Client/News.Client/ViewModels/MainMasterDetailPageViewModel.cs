using News.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace News.Client.ViewModels
{
	public class MainMasterDetailPageViewModel : BaseViewModel
	{
        private readonly INavigationService _navigationService;
        public MainMasterDetailPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Menus";
            IsBusy = true;
            _menuList = GetMenues();
            ExecuteCommand = new DelegateCommand(ExecuteNavigation);
            IsBusy = false;
        }


        public ICommand ExecuteCommand
        {
            get;
        }


        private List<Menu> _menuList = new List<Menu>();
        public List<Menu> MenuList
        {
            get { return _menuList; }
            set { SetProperty(ref _menuList, value); }
        }
        private Menu _itemSelected;
        public Menu ItemSelected
        {
            get => _itemSelected;
            set => SetProperty(ref _itemSelected, value);
        }


        async void ExecuteNavigation()
        {
            var navigationParams = new NavigationParameters
            {
                {
                    "model", _itemSelected
                }
            };
            await NavigationService.NavigateAsync(new Uri(_itemSelected.ViewA, UriKind.Relative));
            //await _navigationService.NavigateAsync("SourcesContentPage", navigationParams);
        }
        public List<Menu> GetMenues()
        {
            List<Menu> menu = new List<Menu>();
            menu.Add(new Menu() { Id = 0, Name = "Home", Icon = "ic_home.png", Descripcion = "Return Ini", ViewA= "/MainMasterDetailPage/NavigationPage/News.ClientContentPage" });
            menu.Add(new Menu() { Id = 2, Name = "Sources by Country", Icon = "ic_flag.png", Descripcion = "Volver al Inicio", ViewA = "/MainMasterDetailPage/NavigationPage/CountryContentPage" });
            menu.Add(new Menu() { Id = 3, Name = "Sources by Category", Icon = "ic_airplay.png", Descripcion = "Buscar por categoria", ViewA = "/MainMasterDetailPage/NavigationPage/CategoryContentPage" });
            menu.Add(new Menu() { Id = 4, Name = "Sources by language", Icon = "ic_language.png", Descripcion = "Volver al Inicio", ViewA = "/MainMasterDetailPage/NavigationPage/LanguageContentPage" });
            menu.Add(new Menu() { Id = 5, Name = "Favorite Sources ", Icon = "ic_favorite.png", Descripcion = "Volver al Inicio", ViewA = "/MainMasterDetailPage/NavigationPage/FavoriteContentPage" });
            menu.Add(new Menu() { Id = 6, Name = "Recent Sources ", Icon = "ic_update.png", Descripcion = "Volver al Inicio", ViewA = "/MainMasterDetailPage/NavigationPage/RecentContentPage" });
            menu.Add(new Menu() { Id = 7, Name = "All Sources", Icon = "ic_receipt.png", Descripcion = "Volver al Inicio", ViewA = "/MainMasterDetailPage/NavigationPage/SourcesTabbedPage" });

            return menu;
        }
    }
}
