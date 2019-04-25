using System;

namespace Uniswap.Common
{
    public class OperationResult<T>
    {
        private T _result;
        
        public OperationResult()
        {
            IsCompleted = false;
        }

        public OperationResult(T result)
        {
            IsCompleted = true;
            Result = result;
        }

        public T Result
        {
            get
            {
                if (IsCompleted)
                {
                    return _result;
                }
                throw new InvalidOperationException();
            }
            private set => _result = value;
        }

        public bool IsCompleted { get; set; }
    }
}