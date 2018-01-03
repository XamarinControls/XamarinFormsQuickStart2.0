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
    class SvgImageSourceConverterForReactive : IBindingTypeConverter, ISvgImageSourceConverterForReactive
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
                result = ConvertFromInvariantString(from.ToString());
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
        public object ConvertFromInvariantString(string value)
        {
            var imgConverter = new FFImageLoading.Forms.ImageSourceConverter();
            var text = value as string;
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var xfSource = imgConverter.ConvertFromInvariantString(value) as ImageSource;

            if (text.IsSvgFileUrl() || text.IsSvgDataUrl())
            {
                return new SvgImageSource(xfSource, 0, 0, true);
            }

            return xfSource;
        }
    }
}
