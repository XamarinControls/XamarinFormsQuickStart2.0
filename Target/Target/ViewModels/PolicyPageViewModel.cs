using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Xamarin.Forms;

namespace Target.ViewModels
{
    public class PolicyPageViewModel : BaseViewModel, IPolicyPageViewModel
    {
        HtmlWebViewSource htmlSource;
        public HtmlWebViewSource HTMLSource
        {
            get { return htmlSource; }
            set { this.RaiseAndSetIfChanged(ref htmlSource, value); }
        }
        public PolicyPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Privacy Policy";
            HTMLSource = HTMLSource ?? new HtmlWebViewSource();
            HTMLSource.BaseUrl = DependencyService.Get<IPlatformStuff>().GetBaseUrl();
            HTMLSource.Html = @"
            <html>
            <head>
            <link rel=""stylesheet"" href=""bootstrap.css"">
            <link rel=""stylesheet"" href=""indigo-pink.css"">
            </head>
            <body>
            <iframe src=""Policy.html"" name=""targetframe"" allowTransparency=""true"" scrolling=""yes"" frameborder=""0"" style=""width:100%;height:100%""></ iframe >
            </body>
            </html>
            ";
        }
    }
}
