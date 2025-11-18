using System.Diagnostics;
using System.Linq;

namespace ArrayOperations
{
    public class SortOperation : ArrayOperation
    {
        public SortOperation() : base("Сортировка", "Сортировка массива по возрастанию") { }

        public override bool CheckPreconditions(int[] array, params object[] parameters)
        {
            return array != null && array.Length > 0;
        }

        public override (int[] result, bool success) Execute(int[] array, params object[] parameters)
        {
            Guard.Requires(CheckPreconditions(array), "Массив не должен быть пустым");

            var originalMultiSet = array.ToArray();
            var result = array.OrderBy(x => x).ToArray();

            Debug.Assert(IsSorted(result), "Массив должен быть отсортирован");
            Debug.Assert(IsMultisetPreserved(originalMultiSet, result), "Мультимножество должно быть сохранено");

            return (result, true);
        }

        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }

        private bool IsMultisetPreserved(int[] original, int[] sorted)
        {
            return original.OrderBy(x => x).SequenceEqual(sorted);
        }

        public override OperationContract GetContract()
        {
            return new OperationContract
            {
                Precondition = "Массив не пуст",
                Postcondition = "Массив упорядочен по неубыванию, мультимножество элементов сохранено",
                Effects = "Исходный массив не изменяется, возвращается новый отсортированный массив",
                ValidExample = "Вход: [3, 1, 2] → Выход: [1, 2, 3]",
                InvalidExample = "Вход: [] → Исключение: массив пуст"
            };
        }
    }
}