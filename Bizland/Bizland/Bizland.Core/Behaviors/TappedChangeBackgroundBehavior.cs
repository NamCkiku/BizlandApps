using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core.Behaviors
{
    public class TappedChangeBackgroundBehavior : Behavior<Frame>
    {

        public static readonly BindableProperty BackgroundProperty =
          BindableProperty.Create(propertyName: nameof(Background),
              returnType: typeof(Color),
              declaringType: typeof(TappedChangeBackgroundBehavior),
              defaultValue: Color.White);

        public Color Background
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        protected override void OnAttachedTo(Frame bindable)
        {
            base.OnAttachedTo(bindable);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            bindable.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var frame = (Frame)sender;
            if (frame != null)
            {
                if(frame.BackgroundColor == Color.White)
                {
                    frame.BackgroundColor = Background;
                }
                else
                {
                    frame.BackgroundColor = Color.White;
                }

                await frame.ScaleTo(0.9, 50, Easing.Linear);
                await Task.Delay(100);
                await frame.ScaleTo(1, 50, Easing.Linear);
            }
        }

        protected override void OnDetachingFrom(Frame bindable)
        {
            base.OnDetachingFrom(bindable);

            var tapGestureRecognizer = new TapGestureRecognizer();
            bindable.GestureRecognizers.Remove(tapGestureRecognizer);
        }

    }
}
