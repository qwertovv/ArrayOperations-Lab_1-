using System;

namespace ArrayOperations
{
    public abstract class ArrayOperation
    {
        public string Name { get; }
        public string Description { get; }

        protected ArrayOperation(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract bool CheckPreconditions(int[] array, params object[] parameters);
        public abstract (int[] result, bool success) Execute(int[] array, params object[] parameters);
        public abstract OperationContract GetContract();
    }
}