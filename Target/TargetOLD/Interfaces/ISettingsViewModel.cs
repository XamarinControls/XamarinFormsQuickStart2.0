using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface ISettingsViewModel
    {
        int FontSize { get; set; }
        bool IsManualFontOn { get; set; }
        bool IsSwitchOn { get; set; }
        bool ShowConnectionErrors { get; set; }
        ReactiveCommand IsManualFontOnClicked { get; }
        ReactiveCommand ShowConnectionErrorsCommand { get; }
        ReactiveCommand FontSliderChanged { get; }

    }
}
