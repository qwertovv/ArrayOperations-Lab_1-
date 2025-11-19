using Xunit;
using System;

namespace ArrayOperations.Tests
{
    public class AdditionalOperationTests
    {
        // Дополнительные тесты для OperationViewModel
        public class OperationViewModelAdditionalTests
        {
            private readonly OperationViewModel _viewModel;

            public OperationViewModelAdditionalTests()
            {
                _viewModel = new OperationViewModel();
            }

            [Fact]
            public void OperationViewModel_ParseInputArray_WithVariousFormats_ParsesCorrectly()
            {
                // Arrange & Act & Assert - различные форматы ввода

                // Формат с пробелами
                _viewModel.InputArray = "1, 2, 3";
                var result1 = ParseInputArrayPrivate(_viewModel);
                Assert.Equal(new[] { 1, 2, 3 }, result1);

                // Формат без пробелов
                _viewModel.InputArray = "1,2,3";
                var result2 = ParseInputArrayPrivate(_viewModel);
                Assert.Equal(new[] { 1, 2, 3 }, result2);

                // Формат с лишними пробелами
                _viewModel.InputArray = "  1 ,  2  ,  3  ";
                var result3 = ParseInputArrayPrivate(_viewModel);
                Assert.Equal(new[] { 1, 2, 3 }, result3);

                // Формат с отрицательными числами
                _viewModel.InputArray = "-1, -2, 3";
                var result4 = ParseInputArrayPrivate(_viewModel);
                Assert.Equal(new[] { -1, -2, 3 }, result4);

                // Пустая строка
                _viewModel.InputArray = "";
                var result5 = ParseInputArrayPrivate(_viewModel);
                Assert.Empty(result5);

                // Только пробелы
                _viewModel.InputArray = "   ";
                var result6 = ParseInputArrayPrivate(_viewModel);
                Assert.Empty(result6);
            }

            [Fact]
            public void OperationViewModel_CurrentOperationChange_UpdatesPreconditionAndStatus()
            {
                // Arrange
                var sortOperation = new SortOperation();
                var sumOperation = new SumOperation();

                // Act
                _viewModel.CurrentOperation = sortOperation;
                var precondition1 = _viewModel.PreconditionMet;
                var status1 = _viewModel.StatusMessage;

                _viewModel.CurrentOperation = sumOperation;
                var precondition2 = _viewModel.PreconditionMet;
                var status2 = _viewModel.StatusMessage;

                // Assert
                Assert.True(precondition1); // Для начального значения "1, 2, 3" предусловие должно выполняться
                Assert.Contains("выполнены", status1);
                Assert.True(precondition2);
                Assert.Contains("выполнены", status2);
            }

            [Fact]
            public void OperationViewModel_ExecuteOperation_MultipleCaches_WorksCorrectly()
            {
                // Arrange
                _viewModel.CurrentOperation = new SumOperation();
                _viewModel.InputArray = "1,2,3";

                // Act - многократное выполнение
                _viewModel.ExecuteOperation();
                var result1 = _viewModel.Result;
                var postcondition1 = _viewModel.PostconditionMet;

                _viewModel.InputArray = "4,5,6";
                _viewModel.ExecuteOperation();
                var result2 = _viewModel.Result;
                var postcondition2 = _viewModel.PostconditionMet;

                // Assert
                Assert.Equal("[6]", result1);
                Assert.True(postcondition1);
                Assert.Equal("[15]", result2);
                Assert.True(postcondition2);
            }

            [Fact]
            public void OperationViewModel_ValidatePreconditions_WithInvalidCharacters_ShowsError()
            {
                // Arrange
                _viewModel.CurrentOperation = new MaxOperation();

                // Act
                _viewModel.InputArray = "1, abc, 3"; // Невалидные символы
                var precondition = _viewModel.PreconditionMet;
                var status = _viewModel.StatusMessage;

                // Assert
                Assert.False(precondition);
                Assert.Contains("Неверный формат массива", status);
            }

