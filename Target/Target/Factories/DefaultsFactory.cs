using Target.Interfaces;
using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Target.Factories
{
    public class DefaultsFactory : IDefaultsFactory
    {
        // General
        private readonly string _appName = "MYAPP";
        private readonly bool _checkInternet = false; // mostly used for external apps
        private readonly bool _showWifiErrors = false; // only useful for internal apps
        private readonly bool _isTermsPageEnabled = true;
        private readonly bool _isLoginPageEnabled = true;        

        //Theme
        private readonly Color _toolbarColor = Color.FromRgb(49, 62, 75);
        private readonly Color _toolbarTextColor = Color.White;
        private readonly Color _sideMenuColor = Color.FromRgb(49, 62, 75);
        private readonly Color _sideMenuTextColor = Color.White;

        // Settings Page Defaults
        private readonly bool _isManualFont = false; // Manual Font switch default setting
        private readonly int _fontSize = 17; // default font size of the app
        private readonly int _fontSizeMax = 34;  // maximum font size allowed when user manually changes it
        private readonly bool _showConnectionErrors = false; // Show Connection switch default setting


        public bool GetCheckInternet()
        {
            return _checkInternet;
        }
        public bool GetShowWifiErrors()
        {
            return _showWifiErrors;
        }
        public bool GetIsTermsPageEnabled()
        {
            return _isTermsPageEnabled;
        }
        public bool GetIsLoginPageEnabled()
        {
            return _isLoginPageEnabled;
        }
        public string GetAppName()
        {
            return _appName;
        }
        public Color GetToolbarColor()
        {
            return _toolbarColor;
        }
        public Color GetToolbarTextColor()
        {
            return _toolbarTextColor;
        }
        public Color GetSideMenuColor()
        {
            return _sideMenuColor;
        }
        public Color GetSideMenuTextColor()
        {
            return _sideMenuTextColor;
        }
        public bool GetIsManualFont()
        {
            return _isManualFont;
        }
        public int GetFontSize()
        {
            return _fontSize;
        }
        public int GetFontSizeMax()
        {
            return _fontSizeMax;
        }
        public bool GetShowConnectionErrors()
        {
            return _showConnectionErrors;
        }
    }
}
