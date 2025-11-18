using System.Windows;

namespace ArrayOperations
{
    public partial class ContractWindow : Window
    {
        public ContractWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}