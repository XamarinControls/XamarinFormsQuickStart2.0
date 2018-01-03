using System;
using System.Collections.Generic;
using System.Text;
using Target.iOS.Renderers;
using Target.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ResizedTableView), typeof(ResizedTableViewRenderer))]
namespace Target.iOS.Renderers
{
    public class ResizedTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            if (e.OldElement != null)
            {
                // Unsubscribe
                
            }
            if (e.NewElement != null)
            {
                // Subscribe
                var tableView = Control as UITableView;
                var resizedTableView = Element as ResizedTableView;
                tableView.WeakDelegate = new CustomTableViewModelRenderer(resizedTableView);
            }
            
        }

        private class CustomTableViewModelRenderer : TableViewModelRenderer
        {
            private readonly ResizedTableView _resizedTableView;
            public CustomTableViewModelRenderer(TableView model) : base(model)
            {
                _resizedTableView = model as ResizedTableView;
            }
            public override UIView GetViewForHeader(UITableView tableView, nint section)
            {
                return new UILabel()
                {
                    Text = TitleForHeader(tableView, section),
                    Font = UIFont.FromName("Helvetica", (System.nfloat)this._resizedTableView.FontSize),
                //font = (System.nfloat)_resizedTableView.FontSize
                    //TextColor = _resizedTableView.GroupHeaderColor.ToUIColor(),
                    //TextAlignment = UITextAlignment.Center
                };
            }
        }
    }
}
