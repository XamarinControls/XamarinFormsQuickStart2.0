using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface IContentPageBase
    {
        double GetSquaredImageSize(int x);
        void ShowToast(INotificationOptions options);

    }
}