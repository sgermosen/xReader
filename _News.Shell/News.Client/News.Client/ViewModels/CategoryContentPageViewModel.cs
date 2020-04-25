using News.Client.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;

namespace News.Client.ViewModels
{
    public class CategoryContentPageViewModel : BaseViewModel
    {

        private readonly INavigationService _navigationService;
        public CategoryContentPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {

            _navigationService = navigationService;
            Title = "Category";
            IsBusy = true;
            _categoryList = GetCategory();
            ExecuteCommand = new DelegateCommand(ExecuteNavigation);
            IsBusy = false;

        }

        public ICommand ExecuteCommand
        {
            get;
        }


        private List<Category> _categoryList = new List<Category>();
        public List<Category> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }


        async void ExecuteNavigation()
        {
            var navigationParams = new NavigationParameters
            {
                {
                    "model", _selectedCategory
                }
            };
            StaticObject.MasterSelected = eMasterSelected.Category;

            navigationParams.Add("Name", _selectedCategory.Name);
            await _navigationService.NavigateAsync("SourcesContentPage", navigationParams);
        }
        public List<Category> GetCategory()
        {
            List<Category> category = new List<Category>
            {
                new Category() {Id = "business", Name = eCategory.Business},
                new Category() {Id = "entertainment", Name = eCategory.Entertainment},
                new Category() {Id = "general", Name = eCategory.General},
                new Category() {Id = "health", Name = eCategory.Health},
                new Category() {Id = "science", Name = eCategory.Science},
                new Category() {Id = "sports", Name = eCategory.Sports},
                new Category() {Id = "technology", Name = eCategory.Technology},
                new Category() {Id = "all", Name = eCategory.All}
            };

            return category;
        }

    }
}
