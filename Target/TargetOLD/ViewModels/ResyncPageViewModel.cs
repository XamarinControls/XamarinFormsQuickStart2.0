using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.ViewModels
{
    public class ResyncPageViewModel : BaseViewModel, IResyncPageViewModel
    {
        public ResyncPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Resync Page";
        }
    }
}
