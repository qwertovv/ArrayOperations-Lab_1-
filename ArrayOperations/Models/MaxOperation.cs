using System.Diagnostics;
using System.Linq;

namespace ArrayOperations
{
    public class MaxOperation : ArrayOperation
    {
        public MaxOperation() : base("Максимум", "Нахождение максимального элемента") { }

        public override bool CheckPreconditions(int[] array, params object[] parameters)
        {
            return array != null && array.Length > 0;
        }

        public override (int[] result, bool success) Execute(int[] array, params object[] parameters)
        {
            Guard.Requires(CheckPreconditions(array), "Массив не должен быть пустым");

            var max = array.Max();
            var result = new[] { max };

            Debug.Assert(result.Length == 1, "Должен возвращаться один элемент");
            Debug.Assert(result[0] == array.Max(), "Должен возвращаться максимальный элемент");

            return (result, true);
        }

        public override OperationContract GetContract()
        {
            return new OperationContract
            {
                Precondition = "Массив не пуст",
                Postcondition = "Возвращается максимальный элемент массива",
                Effects = "Исходный массив не изменяется",
                ValidExample = "Вход: [3, 1, 5, 2] → Выход: [5]",
                InvalidExample = "Вход: [] → Исключение: массив пуст"
            };
        }
    }
}