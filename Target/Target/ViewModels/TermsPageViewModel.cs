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
    public class TermsPageViewModel : BaseViewModel, ITermsPageViewModel
    {
        HtmlWebViewSource htmlSource;
        public HtmlWebViewSource HTMLSource
        {
            get { return htmlSource; }
            set { this.RaiseAndSetIfChanged(ref htmlSource, value); }
        }
        string agreedOn;
        public string AgreedOn
        {
            get { return agreedOn; }
            set { this.RaiseAndSetIfChanged(ref agreedOn, value); }
        }
        bool isTermsOn;
        public bool IsTermsOn
        {
            get { return isTermsOn; }
            set { this.RaiseAndSetIfChanged(ref isTermsOn, value); }
        }
        public TermsPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "Terms And Conditions";
            HTMLSource = HTMLSource ?? new HtmlWebViewSource();
            HTMLSource.BaseUrl = DependencyService.Get<IPlatformStuff>().GetBaseUrl();
            //source.Html = @"<html><head></head><body><p>Hi</p></body></html>";
            HTMLSource.Html = @"
            <html>
            <head>
            <link rel=""stylesheet"" href=""bootstrap.css"">
            <link rel=""stylesheet"" href=""indigo-pink.css"">
            </head>
            <body>
            <iframe src=""Terms.html"" name=""targetframe"" allowTransparency=""true"" scrolling=""yes"" frameborder=""0"" style=""width:100%;height:100%""></ iframe >
            </body>
            </html>
            ";
            var fireandforget = Task.Run(async () => await InitializeSettings());
        }
        private async Task InitializeSettings()
        {
            var settings = await _settingsService.GetSettings();
            if (string.IsNullOrEmpty(settings.AgreedToTermsDate))
            {
                IsTermsOn = true;
            }
            AgreedOn = settings.AgreedToTermsDate;
        }
    }
}
