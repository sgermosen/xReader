using System;

using News.Client.Models;
using Prism.Navigation;

namespace News.Client.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item, INavigationService navi):base(navi)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
