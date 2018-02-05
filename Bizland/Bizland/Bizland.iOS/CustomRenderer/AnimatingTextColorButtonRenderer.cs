﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AnimatingTextColorButton), typeof(AnimatingTextColorButtonRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class AnimatingTextColorButtonRenderer : ButtonRenderer
    {
        private bool _isTextColorAnimationEnabled;

        private UILabel _pulseLabel;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                ((AnimatingTextColorButton)e.NewElement).PropertyChanged += AnimatingTextColorButton_PropertyChanged;

                if (((AnimatingTextColorButton)e.NewElement).IsTextColorAnimating)
                {
                    StartTextColorAnimation();
                }

            }

            if (e.OldElement != null)
            {
                ((AnimatingTextColorButton)e.OldElement).PropertyChanged -= AnimatingTextColorButton_PropertyChanged;
            }
        }

        private void AnimatingTextColorButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AnimatingTextColorButton.IsTextColorAnimating))
            {
                if (((AnimatingTextColorButton)sender).IsTextColorAnimating)
                {
                    StartTextColorAnimation();
                }
                else
                {
                    StopTextColorAnimation();
                }
            }
        }

        private void StartTextColorAnimation()
        {
            _isTextColorAnimationEnabled = true;

            StartPulse();
        }

        private void StopTextColorAnimation()
        {
            _isTextColorAnimationEnabled = false;

            StopPulse();
        }

        private void StartPulse()
        {
            _pulseLabel = new UILabel(new CGRect(0, 0, this.Control.Frame.Width, this.Control.Frame.Height))
            {
                Text = this.Control.Title(UIControlState.Normal),
                TextColor = Color.FromHex("#ff0080").ToUIColor(),
                Font = this.Control.Font,
                Alpha = 0,
                TextAlignment = UITextAlignment.Center,
            };
            _pulseLabel.UserInteractionEnabled = true;

            // adding a touch event to UILabel since it covers UIButton touch area
            UITapGestureRecognizer tapGestureRecognizer = new UITapGestureRecognizer(
                () =>
                {
                    // redirect label touch event to Button touch event
                    this.Control.SendActionForControlEvents(UIControlEvent.TouchUpInside);
                });
            _pulseLabel.AddGestureRecognizer(tapGestureRecognizer);

            Add(_pulseLabel);

            UIView.Animate(1, 0,
            UIViewAnimationOptions.Autoreverse |
            UIViewAnimationOptions.Repeat |
            UIViewAnimationOptions.AllowUserInteraction,
            () =>
            {
                _pulseLabel.Alpha = 1;
            },
            () =>
            {
                return;
            });
        }

        public void StopPulse()
        {
            if (_pulseLabel != null)
            {
                _pulseLabel.RemoveFromSuperview();

                _isTextColorAnimationEnabled = false;
            }
        }

    }
}