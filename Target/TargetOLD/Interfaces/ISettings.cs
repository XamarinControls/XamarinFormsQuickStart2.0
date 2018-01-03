using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface ISettings
    {
        bool IsManualFont { get; set; }
        int FontSize { get; set; }
        bool ShowConnectionErrors { get; set; }
        string AgreedToTermsDate { get; set; }
    }
}
