namespace BtaDomain.Common;

public abstract class BaseOperationResult
{
    protected bool _result;
        
    public bool IsOk => _result;
        
    public string ErrorMessage { get; } = string.Empty;

    protected BaseOperationResult(bool result)
    {
        _result = result;
    }

    protected BaseOperationResult(string errorMessage)
    {
        _result = false;
        ErrorMessage = errorMessage;
    }
}