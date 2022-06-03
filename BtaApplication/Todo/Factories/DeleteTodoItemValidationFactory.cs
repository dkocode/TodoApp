using BtaApplication.Common;
using BtaApplication.Todo.Services;
using BtaDomain.ToDo;

namespace BtaApplication.Todo.Factories;

public class DeleteTodoItemValidationFactory : ITodoItemValidationFactory
{
    private readonly ITodoItemValidationService _todoItemValidationService;
    
    public DeleteTodoItemValidationFactory(ITodoItemValidationService todoItemValidationService)
    {
        _todoItemValidationService = todoItemValidationService;
    }
    
    public ValidationResult Validate(TodoItem entity)
    {
        var result = new ValidationResult();
        
        if (!entity.CanDelete())
        {
            result.AddError("Only completed items can be deleted!");
            
            // Not deleteable? Stop validation process!
            return result;
        }

        return result;
    }
}