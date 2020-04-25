using Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Client.ViewModels
{
	public class ClientContentPageViewModel : BaseViewModel
    {


        private readonly INavigationService _navigationService;
        public ClientContentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = true;
            _navigationService = navigationService;
            Title = "Local News";
            _ClientList= GetClient();
            ExecuteCommand = new DelegateCommand(ExecuteNavigation);
            IsBusy = false;

        }

        public ICommand ExecuteCommand
        {
            get;
        }


        private List<ClientModel> _ClientList = new List<ClientModel>();
        public List<ClientModel> ClientList
        {
            get { return _ClientList; }
            set { SetProperty(ref _ClientList, value); }
        }
        private ClientModel _selectedItem;
        public ClientModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        async void ExecuteNavigation()
        {
            var navigationParams = new NavigationParameters
            {
                {
                    "model", _selectedItem
                }
            };
            navigationParams.Add("PageName", _selectedItem.Name);
            navigationParams.Add("UrlPage", _selectedItem.Url);
            await _navigationService.NavigateAsync("CustomWebViewContentPage", navigationParams);


        }
        public List<ClientModel> GetClient()
        {
            List<ClientModel> Client = new List<ClientModel>();
           
            Client.Add(new ClientModel() { Name = "Acento", Url = "https://acento.com.do" });
            Client.Add(new ClientModel() { Name = "Remolacha.net",Url= "https://remolacha.net" });
            Client.Add(new ClientModel() { Name = "Diario Libre", Url = "https://www.diariolibre.com" });
            Client.Add(new ClientModel() { Name = "El Dia", Url= "http://eldia.com.do" });
            Client.Add(new ClientModel() { Name= "CDN", Url = "http://www.cdn.com.do" });
            Client.Add(new ClientModel() { Name = "Listin Diario", Url= "https://www.listindiario.com" });
            Client.Add(new ClientModel() { Name = "Hoy", Url = "http://hoy.com.do" });
            Client.Add(new ClientModel() { Name = "El Caribe", Url = "https://www.elcaribe.com.do" });

            return Client;
        }

    }
}
