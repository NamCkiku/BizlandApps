﻿using Xamarin.Forms;

namespace Bizland.CustomControl
{
    public class FloatingActionButton : Button
    {
        public static BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(FloatingActionButton), Color.Accent);
        public Color ButtonColor
        {
            get
            {
                return (Color)GetValue(ButtonColorProperty);
            }
            set
            {
                SetValue(ButtonColorProperty, value);
            }
        }
    }
}
