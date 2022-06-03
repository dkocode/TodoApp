using BtaApplication.Common;
using BtaApplication.Todo.Services;
using BtaDomain.ToDo;

namespace BtaApplication.Todo.Factories;

public class AddTodoItemValidationFactory : ITodoItemValidationFactory
{
    private readonly ITodoItemValidationService _todoItemValidationService;
    
    public AddTodoItemValidationFactory(ITodoItemValidationService todoItemValidationService)
    {
        _todoItemValidationService = todoItemValidationService;
    }
    
    public ValidationResult Validate(TodoItem entity)
    {
        var result = new ValidationResult();

        var validateName = _todoItemValidationService.ValidateName(entity);
        if (!validateName.IsOk)
        {
            result.AddError(validateName.ErrorMessage);
        }

        var validatePriority = _todoItemValidationService.ValidatePriority(entity);
        if (!validatePriority.IsOk)
        {
            result.AddError(validatePriority.ErrorMessage);
        }

        return result;
    }
}