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
	public class LanguageContentPageViewModel : BaseViewModel
	{
        private readonly INavigationService _navigationService;
        public LanguageContentPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Languages";
            IsBusy = true;
            _languageFullList = GetLanguageFull();
            ExecuteCommand = new DelegateCommand(ExecuteNavigation);
            IsBusy = false;
        }



        public ICommand ExecuteCommand
        {
            get;
        }


        private List<LanguageFull> _languageFullList = new List<LanguageFull>();
        public List<LanguageFull> LanguageFullList
        {
            get { return _languageFullList; }
            set { SetProperty(ref _languageFullList, value); }
        }
        private LanguageFull _selectedItem;
        public LanguageFull SelectedItem
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
            StaticObject.MasterSelected = eMasterSelected.Language;
            await _navigationService.NavigateAsync("SourcesContentPage", navigationParams);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }
        public List<LanguageFull> GetLanguageFull()
        {
            List<LanguageFull> Language = new List<LanguageFull>();
            Language.Add(new LanguageFull() { Id = "ar", Name = "Arabe"});
            Language.Add(new LanguageFull() { Id = "de", Name = "Alemán"});
            Language.Add(new LanguageFull() { Id = "en", Name = "Inglés" });
            Language.Add(new LanguageFull() { Id = "es", Name = "Español (o Castellano)" });
            Language.Add(new LanguageFull() { Id = "fr", Name = "Hrancés" });
            Language.Add(new LanguageFull() { Id = "he", Name = "Hebreo" });
            Language.Add(new LanguageFull() { Id = "it", Name = "Italiano" });
            Language.Add(new LanguageFull() { Id = "nl", Name = "Neerlandés (u Holandés)" });
            Language.Add(new LanguageFull() { Id = "no", Name = "Noruego"});
            Language.Add(new LanguageFull() { Id = "pt", Name = "Portugués" });
            Language.Add(new LanguageFull() { Id = "ru", Name = "Ruso" });
            Language.Add(new LanguageFull() { Id = "se", Name = "Sami Septentrional" });
            Language.Add(new LanguageFull() { Id = "zh", Name = "Chino" });

            return Language;
        }
    }
}
