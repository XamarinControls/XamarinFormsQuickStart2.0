using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Target.Interfaces
{
    public interface ISettingsViewModel
    {
        string FontSliderLabel { get; set; }
        int FontSize { get; set; }
        bool IsManualFontOn { get; set; }
        bool ShowConnectionErrors { get; set; }

    }
}
