using BtaDomain.Common;
using BtaDomain.ToDo;

namespace BtaApplication.Todo.Services;

public interface ITodoItemValidationService
{
    OperationResult ValidateName(TodoItem entity);
    OperationResult ValidatePriority(TodoItem entity);
}