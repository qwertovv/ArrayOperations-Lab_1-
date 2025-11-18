using Xunit;
using System;
using System.Linq;

namespace ArrayOperations.Tests
{
    public class ArrayOperationsTests
    {
        // SortOperation Tests
        public class SortOperationTests
        {
            private readonly SortOperation _operation = new SortOperation();

            [Fact]
            public void Sort_CheckPreconditions_WithEmptyArray_ReturnsFalse()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();

                // Act
                bool result = _operation.CheckPreconditions(emptyArray);

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Sort_CheckPreconditions_WithNonEmptyArray_ReturnsTrue()
            {
                // Arrange
                int[] array = { 3, 1, 2 };

                // Act
                bool result = _operation.CheckPreconditions(array);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Sort_Execute_WithValidArray_ReturnsSortedArray()
            {
                // Arrange
                int[] array = { 3, 1, 4, 2 };
                int[] expected = { 1, 2, 3, 4 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Sort_Execute_WithEmptyArray_ThrowsException()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => _operation.Execute(emptyArray));
            }

            [Fact]
            public void Sort_Execute_PreservesMultiset()
            {
                // Arrange
                int[] array = { 3, 1, 2, 1, 3 };
                var expectedSet = new[] { 1, 1, 2, 3, 3 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(expectedSet, result);
            }

            [Fact]
            public void Sort_GetContract_ReturnsValidContract()
            {
                // Act
                var contract = _operation.GetContract();

                // Assert
                Assert.NotNull(contract);
                Assert.Equal("Массив не пуст", contract.Precondition);
                Assert.Contains("упорядочен по неубыванию", contract.Postcondition);
            }
        }

        // MaxOperation Tests
        public class MaxOperationTests
        {
            private readonly MaxOperation _operation = new MaxOperation();

            [Fact]
            public void Max_CheckPreconditions_WithEmptyArray_ReturnsFalse()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();

                // Act
                bool result = _operation.CheckPreconditions(emptyArray);

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Max_CheckPreconditions_WithNonEmptyArray_ReturnsTrue()
            {
                // Arrange
                int[] array = { 1, 2, 3 };

                // Act
                bool result = _operation.CheckPreconditions(array);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Max_Execute_WithValidArray_ReturnsMaxValue()
            {
                // Arrange
                int[] array = { 3, 1, 4, 2 };
                int[] expected = { 4 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Single(result);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Max_Execute_WithNegativeNumbers_ReturnsCorrectMax()
            {
                // Arrange
                int[] array = { -5, -2, -10, -1 };
                int[] expected = { -1 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Max_Execute_WithSingleElement_ReturnsThatElement()
            {
                // Arrange
                int[] array = { 42 };
                int[] expected = { 42 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Max_Execute_WithEmptyArray_ThrowsException()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => _operation.Execute(emptyArray));
            }

            [Fact]
            public void Max_GetContract_ReturnsValidContract()
            {
                // Act
                var contract = _operation.GetContract();

                // Assert
                Assert.NotNull(contract);
                Assert.Equal("Массив не пуст", contract.Precondition);
                Assert.Contains("максимальный элемент", contract.Postcondition);
            }
        }

        // SumOperation Tests
        public class SumOperationTests
        {
            private readonly SumOperation _operation = new SumOperation();

            [Fact]
            public void Sum_CheckPreconditions_WithNullArray_ReturnsFalse()
            {
                // Arrange
                int[] nullArray = null;

                // Act
                bool result = _operation.CheckPreconditions(nullArray);

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Sum_CheckPreconditions_WithEmptyArray_ReturnsTrue()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();

                // Act
                bool result = _operation.CheckPreconditions(emptyArray);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Sum_Execute_WithValidArray_ReturnsSum()
            {
                // Arrange
                int[] array = { 1, 2, 3, 4 };
                int[] expected = { 10 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Single(result);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Sum_Execute_WithEmptyArray_ReturnsZero()
            {
                // Arrange
                int[] emptyArray = Array.Empty<int>();
                int[] expected = { 0 };

                // Act
                var (result, success) = _operation.Execute(emptyArray);

                // Assert
                Assert.True(success);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Sum_Execute_WithNegativeNumbers_ReturnsCorrectSum()
            {
                // Arrange
                int[] array = { -1, -2, -3 };
                int[] expected = { -6 };

                // Act
                var (result, success) = _operation.Execute(array);

                // Assert
                Assert.True(success);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void Sum_Execute_WithNullArray_ThrowsException()
            {
                // Arrange
                int[] nullArray = null;

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => _operation.Execute(nullArray));
            }

            [Fact]
            public void Sum_GetContract_ReturnsValidContract()
            {
                // Act
                var contract = _operation.GetContract();

                // Assert
                Assert.NotNull(contract);
                Assert.Equal("Массив не null", contract.Precondition);
                Assert.Contains("сумма всех элементов", contract.Postcondition);
            }
        }

        // Guard Tests
        public class GuardTests
        {
            [Fact]
            public void Guard_Requires_WithTrueCondition_DoesNotThrow()
            {
                // Arrange
                bool condition = true;

                // Act & Assert
                Exception ex = Record.Exception(() => Guard.Requires(condition, "Test message"));
                Assert.Null(ex);
            }

            [Fact]
            public void Guard_Requires_WithFalseCondition_ThrowsException()
            {
                // Arrange
                bool condition = false;
                string message = "Test precondition failed";

                // Act & Assert
                var ex = Assert.Throws<InvalidOperationException>(() => Guard.Requires(condition, message));
                Assert.Contains(message, ex.Message);
                Assert.Contains("Precondition failed", ex.Message);
            }
        }

        // OperationViewModel Tests
        public class OperationViewModelTests
        {
            [Fact]
            public void OperationViewModel_InputArray_ParsesCorrectly()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                string input = "1, 2, 3, 4";

                // Act
                viewModel.InputArray = input;

                // Assert
                Assert.Equal(input, viewModel.InputArray);
            }

            [Fact]
            public void OperationViewModel_CurrentOperation_ChangesPrecondition()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                var operation = new SortOperation();

                // Act
                viewModel.CurrentOperation = operation;

                // Assert
                Assert.Equal(operation, viewModel.CurrentOperation);
            }

            [Fact]
            public void OperationViewModel_ExecuteOperation_WithValidInput_UpdatesResult()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                viewModel.CurrentOperation = new SumOperation();
                viewModel.InputArray = "1,2,3";

                // Act
                viewModel.ExecuteOperation();

                // Assert
                Assert.Equal("[6]", viewModel.Result);
                Assert.True(viewModel.PostconditionMet);
                Assert.Contains("успешно", viewModel.StatusMessage);
            }

            [Fact]
            public void OperationViewModel_ExecuteOperation_WithInvalidInput_ShowsError()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                viewModel.CurrentOperation = new SortOperation();
                viewModel.InputArray = ""; // Пустой массив для сортировки

                // Act
                viewModel.ExecuteOperation();

                // Assert
                Assert.Equal("Ошибка", viewModel.Result);
                Assert.False(viewModel.PostconditionMet);
                Assert.Contains("Ошибка", viewModel.StatusMessage);
            }

            [Fact]
            public void OperationViewModel_ValidatePreconditions_WithValidArray_SetsPreconditionMet()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                viewModel.CurrentOperation = new MaxOperation();
                viewModel.InputArray = "5, 3, 8";

                // Act - изменение InputArray автоматически вызывает ValidatePreconditions
                // Мы можем вызвать ExecuteOperation чтобы проверить результат

                // Assert
                Assert.True(viewModel.PreconditionMet);
            }

            [Fact]
            public void OperationViewModel_ValidatePreconditions_WithInvalidFormat_SetsPreconditionFalse()
            {
                // Arrange
                var viewModel = new OperationViewModel();
                viewModel.CurrentOperation = new MaxOperation();
                viewModel.InputArray = "1, abc, 3"; // Неверный формат

                // Act - изменение InputArray автоматически вызывает ValidatePreconditions

                // Assert
                Assert.False(viewModel.PreconditionMet);
                Assert.Contains("Неверный формат массива", viewModel.StatusMessage);
            }
        }

        // MainViewModel Tests
        public class MainViewModelTests
        {
            [Fact]
            public void MainViewModel_Constructor_InitializesOperations()
            {
                // Act
                var viewModel = new MainViewModel();

                // Assert
                Assert.NotNull(viewModel.Operations);
                Assert.Equal(3, viewModel.Operations.Count);
                Assert.NotNull(viewModel.SelectedOperation);
                Assert.NotNull(viewModel.OperationViewModel);
            }

            [Fact]
            public void MainViewModel_SelectedOperation_UpdatesOperationViewModel()
            {
                // Arrange
                var viewModel = new MainViewModel();
                var newOperation = new SumOperation();

                // Act
                viewModel.SelectedOperation = newOperation;

                // Assert
                Assert.Equal(newOperation, viewModel.OperationViewModel.CurrentOperation);
            }

            [Fact]
            public void MainViewModel_Operations_HaveCorrectTypes()
            {
                // Arrange
                var viewModel = new MainViewModel();

                // Assert
                Assert.Contains(viewModel.Operations, op => op is SortOperation);
                Assert.Contains(viewModel.Operations, op => op is MaxOperation);
                Assert.Contains(viewModel.Operations, op => op is SumOperation);
            }
        }
    }
}