using Xamarin.Forms;

namespace CashTracker.Controls
{
    public class FancyEntry : Entry
    {
        #region CornerRadius
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FancyEntry), 0);

        /// <summary>
        /// The radius rounding on the corner of the entry
        /// </summary>
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion

        #region BorderThickness
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(FancyEntry), 0);

        /// <summary>
        /// Thickness to apply to the entry's border line
        /// </summary>
        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        #endregion

        #region Padding
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(FancyEntry), new Thickness(5));

        /// <summary>
        /// Padding to apply to the inside of the entry
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion

        #region BorderColor
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FancyEntry), Color.Transparent);

        /// <summary>
        /// Color of the border around the entry
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion

    }
}
