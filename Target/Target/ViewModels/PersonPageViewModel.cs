using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.ViewModels
{
    public class PersonPageViewModel : BaseViewModel, IPersonPageViewModel
    {
        public PersonPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Person Page";
        }
    }
}
