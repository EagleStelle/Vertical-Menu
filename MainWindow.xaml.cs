using Microsoft.UI.Text;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;

namespace DashboardTesting
{
    public sealed partial class MainWindow : Window
    {
        private readonly Dictionary<string, StackPanel> _panelMap;
        private CustomButtonControl _previousButton;

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
            if (sender is CustomButtonControl clickedControl && clickedControl.Tag is string panelName && _panelMap.ContainsKey(panelName))
            {
                var panelToShow = _panelMap[panelName];

                // Check if the clicked button's panel is already visible
                if (panelToShow.Visibility == Visibility.Visible)
                {
                    // If it's already visible, hide it and remove the selection indicators
                    panelToShow.Visibility = Visibility.Collapsed;
                    ResetButtonStyle(clickedControl);
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

                    // Apply selection style on the clicked control
                    ApplySelectionStyle(clickedControl);
                    _previousButton = clickedControl;  // Update previous button tracking
                }
            }
        }

        private void ApplySelectionStyle(CustomButtonControl button)
        {
            button.Background = new SolidColorBrush(Colors.DarkSlateGray);
            button.TextFontWeight = FontWeights.Bold; // Set text to bold

            // Set the border to indicate selection
            button.SelectionBorderBrush = new SolidColorBrush(Colors.LightGray);
            button.SelectionBorderThickness = new Thickness(5, 0, 0, 0); // Adjust as needed
        }

        private void ResetButtonStyle(CustomButtonControl button)
        {
            button.Background = new SolidColorBrush(Colors.Transparent);
            button.TextFontWeight = FontWeights.Normal; // Reset text weight

            button.SelectionBorderBrush = null;
            button.SelectionBorderThickness = new Thickness(0);
        }
    }
}