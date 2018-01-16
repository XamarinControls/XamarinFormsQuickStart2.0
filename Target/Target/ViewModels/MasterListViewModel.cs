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
        public MasterListViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            // DONT TRY AND SET TITLE FROM VIEWMODEL for MasterListPage, it won't work
            //Title = defaultsFactory.GetAppName();

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
            var color = defaultsFactory.GetSideMenuTextColor();
            _items = _items ?? new ReactiveList<BaseListItem>();
            Device.BeginInvokeOnMainThread(() =>
            {
                _items.Clear();
                _items.Add(new BaseListItem()
                {
                    Title = "HOME",
                    IconSource = "resource://Target.Resources.ic_home_black_36px.svg",
                    TargetType = typeof(HomePage),
                    FontSize = size,
                    FontColor = color
                });
                _items.Add(new BaseListItem()
                {
                    Title = "ORGANIZATION",
                    IconSource = "resource://Target.Resources.ic_public_black_24px.svg",
                    TargetType = typeof(OrganizationPage),
                    FontSize = size,
                    FontColor = color
                });
                _items.Add(new BaseListItem()
                {
                    Title = "PERSON",
                    IconSource = "resource://Target.Resources.ic_person_black_24px.svg",
                    TargetType = typeof(PersonPage),
                    FontSize = size,
                    FontColor = color
                });
                _items.Add(new BaseListItem()
                {
                    Title = "ACTIVITIES",
                    IconSource = "resource://Target.Resources.ic_event_note_black_24px.svg",
                    TargetType = typeof(ActivitiesPage),
                    FontSize = size,
                    FontColor = color
                });
                if (defaultsFactory.GetIsLoginPageEnabled() && App.loggedIn)
                {
                    _items.Add(new BaseListItem()
                    {
                        Title = "LOGOUT",
                        IconSource = "resource://Target.Resources.ic_vpn_key_black_24px.svg",
                        TargetType = typeof(LogoutPage),
                        FontSize = size,
                        FontColor = color
                    });
                }
                else
                {
                    if (defaultsFactory.GetIsLoginPageEnabled())
                    {
                        _items.Add(new BaseListItem()
                        {
                            Title = "LOGIN",
                            IconSource = "resource://Target.Resources.ic_vpn_key_black_24px.svg",
                            TargetType = typeof(LoginPage),
                            FontSize = size,
                            FontColor = color
                        });
                    }

                }
                _items.Add(new BaseListItem()
                {
                    Title = "SETTINGS",
                    IconSource = "resource://Target.Resources.ic_settings_black_24px.svg",
                    TargetType = typeof(SettingsPage),
                    FontSize = size,
                    FontColor = color
                });
                _items.Add(new BaseListItem()
                {
                    Title = "ABOUT",
                    IconSource = "resource://Target.Resources.ic_info_black_24px.svg",
                    TargetType = typeof(AboutPage),
                    FontSize = size,
                    FontColor = color
                });
            });
        }
    }
}
