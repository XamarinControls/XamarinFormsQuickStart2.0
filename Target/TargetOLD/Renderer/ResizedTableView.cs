using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target.Renderer
{
    public class ResizedTableView : TableView
    {
        public static readonly BindableProperty FontSizeProperty =
    BindableProperty.Create("FontSize", typeof(double), typeof(ResizedTableView), (double)16);
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
    }
}
