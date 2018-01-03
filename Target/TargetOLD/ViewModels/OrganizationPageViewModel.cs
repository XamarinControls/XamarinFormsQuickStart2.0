using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Target.ViewModels
{
    public class OrganizationPageViewModel : BaseViewModel, IOrganizationPageViewModel
    {
        public OrganizationPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Organization Page";
        }
    }
}
