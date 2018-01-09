using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.ViewModels
{
    public class PersonPageViewModel : BaseViewModel, IPersonPageViewModel
    {
        public PersonPageViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Greeting = "Person Page";
        }
    }
}