            [Fact]
            public void OperationViewModel_ValidatePreconditions_WithNullOperation_ReturnsFalse()
            {
                // Arrange
                _viewModel.CurrentOperation = null;

                // Act
                _viewModel.InputArray = "1,2,3";
                var precondition = _viewModel.PreconditionMet;

                // Assert
                Assert.False(precondition);
            }

            // Вспомогательный метод для доступа к приватному методу ParseInputArray через рефлексию
            private int[] ParseInputArrayPrivate(OperationViewModel viewModel)
            {
                var method = typeof(OperationViewModel).GetMethod("ParseInputArray",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (method != null)
                {
                    return (int[])method.Invoke(viewModel, null);
                }

                throw new InvalidOperationException("Метод ParseInputArray не найден");
            }
        }

        // Дополнительные тесты для операций с граничными значениями
        public class OperationsEdgeCasesTests
        {
            [Fact]
            public void SortOperation_Execute_WithAlreadySortedArray_ReturnsSameArray()
            {
                // Arrange
                var operation = new SortOperation();
                var array = new[] { 1, 2, 3, 4, 5 };

                // Act
                var (result, success) = operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(array, result);
            }

            [Fact]
            public void SortOperation_Execute_WithSingleElement_ReturnsSameArray()
            {
                // Arrange
                var operation = new SortOperation();
                var array = new[] { 42 };

                // Act
                var (result, success) = operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(array, result);
            }

            [Fact]
            public void MaxOperation_Execute_WithAllSameElements_ReturnsThatElement()
            {
                // Arrange
                var operation = new MaxOperation();
                var array = new[] { 5, 5, 5, 5 };

                // Act
                var (result, success) = operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Single(result);
                Assert.Equal(5, result[0]);
            }

            

            [Fact]
            public void SumOperation_Execute_WithZeroes_ReturnsZero()
            {
                // Arrange
                var operation = new SumOperation();
                var array = new[] { 0, 0, 0, 0 };

                // Act
                var (result, success) = operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Single(result);
                Assert.Equal(0, result[0]);
            }
        }

        // Тесты для контрактов операций
        public class OperationContractTests
        {
            [Fact]
            public void SortOperation_GetContract_ContainsAllRequiredSections()
            {
                // Arrange
                var operation = new SortOperation();

                // Act
                var contract = operation.GetContract();

                // Assert
                Assert.False(string.IsNullOrEmpty(contract.Precondition));
                Assert.False(string.IsNullOrEmpty(contract.Postcondition));
                Assert.False(string.IsNullOrEmpty(contract.Effects));
                Assert.False(string.IsNullOrEmpty(contract.ValidExample));
                Assert.False(string.IsNullOrEmpty(contract.InvalidExample));
            }

            
        }

        // Интеграционные тесты
        public class IntegrationTests
        {
            [Fact]
            public void MainViewModel_OperationFlow_CompletesSuccessfully()
            {
                // Arrange
                var mainViewModel = new MainViewModel();
                mainViewModel.SelectedOperation = new SortOperation();
                mainViewModel.OperationViewModel.InputArray = "3,1,2";

                // Act
                mainViewModel.OperationViewModel.ExecuteOperation();

                // Assert
                Assert.Equal("[1, 2, 3]", mainViewModel.OperationViewModel.Result);
                Assert.True(mainViewModel.OperationViewModel.PreconditionMet);
                Assert.True(mainViewModel.OperationViewModel.PostconditionMet);
                Assert.Contains("успешно", mainViewModel.OperationViewModel.StatusMessage);
            }

            [Fact]
            public void MainViewModel_SwitchOperations_MaintainsCorrectState()
            {
                // Arrange
                var mainViewModel = new MainViewModel();

                // Act - переключаемся между операциями
                mainViewModel.SelectedOperation = new SortOperation();
                var operation1 = mainViewModel.OperationViewModel.CurrentOperation;

                mainViewModel.SelectedOperation = new MaxOperation();
                var operation2 = mainViewModel.OperationViewModel.CurrentOperation;

                // Assert
                Assert.IsType<SortOperation>(operation1);
                Assert.IsType<MaxOperation>(operation2);
                Assert.NotEqual(operation1, operation2);
            }
        }
    }
}