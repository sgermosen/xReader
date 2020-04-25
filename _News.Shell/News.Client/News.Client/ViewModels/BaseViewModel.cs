using News.Client.Models;
using News.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace News.Client.ViewModels
{
    public enum eMasterSelected
    {
        Nothing = 0,
        Category = 1,
        Source = 2,
        Country = 3,
        Language = 4,
        Favorite = 5,
        Recent = 6
    }

    public enum eHorizontalOptions
    {
        Center = 1,
        CenterAndExpand = 2,
        End = 3,
        EndAndExpand = 4,
        Fill = 5,
        FillAndExpand = 6,
        Start = 7,
        StartAndExpand = 8
    }
    public class BaseViewModel : BindableBase, INotifyPropertyChanged, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }


        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        //private string _horizontalOptions;
        //public View.FocusRequestArgs. HorizontalOptions
        //{

        //    get { _horizontalOptions; }
        //    set { SetProperty(ref _horizontalOptions, value); }
        //}

        public bool _isVisibleSourceTab;
        public bool IsVisibleSourceTab
        {
            get { return _isVisibleSourceTab; }
            set { SetProperty(ref _isVisibleSourceTab, value); }
        }

        private bool isRunning = false;
        private bool isNotRunning = true;
        public bool IsRunning
        {
            get {
                return isRunning;
            }
            set {
                SetProperty(ref isRunning, value);
                SetProperty(ref isNotRunning, !value, nameof(IsNotRunning));

            }
        }

        public bool IsNotRunning
        {
            get {
                return isNotRunning;
            }
        }

        private bool _isFavorite;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
            _isVisibleSourceTab = true;
        }
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }



        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private string _labelInformation;
        public string LabelInformation
        {
            get { return _labelInformation; }
            set { SetProperty(ref _labelInformation, value); }
        }

        private bool _isVisibleWaitAbsoluteLayout;
        public bool IsVisibleWaitAbsoluteLayout
        {
            get { return _isVisibleWaitAbsoluteLayout; }
            set {

                SetProperty(ref _isVisibleWaitAbsoluteLayout, value);
                SetProperty(ref _isNoVisibleWaitAbsoluteLayout, !_isVisibleWaitAbsoluteLayout, nameof(IsNoVisibleWaitAbsoluteLayout));
            }
        }


        private bool _isNoVisibleWaitAbsoluteLayout;
        public bool IsNoVisibleWaitAbsoluteLayout
        {
            get;
        }

        private bool _isVisibleWaitIndicator;
        public bool IsVisibleWaitIndicator
        {
            get { return _isVisibleWaitIndicator; }
            set { SetProperty(ref _isVisibleWaitIndicator, value); }
        }


        public DelegateCommand<string> OnNavigateCommand
        {
            get;
            set;
        }
        async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }

        public virtual void Destroy()
        {

        }


        //public void OnMore(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);
        //    //DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        //}

        //public void OnDelete(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);
        //    //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        //}
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
