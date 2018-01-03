using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Target.Templates
{
    public class TwoValueHorizontalGrid
    {
        Grid twoValueHorizontalGrid;
        // any time you see parameters with '=' sign it means they are optional
        public Grid Create(double firstComlumnWidth = 0, double secondColumnWidth = 0) { 
            if(twoValueHorizontalGrid == null)
            {
                twoValueHorizontalGrid = new Grid()
                {
                    ColumnSpacing = 0,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                twoValueHorizontalGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
                twoValueHorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                
                if (secondColumnWidth != 0)
                {
                    twoValueHorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(secondColumnWidth) });
                }
                else
                {
                    twoValueHorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
                
            }
            return twoValueHorizontalGrid;
        }
    }
}
