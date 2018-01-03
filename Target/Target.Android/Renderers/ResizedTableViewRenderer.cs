using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Target.Renderer;
using Xamarin.Forms;
using Target.Android.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ResizedTableView), typeof(ResizedTableViewRenderer))]
namespace Target.Android.Renderers
{
    public class ResizedTableViewRenderer : TableViewRenderer
    {
        protected override TableViewModelRenderer GetModelRenderer(global::Android.Widget.ListView listView, TableView view)
        {
            return new CustomTableViewModelRenderer(this.Context, listView, view);
        }
        private class CustomTableViewModelRenderer : TableViewModelRenderer
        {
            private readonly ResizedTableView _resizedTableView;

            public CustomTableViewModelRenderer(Context context, global::Android.Widget.ListView listView, TableView view) : base(context, listView, view)
            {
                _resizedTableView = view as ResizedTableView;
            }

            public override global::Android.Views.View GetView(int position, global::Android.Views.View convertView, ViewGroup parent)
            {
                var view = base.GetView(position, convertView, parent);

                var element = GetCellForPosition(position);

                // section header will be a TextCell
                if (element.GetType() == typeof(TextCell))
                {
                    try
                    {
                        // Get the textView of the actual layout
                        var textView = ((((view as LinearLayout).GetChildAt(0) as LinearLayout).GetChildAt(1) as LinearLayout).GetChildAt(0) as TextView);

                        textView.TextSize = (float)_resizedTableView.FontSize;
                        //((IElementController)element).SetValueFromRenderer(Label.FontSizeProperty, _resizedTableView.FontSize);
                    }
                    catch (Exception) { }
                }

                return view;
            }
        }
    }
    
}
        //    base.OnElementChanged(e);
        //    if (Control != null)
        //    {
        //        if(e.NewElement != null)
        //        {
        //            var resizedTableView = (ResizedTableView)e.NewElement;
        //            foreach(var tablesection in e.NewElement.Root)
        //            {
        //                foreach(ViewCell viewcell in tablesection)
        //                {
        //                    viewcell.View
        //                    //foreach (var x in ((Grid)viewcell.View).Children)
        //                    //{
        //                    //    //if(x.GetType() == typeof(Label))
        //                    //    //{
        //                    //    //    (x as Label).FontSize = resizedTableView.FontSize;

        //                    //    //}
        //                    //}
        //                }
        //            }
        //            //Control.Adapter.
        //        }
        //    }
        //}
        //protected override TableViewModelRenderer GetModelRenderer(global::Android.Widget.ListView listView, TableView view)
        //{
        //    return new CustomHeaderTableViewModelRenderer(this.Context, listView, view);
        //}

        //private class CustomHeaderTableViewModelRenderer : TableViewModelRenderer
        //{
        //    private readonly ResizedTableView _ResizedTableView;

        //    public CustomHeaderTableViewModelRenderer(Context context, global::Android.Widget.ListView listView, TableView view) : base(context, listView, view)
        //    {
        //        _ResizedTableView = view as ResizedTableView;
        //    }

        //    public override global::Android.Views.View GetView(int position, global::Android.Views.View convertView, ViewGroup parent)
        //    {
        //        var view = base.GetView(position, convertView, parent);

        //        var element = GetCellForPosition(position);

        //        // section header will be a TextCell
        //        if (element.GetType() == typeof(TextCell))
        //        {
        //            try
        //            {
        //                // Get the textView of the actual layout
        //                var textView = ((((view as LinearLayout).GetChildAt(0) as LinearLayout).GetChildAt(1) as LinearLayout).GetChildAt(0) as TextView);

        //                // get the divider below the header
        //                var divider = (view as LinearLayout).GetChildAt(1);

        //                // Set the color
        //                textView.TextSize = (float)_ResizedTableView.SectionTitleFontSize;
        //            }
        //            catch (Exception e) {
        //                Console.WriteLine(e.Message);
        //            }
        //        }

        //        return view;
        //    }
        //}