using BtaApplication.Common;
using BtaApplication.Todo.Services;
using BtaDomain.ToDo;

namespace BtaApplication.Todo.Factories;

public class EditTodoItemValidationFactory : ITodoItemValidationFactory
{
    private readonly ITodoItemValidationService _todoItemValidationService;
    
    public EditTodoItemValidationFactory(ITodoItemValidationService todoItemValidationService)
    {
        _todoItemValidationService = todoItemValidationService;
    }
    
    public ValidationResult Validate(TodoItem entity)
    {
        var result = new ValidationResult();
        
        if (!entity.CanEdit())
        {
            result.AddError("Item is not editable!");
            
            // Not editable? Stop validation process!
            return result;
        }
        
        // Other validation rules

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