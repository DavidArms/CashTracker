using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Widget;
using CashTracker.Controls;
using CashTracker.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FancyEntry), typeof(DroidFancyEntryRenderer))]
namespace CashTracker.Droid.CustomRenderers
{
    /// <summary>
    /// Custom entry renderer for Android
    /// </summary>
    /// <remarks>Code from https://somostechies.com/custom-entry/ </remarks>
    public class DroidFancyEntryRenderer : EntryRenderer
    {
        List<string> OverrideProperties = new List<string>
            {
                FancyEntry.CornerRadiusProperty.PropertyName,
                FancyEntry.BorderThicknessProperty.PropertyName,
                FancyEntry.BorderColorProperty.PropertyName
            };

        public DroidFancyEntryRenderer(Context context) : base(context)
        { }

        public FancyEntry ElementV2 => Element as FancyEntry;
        private FormsEditText EditTextControl { get; set; }

        protected override FormsEditText CreateNativeControl()
        {
            EditTextControl = base.CreateNativeControl();
            UpdateBackground(EditTextControl);
            return EditTextControl;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (OverrideProperties.Contains(e.PropertyName) && EditTextControl != null)
                UpdateBackground(EditTextControl);
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

        /// <summary>
        /// Overriding so that we can set the cursor color
        /// </summary>
        /// <remarks>see reference here: https://forums.xamarin.com/discussion/138361/change-cursor-color-in-entry </remarks>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
            IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

            JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.custom_cursor);
        }
    }
}