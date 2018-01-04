using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Target.Interfaces
{
    public interface IDefaultsFactory
    {
        bool GetCheckInternet();
        bool GetShowWifiErrors();
        bool GetIsTermsPageEnabled();
        bool GetIsLoginPageEnabled();
        string GetAppName();
        Color GetToolbarColor();
        Color GetToolbarTextColor();
        Color GetSideMenuColor();
        Color GetSideMenuTextColor();
        bool GetIsManualFont();
        int GetFontSize();
        int GetFontSizeMax();
        bool GetShowConnectionErrors();
    }
}
