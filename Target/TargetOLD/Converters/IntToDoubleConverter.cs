using Plugin.GoogleAnalytics;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;

namespace Target.Converters
{
    public class IntToDoubleConverter : IBindingTypeConverter, IIntToDoubleConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(int))
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
                int number;

                if (!Int32.TryParse(conversionHint as string, out number))
                    number = 1;

                result = (double)(number * (int)from);
            }
            catch (Exception ex)
            {
                //GoogleAnalytics.Current.Tracker.SendView(ex.Message);
                GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                //this.Log().WarnException("Couldn't convert object to type: " + toType, ex);
                result = null;
                return false;
            }

            return true;
        }
    }
}
