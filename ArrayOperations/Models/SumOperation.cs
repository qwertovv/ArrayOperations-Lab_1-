using System.Diagnostics;
using System.Linq;

namespace ArrayOperations
{
    public class SumOperation : ArrayOperation
    {
        public SumOperation() : base("Сумма", "Вычисление суммы элементов") { }

        public override bool CheckPreconditions(int[] array, params object[] parameters)
        {
            return array != null;
        }

        public override (int[] result, bool success) Execute(int[] array, params object[] parameters)
        {
            Guard.Requires(CheckPreconditions(array), "Массив не должен быть null");

            var sum = array.Sum();
            var result = new[] { sum };

            Debug.Assert(result.Length == 1, "Должен возвращаться один элемент");
            Debug.Assert(result[0] == array.Sum(), "Должна возвращаться корректная сумма");

            return (result, true);
        }

        public override OperationContract GetContract()
        {
            return new OperationContract
            {
                Precondition = "Массив не null",
                Postcondition = "Возвращается сумма всех элементов массива",
                Effects = "Исходный массив не изменяется",
                ValidExample = "Вход: [1, 2, 3] → Выход: [6]",
                InvalidExample = "Вход: null → Исключение: массив не должен быть null"
            };
        }
    }
}