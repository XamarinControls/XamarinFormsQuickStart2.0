using ReactiveUI;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.ViewModels
{
    public class HomePageViewModel : BaseViewModel, IHomePageViewModel
    {
        private string sourceImg;
        public string SourceImg
        {
            get
            {
                return sourceImg;
            }
            set { this.RaiseAndSetIfChanged(ref sourceImg, value); }
        }

        public HomePageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            sourceImg = "resource://Target.Resources.ic_home_black_36px.svg";
        }
    }
}
