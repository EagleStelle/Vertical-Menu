using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace DashboardTesting
{
    public sealed partial class CustomButtonControl : UserControl
    {
        public CustomButtonControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CustomButtonControl), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomButtonControl), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(object), typeof(CustomButtonControl), new PropertyMetadata(null));

        public new object Tag
        {
            get => GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public new event RoutedEventHandler Click;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Forward the click event to the parent
            Click?.Invoke(this, e);
        }

        // Expose FontWeight for the TextBlock
        public FontWeight TextFontWeight
        {
            get => (FontWeight)GetValue(TextFontWeightProperty);
            set => SetValue(TextFontWeightProperty, value);
        }

        public static readonly DependencyProperty TextFontWeightProperty =
            DependencyProperty.Register(nameof(TextFontWeight), typeof(FontWeight), typeof(CustomButtonControl), new PropertyMetadata(FontWeights.Normal));

        // Expose BorderThickness for selection indication
        public Thickness SelectionBorderThickness
        {
            get => (Thickness)GetValue(SelectionBorderThicknessProperty);
            set => SetValue(SelectionBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty SelectionBorderThicknessProperty =
            DependencyProperty.Register(nameof(SelectionBorderThickness), typeof(Thickness), typeof(CustomButtonControl), new PropertyMetadata(new Thickness(0)));

        // Expose BorderBrush for selection indication
        public Brush SelectionBorderBrush
        {
            get => (Brush)GetValue(SelectionBorderBrushProperty);
            set => SetValue(SelectionBorderBrushProperty, value);
        }

        public static readonly DependencyProperty SelectionBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectionBorderBrush), typeof(Brush), typeof(CustomButtonControl), new PropertyMetadata(null));
    }
}
