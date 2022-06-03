using BtaDomain.Common;
using BtaDomain.ToDo;
using BtaDomainInterfaces.Todo;

namespace BtaApplication.Todo.Services
{
    public class TodoItemValidationService : ITodoItemValidationService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoItemValidationService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public OperationResult ValidateName(TodoItem entity)
        {
            string name = entity.Name;
            
            if (string.IsNullOrWhiteSpace(name))
            {
                return OperationResult.Error($"Name must be filled!");  
            }
            
            if (_todoRepository.NameExists(entity))
            {
                return OperationResult.Error($"Name '{name}' already exists!");  
            }
            
            return OperationResult.Ok;
        }

        public OperationResult ValidatePriority(TodoItem entity)
        {
            if (!Enum.TryParse(typeof(TodoPriority), entity.Priority.ToString(), out object? priority))
            {
                return OperationResult.Error($"Priority doesn't exists!");   
            }
            
            return OperationResult.Ok;
        }
    }
}