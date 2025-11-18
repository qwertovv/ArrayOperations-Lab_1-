using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArrayOperations
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Заполняем список операций
            OperationsList.ItemsSource = _viewModel.Operations;
            OperationsList.SelectedItem = _viewModel.SelectedOperation;

            // Обработчики событий с разными именами параметров
            OperationsList.SelectionChanged += OperationsList_SelectionChanged;
            InputTextBox.TextChanged += InputTextBox_TextChanged;
            ExecuteButton.Click += ExecuteButton_Click;
            ContractButton.Click += ContractButton_Click;

            // Инициализация UI
            UpdateUI();
        }

        private void OperationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedOperation = OperationsList.SelectedItem as ArrayOperation;
            UpdateUI();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.OperationViewModel.InputArray = InputTextBox.Text;
            UpdatePrecondition();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OperationViewModel.ExecuteOperation();
            UpdatePostcondition();
        }

        private void ContractButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OperationViewModel.ShowContract();
        }

        // ДОБАВЛЕННЫЙ ОБРАБОТЧИК ДЛЯ КНОПКИ СПРАВКИ
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            helpWindow.ShowDialog();
        }

        private void UpdateUI()
        {
            if (_viewModel.SelectedOperation != null)
            {
                DescriptionText.Text = _viewModel.SelectedOperation.Description;
                InputTextBox.Text = _viewModel.OperationViewModel.InputArray;
                UpdatePrecondition();
                UpdatePostcondition();
            }
        }

        private void UpdatePrecondition()
        {
            PreIndicator.Background = _viewModel.OperationViewModel.PreconditionMet
                ? Brushes.Green : Brushes.Red;
            PreText.Text = _viewModel.OperationViewModel.PreconditionMet
                ? "Предусловие: выполнено" : "Предусловие: не выполнено";
            StatusText.Text = _viewModel.OperationViewModel.StatusMessage;
        }

        private void UpdatePostcondition()
        {
            PostIndicator.Background = _viewModel.OperationViewModel.PostconditionMet
                ? Brushes.Green : Brushes.Red;
            PostText.Text = _viewModel.OperationViewModel.PostconditionMet
                ? "Постусловие: выполнено" : "Постусловие: не выполнено";
            ResultText.Text = _viewModel.OperationViewModel.Result;
            StatusText.Text = _viewModel.OperationViewModel.StatusMessage;
        }
    }
}