using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target.Models
{
    public class BaseListItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
        public int FontSize { get; set; }
        //public IObservable<Color> FontColor { get; set; }
        public Color FontColor { get; set; }
    }
}
