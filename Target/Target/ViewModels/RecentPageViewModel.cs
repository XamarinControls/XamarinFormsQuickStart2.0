using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.ViewModels
{
    public class RecentPageViewModel : BaseViewModel, IRecentPageViewModel
    {
        public RecentPageViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Title = "Recent";
            Greeting = "Recent Page";
        }
    }
}