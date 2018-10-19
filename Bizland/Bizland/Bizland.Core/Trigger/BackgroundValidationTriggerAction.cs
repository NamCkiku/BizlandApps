using System;
using Xamarin.Forms;

namespace Bizland.Core.Trigger
{
    public class BackgroundValidationTriggerAction : TriggerAction<Entry>
    {
        protected override void Invoke(Entry entry)
        {
            double result;
            bool isValid = Double.TryParse(entry.Text, out result);
            entry.PlaceholderColor = isValid ? Color.Default : Color.Red;
        }
    }
}
