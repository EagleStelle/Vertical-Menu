using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DashboardTesting
{
    public sealed partial class MainWindow : Window
    {
        private readonly Dictionary<string, StackPanel> _panelMap;
        private CustomButtonControl _previousButton;  // Change type to CustomButtonControl

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
