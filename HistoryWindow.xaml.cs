using System.Collections.Generic;
using System.Windows;

namespace AbobaMusic
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }

        public void UpdateHistory(List<string> playlist)
        {
            historyListBox.ItemsSource = playlist;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
