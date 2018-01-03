using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;

namespace Target.ViewModels
{
    public class LogoutPageViewModel : BaseViewModel, ILogoutPageViewModel
    {
        public LogoutPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = $"Leaving {Constants.AppName}?";
        }
    }
}
