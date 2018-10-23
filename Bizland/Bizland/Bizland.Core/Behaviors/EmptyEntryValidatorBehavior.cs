using Xamarin.Forms;

namespace Bizland.Core.Behaviors
{
    public class EmptyEntryValidatorBehavior : Behavior<Entry>
    {
        Entry control;
        string _placeHolder;
        Xamarin.Forms.Color _placeHolderColor;

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            bindable.PropertyChanged += OnPropertyChanged;
            control = bindable;
            _placeHolder = bindable.Placeholder;
            _placeHolderColor = bindable.PlaceholderColor;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                ((Entry)sender).PlaceholderColor = Color.FromHex("#999");
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            bindable.PropertyChanged -= OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (control != null)
            {
                if (control.PlaceholderColor == Color.FromHex("#999"))
                {
                    control.PlaceholderColor = Color.Red;
                }

                else
                {
                    control.PlaceholderColor = _placeHolderColor;
                }

            }
        }
    }
}
