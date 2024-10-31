using Microsoft.UI.Text;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using Windows.Foundation;

namespace DashboardTesting
{
    public sealed partial class MainWindow : Window
    {
        private readonly Dictionary<string, StackPanel> _panelMap;
        private Button _previousButton;

        public MainWindow()
        {
            this.InitializeComponent();

            // Map each button tag to its corresponding StackPanel
            _panelMap = new Dictionary<string, StackPanel>
            {
                { "Panel1Control", Panel1Control },
                { "Panel2Control", Panel2Control },
                { "Panel3Control", Panel3Control },
                { "Panel4Control", Panel4Control }
            };
        }
        private void ButtonStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            double maxButtonWidth = 0;

            // Loop through each button in the StackPanel to get the widest button's ActualWidth
            foreach (var child in ButtonStackPanel.Children)
            {
                if (child is Button button)
                {
                    double buttonWidth = button.ActualWidth;
                    if (buttonWidth > maxButtonWidth)
                    {
                        maxButtonWidth = buttonWidth;
                    }
                }
            }

            // Set the MinWidth of the StackPanel to 1.3 times the maximum button width
            ButtonStackPanel.MinWidth = maxButtonWidth * 1.15;
        }

        private void TogglePanel(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is string panelName && _panelMap.ContainsKey(panelName))
            {
                var panelToShow = _panelMap[panelName];

                // Check if the clicked button's panel is already visible
                if (panelToShow.Visibility == Visibility.Visible)
                {
                    // If it's already visible, hide it and remove the selection indicators
                    panelToShow.Visibility = Visibility.Collapsed;
                    ResetButtonStyle(clickedButton);
                    _previousButton = null;  // Reset previous button tracking
                }
                else
                {
                    // Hide all panels initially
                    foreach (var panel in _panelMap.Values)
                    {
                        panel.Visibility = Visibility.Collapsed;
                    }

                    // Show the selected panel
                    panelToShow.Visibility = Visibility.Visible;

                    // Reset style on previously selected button if it exists
                    if (_previousButton != null)
                    {
                        ResetButtonStyle(_previousButton);
                    }

                    // Apply selection style on the clicked button
                    ApplySelectionStyle(clickedButton);
                    _previousButton = clickedButton;  // Update previous button tracking
                }
            }
        }

        private void ApplySelectionStyle(Button button)
        {
            // Change background color for selection
            button.Background = new SolidColorBrush(Colors.DarkSlateGray);

            // Find TextBlock within the button and set font to bold
            if (button.Content is StackPanel stackPanel && stackPanel.Children[1] is TextBlock textBlock)
            {
                textBlock.FontWeight = FontWeights.Bold;
            }

            // Add a right-side border to indicate selection
            button.BorderBrush = new SolidColorBrush(Colors.LightGray);
            button.BorderThickness = new Thickness(0, 0, 5, 0);
        }

        private void ResetButtonStyle(Button button)
        {
            // Reset background color
            button.Background = new SolidColorBrush(Colors.Transparent);

            // Reset TextBlock font weight to normal
            if (button.Content is StackPanel stackPanel && stackPanel.Children[1] is TextBlock textBlock)
            {
                textBlock.FontWeight = FontWeights.Normal;
            }

            // Reset border
            button.BorderBrush = null;
            button.BorderThickness = new Thickness(0);
        }
    }
}
