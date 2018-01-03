using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;
using Xamarin.Forms;

namespace Target.ViewModels
{
    public class MasterPageViewModel: BaseViewModel, IMasterPageViewModel
    {
       

        public MasterPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
        }
        
    }
}
