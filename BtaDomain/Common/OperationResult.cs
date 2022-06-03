namespace BtaDomain.Common
{
    public class OperationResult : BaseOperationResult
    {
        public OperationResult(bool result)
        : base(result)
        {
        }

        public OperationResult(string errorMessage)
        : base(errorMessage)
        {
        }
        
        public static OperationResult Ok => new (true);
        public static OperationResult Error(string message) => new (message);
    }

    public class OperationResult<T> : BaseOperationResult where T : class
    {
        public T? Data { get; }
        
        public OperationResult(T? data)
        : base(data != null)
        {
            Data = data;
        }

        public OperationResult(string errorMessage)
        : base(errorMessage)
        {
        }
        
        public static OperationResult<T> Ok(T data) => new (data);
        public static OperationResult<T> Error(string message) => new (message);
    }
}