using BtaDomain.Common;

namespace BtaApplication.Common
{
    public class ValidationResult
    {
        private OperationResult<IList<string>> _result = new (new List<string>());
        
        public OperationType OperationType { get; }

        public IList<string> Errors => _result.Data;

        public void AddError(string message)
        {
            _result.Data.Add(message);
        }

        public bool IsValid => !_result.Data.Any();
    }
}