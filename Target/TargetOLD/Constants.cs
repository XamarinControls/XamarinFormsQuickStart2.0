using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target
{
    public static class Constants
    {
        
        // General defaults
        public static bool CheckInternet = false; // mostly used for external apps
        public static bool ShowWifiErrors = false; // only useful for internal apps
        public static bool IsTermsPageEnabled = true;
        public static bool IsLoginPageEnabled = true;
        public static string AppName = "MYAPP";

        //Colors
        public static Color ToolbarColor = Color.FromRgb(49, 62, 75);
        public static Color ToolbarTextColor = Color.White;
        public static Color SideMenuColor = Color.FromRgb(49, 62, 75);
        public static Color SideMenuTextColor = Color.White;

        // Settings Defaults
        public static bool IsManualFont = false;
        public static int FontSize = 16;
        public static int FontSizeSmallSubtract = 5;
        public static int FontSizeLargeAdd = 5;
        public static int FontSizeMax = 34;
        public static bool ShowConnectionErrors = false;
        
    }
    
}
