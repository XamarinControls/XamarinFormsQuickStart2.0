using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Target.Interfaces;
using Xamarin.Forms;

namespace Target.ViewModels
{
    public class AboutPageViewModel : BaseViewModel, IAboutPageViewModel
    {
        HtmlWebViewSource htmlSource;
        public HtmlWebViewSource HTMLSource
        {
            get { return htmlSource; }
            set { this.RaiseAndSetIfChanged(ref htmlSource, value); }
        }

        string appName;
        public string AppName
        {
            get { return appName; }
            set { this.RaiseAndSetIfChanged(ref appName, value); }
        }

        string version;
        public string Version
        {
            get { return version; }
            set { this.RaiseAndSetIfChanged(ref version, value); }
        }

        bool isTermsOn;
        public bool IsTermsOn
        {
            get { return isTermsOn; }
            set { this.RaiseAndSetIfChanged(ref isTermsOn, value); }
        }
        public AboutPageViewModel(ISettingsService settingsService, ISettingsFactory settingsFactory)
            : base(settingsService, settingsFactory)
        {
            Greeting = "About Page";
            InitializeSettings();
            HTMLSource = HTMLSource ?? new HtmlWebViewSource();
            HTMLSource.BaseUrl = DependencyService.Get<IPlatformStuff>().GetBaseUrl();           
            //var htmlbody = DependencyService.Get<IPlatformStuff>().GetHtmlFileAsString("about.html");
            //htmlbody = Regex.Replace(htmlbody, "replacevalue", oidcRedirectUrl);
            //var encodedHtmlBody = "data:text/html;charset=utf-8," +  Uri.EscapeUriString(htmlbody);
            HTMLSource.Html = @"
            <!DOCTYPE html>
            <html>
                <head>
                    <title>oidc-client test</title>
                    <link rel=""stylesheet"" href=""bootstrap.css"">
                    <link rel=""stylesheet"" href=""indigo-pink.css"">        
                    <link rel=""stylesheet"" href=""styles.css"">        
                </head>
                <body id=""mybod"">
                    <md-content layout-padding>
                    <div class=""md-headline""><strong>About Us</strong></div><br>
                        <p>
                            <strong>Some Company®</strong> is the leader in blah, blah, blah...
                        </p>
                    </md-content>           
                </body>
            </html>";
            
        }
        private void InitializeSettings()
        {
            if (Constants.IsTermsPageEnabled)
            {
                IsTermsOn = true;
            }
            Version = App.Version;
            AppName = Constants.AppName;
        }
    }
}
