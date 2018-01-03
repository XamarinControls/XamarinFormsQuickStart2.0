using FFImageLoading;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Plugin.GoogleAnalytics;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Xamarin.Forms;

namespace Target.Converters
{
    class PaddingTopBottomConverter : IBindingTypeConverter, IPaddingTopBottomConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(string))
            {
                return 100; // any number other than 0 signifies conversion is possible.
            }
            return 0;
        }
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        { 
            try
            {
                result = new Thickness(0, Convert.ToInt32(from), 0, Convert.ToInt32(from));
            }
            catch (Exception ex)
            {
                //GoogleAnalytics.Current.Tracker.SendView(ex.Message);
                GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                result = null;
                return false;
            }

            return true;
        }
    }
}
