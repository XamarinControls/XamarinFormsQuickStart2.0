using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Target.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel, IActivitiesPageViewModel
    {
        
        public ActivitiesPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Activities Page";
        }
    }
}
