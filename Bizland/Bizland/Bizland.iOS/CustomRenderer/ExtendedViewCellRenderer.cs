using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as ExtendedViewCell;
            if (view.SelectedBackgroundColor == Color.Transparent)
            {
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            }
            else
            {
                cell.SelectedBackgroundView = new UIView
                {
                    BackgroundColor = view.SelectedBackgroundColor.ToUIColor()
                };
            }

            return cell;
        }

    }
}