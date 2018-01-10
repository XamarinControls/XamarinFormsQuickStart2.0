using ReactiveUI;
using Target.Interfaces;
using Target.Models;
using Target.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using Xamarin.Forms;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Target.ViewModels
{
    public class MasterListViewModel : BaseViewModel, IMasterListViewModel
    {
        
        private ReactiveList<MasterPageItem> _items;

        public ReactiveList<MasterPageItem> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ReactiveList<MasterPageItem>();
                }
                return _items;
            }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }
        public MasterListViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            // this subscription is needed to refresh the size of text and images after changing
            // settings on the settings page.  Otherwise, they won't automatically
            // change size after changing the settings.
            MessagingCenter.Subscribe<ISettingsViewModel>(this, "mSettingsFontChanged", (sender) =>
            {
                var fireandforget2 = Task.Run(() => RunAsync(300));
            });
            var fireandforget = Task.Run(() => RunAsync(0));
            
        }
        
        private async Task RunAsync(int waitTime)
        {

            await Task.Delay(TimeSpan.FromMilliseconds(waitTime)).ConfigureAwait(false);
            var size = _settingsFactory.GetSettings().FontSize;
            _items = _items ?? new ReactiveList<MasterPageItem>();
            Device.BeginInvokeOnMainThread(() =>
            {
                _items.Clear();
                _items.Add(new MasterPageItem()
                {
                    Title = "HOME",
                    IconSource = "resource://Target.Resources.ic_home_black_36px.svg",
                    TargetType = typeof(HomePage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "ORGANIZATION",
                    IconSource = "resource://Target.Resources.ic_public_black_24px.svg",
                    TargetType = typeof(OrganizationPage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "PERSON",
                    IconSource = "resource://Target.Resources.ic_person_black_24px.svg",
                    TargetType = typeof(PersonPage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "ACTIVITIES",
                    IconSource = "resource://Target.Resources.ic_event_note_black_24px.svg",
                    TargetType = typeof(ActivitiesPage),
                    FontSize = size
                });
                if (defaultsFactory.GetIsLoginPageEnabled() && App.loggedIn)
                {
                    _items.Add(new MasterPageItem()
                    {
                        Title = "LOGOUT",
                        IconSource = "resource://Target.Resources.ic_vpn_key_black_24px.svg",
                        TargetType = typeof(LogoutPage),
                        FontSize = size
                    });
                }
                else
                {
                    if(defaultsFactory.GetIsLoginPageEnabled())
                    {
                        _items.Add(new MasterPageItem()
                        {
                            Title = "LOGIN",
                            IconSource = "resource://Target.Resources.ic_vpn_key_black_24px.svg",
                            TargetType = typeof(LoginPage),
                            FontSize = size
                        });
                    }
                    
                }
                _items.Add(new MasterPageItem()
                {
                    Title = "SETTINGS",
                    IconSource = "resource://Target.Resources.ic_settings_black_24px.svg",
                    TargetType = typeof(SettingsPage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "ABOUT",
                    IconSource = "resource://Target.Resources.ic_info_black_24px.svg",
                    TargetType = typeof(AboutPage),
                    FontSize = size
                });
            });
        }
    }
}
