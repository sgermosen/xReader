using Xamarin.Forms;

namespace News.Client.Behaviors
{
    public class SearchBarFocusBehavior : Prism.Behaviors.BehaviorBase<SearchBar>
    {
        public static readonly BindableProperty OnFocusProperty =
            BindableProperty.Create(nameof(OnFocus), typeof(bool?), typeof(SearchBarFocusBehavior), defaultValue: false,
                propertyChanged: OnFocusPropertyChanged);

        private static void OnFocusPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((SearchBarFocusBehavior)sender).UpdateFocused();
        }

        SearchBar _searchBar;

        public bool? OnFocus
        {
            get => (bool?)GetValue(OnFocusProperty);
            set => SetValue(OnFocusProperty, value);
        }

        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            _searchBar = bindable;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            _searchBar = null;
        }

        private void UpdateFocused()
        {
            if (OnFocus == true)
                _searchBar.Focus();
            else
                _searchBar.Unfocus();
        }
    }
}
