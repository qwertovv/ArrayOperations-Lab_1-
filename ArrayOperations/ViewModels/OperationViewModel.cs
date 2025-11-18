using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ArrayOperations
{
    public class OperationViewModel : INotifyPropertyChanged
    {
        private ArrayOperation _currentOperation;
        private string _inputArray = "1, 2, 3";
        private string _result = string.Empty;
        private bool _preconditionMet;
        private bool _postconditionMet;
        private string _statusMessage = string.Empty;

        public ArrayOperation CurrentOperation
        {
            get => _currentOperation;
            set
            {
                _currentOperation = value;
                OnPropertyChanged();
                ValidatePreconditions();
            }
        }

        public string InputArray
        {
            get => _inputArray;
            set
            {
                _inputArray = value;
                OnPropertyChanged();
                ValidatePreconditions();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool PreconditionMet
        {
            get => _preconditionMet;
            set
            {
                _preconditionMet = value;
                OnPropertyChanged();
            }
        }

        public bool PostconditionMet
        {
            get => _postconditionMet;
            set
            {
                _postconditionMet = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public void ExecuteOperation()
        {
            if (CurrentOperation == null)
                return;

            try
            {
                var array = ParseInputArray();
                var (result, success) = CurrentOperation.Execute(array);

                Result = $"[{string.Join(", ", result)}]";
                PostconditionMet = success;
                StatusMessage = "Операция выполнена успешно";
            }
            catch (Exception ex)
            {
                Result = "Ошибка";
                PostconditionMet = false;
                StatusMessage = $"Ошибка: {ex.Message}";
            }
        }

        public void ShowContract()
        {
            if (CurrentOperation == null)
                return;

            try
            {
                var contract = CurrentOperation.GetContract();
                var contractWindow = new ContractWindow();
                contractWindow.Owner = Application.Current.MainWindow;
                contractWindow.DataContext = contract;
                contractWindow.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при показе контракта: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidatePreconditions()
        {
            if (CurrentOperation == null)
            {
                PreconditionMet = false;
                return;
            }

            try
            {
                var array = ParseInputArray();
                PreconditionMet = CurrentOperation.CheckPreconditions(array);
                StatusMessage = PreconditionMet ? "Предусловия выполнены" : "Предусловия не выполнены";
            }
            catch
            {
                PreconditionMet = false;
                StatusMessage = "Неверный формат массива";
            }
        }

        private int[] ParseInputArray()
        {
            if (string.IsNullOrWhiteSpace(InputArray))
                return Array.Empty<int>();

            return InputArray.Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(int.Parse)
                .ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}