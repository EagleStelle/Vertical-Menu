using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;

namespace DashboardTesting
{
    public sealed partial class MainWindow : Window
    {
        private readonly Dictionary<string, StackPanel> _panelMap;
        private CustomButtonControl _previousButton;  // Change type to CustomButtonControl

        private readonly Dictionary<CustomButtonControl, string> _buttonTextMap = new();
        private bool _isMenuCollapsed = false;  // Track the collapsed state

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

            // Initialize button text map for all buttons
            if (this.Content is DependencyObject content)
            {
                InitializeButtonTextMap(content);
            }

            // Attach HamburgerButton click event
            HamburgerButton.Click += ToggleMenu;
        }

        private void InitializeButtonTextMap(DependencyObject parent)
        {
            // Recursively find all CustomButtonControls and store their original text
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is CustomButtonControl customButton)
                {
                    if (!_buttonTextMap.ContainsKey(customButton))
                    {
                        _buttonTextMap[customButton] = customButton.Text; // Save original text
                    }
                }
                else
                {
                    // Recursively search child elements
                    InitializeButtonTextMap(child);
                }
            }
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            _isMenuCollapsed = !_isMenuCollapsed;

            foreach (var button in _buttonTextMap.Keys)
            {
                // Toggle button text visibility
                button.Text = _isMenuCollapsed ? string.Empty : _buttonTextMap[button];

                // Toggle image margin
                if (button.FindName("ButtonImage") is Image buttonImage)
                {
                    buttonImage.Margin = _isMenuCollapsed ? new Thickness(0) : new Thickness(0, 0, 10, 0);
                }
            }
        }

        private void TogglePanel(object sender, RoutedEventArgs e)
        {
            if (sender is CustomButtonControl clickedControl && clickedControl.Tag is string panelName && _panelMap.ContainsKey(panelName))
            {
                var panelToShow = _panelMap[panelName];

                if (panelToShow.Visibility == Visibility.Visible)
                {
                    // Hide it and remove the selection indicators
                    panelToShow.Visibility = Visibility.Collapsed;
                    clickedControl.IsSelected = false;
                    _previousButton = null;
                }
                else
                {
                    // Hide all panels
                    foreach (var panel in _panelMap.Values)
                    {
                        panel.Visibility = Visibility.Collapsed;
                    }

                    // Show the selected panel
                    panelToShow.Visibility = Visibility.Visible;

                    // Reset the previously selected button, if any
                    if (_previousButton != null)
                    {
                        _previousButton.IsSelected = false;
                    }

                    // Set the clicked control as selected
                    clickedControl.IsSelected = true;
                    _previousButton = clickedControl;
                }
            }
        }
    }
}
