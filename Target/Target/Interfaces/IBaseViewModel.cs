using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface IBaseViewModel
    {
        string Title { get; set; }
        string Greeting { get; set; }
        string ToastMessage { get; set; }
        int FontSize { get; set; }
        ViewModelActivator Activator { get; }
    }
}