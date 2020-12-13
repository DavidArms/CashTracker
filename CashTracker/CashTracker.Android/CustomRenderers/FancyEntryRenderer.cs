using Android.Content;
using Android.Graphics.Drawables;
using CashTracker.Controls;
using CashTracker.Droid.CustomRenderers;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FancyEntry), typeof(FancyEntryRenderer))]
namespace CashTracker.Droid.CustomRenderers
{
    /// <summary>
    /// Custom entry renderer for Android
    /// </summary>
    /// <remarks>Code from https://somostechies.com/custom-entry/ </remarks>
    public class FancyEntryRenderer : EntryRenderer
    {
        public FancyEntryRenderer(Context context) : base(context)
        { }

        public FancyEntry ElementV2 => Element as FancyEntry;

        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var overrideProperties = new List<string>
            {
                FancyEntry.CornerRadiusProperty.PropertyName,
                FancyEntry.BorderThicknessProperty.PropertyName,
                FancyEntry.BorderColorProperty.PropertyName
            };

            if (overrideProperties.Contains(e.PropertyName))
                UpdateBackground();

            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());

            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
    }
}