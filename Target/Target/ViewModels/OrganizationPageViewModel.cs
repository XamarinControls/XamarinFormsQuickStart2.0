using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Target.Models;
using Xamarin.Forms;
using Target.Pages;

namespace Target.ViewModels
{
    public class OrganizationPageViewModel : BaseViewModel, IOrganizationPageViewModel
    {
        private ReactiveList<BaseListItem> _items;

        public ReactiveList<BaseListItem> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ReactiveList<BaseListItem>();
                }
                return _items;
            }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }
        public OrganizationPageViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Title = "Organization";
            Greeting = "Organization Page";
            var fontsize = settingsFactory.GetSettings().FontSize;
            _items = _items ?? new ReactiveList<BaseListItem>();

            // this needs to be converted to a service
                _items.Clear();
                _items.Add(new BaseListItem()
                {
                    Title = "Some Organization",
                    IconSource = "resource://Target.Resources.ic_public_black_24px.svg",
                    TargetType = typeof(RecentPage),
                    FontSize = fontsize,
                    FontColor = Color.Black
                });
                _items.Add(new BaseListItem()
                {
                    Title = "Organization 2",
                    IconSource = "resource://Target.Resources.ic_public_black_24px.svg",
                    TargetType = typeof(RecentPage),
                    FontSize = fontsize,
                    FontColor = Color.Black
                });
            
        }
    }
}
