using System;

namespace ArrayOperations
{
    public static class Guard
    {
        public static void Requires(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException($"Precondition failed: {message}");
        }
    }
}