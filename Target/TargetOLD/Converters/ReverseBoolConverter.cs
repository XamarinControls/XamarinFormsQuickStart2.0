using Target.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Plugin.GoogleAnalytics;

namespace Target.Converters
{
    public class ReverseBoolConverter : IBindingTypeConverter, IReverseBoolConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(bool))
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
                result = !(bool)from;
            }
            catch (Exception ex)
            {
                GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                result = null;
                return false;
            }

            return true;
        }
    }
}
