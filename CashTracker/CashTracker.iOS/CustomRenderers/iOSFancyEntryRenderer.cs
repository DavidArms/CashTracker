using CashTracker.Controls;
using CashTracker.iOS.CustomRenderers;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FancyEntry), typeof(iOSFancyEntryRenderer))]
namespace CashTracker.iOS.CustomRenderers
{
    /// <summary>
    /// Custom entry renderer for iOS
    /// </summary>
    /// <remarks>Code from https://somostechies.com/custom-entry/ </remarks>
    public sealed class iOSFancyEntryRenderer : EntryRendererBase<UITextFieldPadding>
    {
        public FancyEntry ElementV2 => Element as FancyEntry;
        private UITextFieldPadding EditTextControl { get; set; }

        List<string> OverrideProperties = new List<string>
            {
                FancyEntry.CornerRadiusProperty.PropertyName,
                FancyEntry.BorderThicknessProperty.PropertyName,
                FancyEntry.BorderColorProperty.PropertyName
            };

        public iOSFancyEntryRenderer()
        {
            Frame = new RectangleF(0, 20, 320, 40);
        }

        protected override UITextFieldPadding CreateNativeControl()
        {
            EditTextControl = new UITextFieldPadding(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateBackground(EditTextControl);

            return EditTextControl;
        }

        void UpdateBackground(UITextField control)
        {
            if (control == null)
                return;

            control.Layer.CornerRadius = ElementV2.CornerRadius;
            control.Layer.BorderWidth = ElementV2.BorderThickness;
            control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (OverrideProperties.Contains(e.PropertyName) && EditTextControl != null)
                UpdateBackground(EditTextControl);
        }
    }

    public class UITextFieldPadding : UITextField
    {
        public Thickness Padding { get; set; } = new Thickness(5);

        public UITextFieldPadding()
        { }

        public UITextFieldPadding(NSCoder coder) : base(coder) { }
        public UITextFieldPadding(CGRect rect) : base(rect) { }

        public override CGRect TextRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets(
                (float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets(
                (float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets(
                (float)Padding.Top,
                (float)Padding.Left,
                (float)Padding.Bottom,
                (float)Padding.Right);

            return insets.InsetRect(forBounds);
        }
    }
}