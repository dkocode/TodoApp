using BtaApplication.Common;
using BtaApplication.Todo.Dtos;
using BtaApplication.Todo.Factories;
using BtaApplication.Todo.Mapping;
using BtaDomain.Common;
using BtaDomain.ToDo;
using BtaDomainInterfaces.Todo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BtaApplication.Todo.Services
{
    public class TodoItemApplicationService : ITodoItemApplicationService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoItemValidationService _validationService;

        public TodoItemApplicationService(ITodoRepository todoRepository, ITodoItemValidationService validationService)
        {
            _todoRepository = todoRepository;
            _validationService = validationService;
        }

        public IList<TodoItemDto> GetList()
        {
            var data = _todoRepository.GetAll();

            var list = new List<TodoItemDto>(data.Count);

            foreach (TodoItem entity in data)
            {
                list.Add(entity.ToDto());
            }

            return list;
        }

        public IList<SelectListItem> GetTodoPriorityDataSource()
        {
            return new List<SelectListItem>
            {
                TodoPriority.Low.ToSelectListItem(),
                TodoPriority.Medium.ToSelectListItem(),
                TodoPriority.High.ToSelectListItem(),
            };
        }

        public bool IsEditable(TodoItemDto dto)
        {
            return dto.ToEntity().CanEdit();
        }

        public bool IsDeletable(TodoItemDto dto)
        {
            return dto.ToEntity().CanDelete();
        }

        public OperationResult<TodoItem> GetById(int id)
        {
            TodoItem? entity = _todoRepository.GetById(id);

            if (entity == null)
            {
                return OperationResult<TodoItem>.Error($"Todo item id '{id}' doesn't exists.");
            }

            return OperationResult<TodoItem>.Ok(entity);
        }

        public OperationResult Add(TodoItem item)
        {
            var validation = ValidateOperationFor(item, OperationType.Add);
            if (!validation.IsValid)
            {
                return OperationResult.Error(validation.ToErrorMessage());
            }

            _todoRepository.Insert(item);

            return OperationResult.Ok;
        }

        public OperationResult Edit(TodoItem item)
        {
            TodoItem? entity = _todoRepository.GetById(item.Id);

            if (entity == null)
            {
                return OperationResult.Error($"Edit operation failed! Todo item id '{item.Id}' doesn't exists.");
            }

            var validation = ValidateOperationFor(item, OperationType.Edit);
            if (!validation.IsValid)
            {
                return OperationResult.Error(validation.ToErrorMessage());
            }

            var result = _todoRepository.Update(item);

            if (!result)
            {
                return OperationResult.Error("Todo item update failed!");
            }

            return OperationResult.Ok;
        }

        public OperationResult Delete(int id)
        {
            TodoItem? entity = _todoRepository.GetById(id);

            if (entity == null)
            {
                return OperationResult.Error($"Delete operation failed! Todo item id '{id}' doesn't exists.");
            }

            var validation = ValidateOperationFor(entity, OperationType.Delete);
            if (!validation.IsValid)
            {
                return OperationResult.Error(validation.ToErrorMessage());
            }

            var result = _todoRepository.Remove(entity);

            if (!result)
            {
                return OperationResult.Error("Todo item update failed!");
            }

            return OperationResult.Ok;
        }

        public OperationResult ChangeStatus(int id, TodoStatus status)
        {
            TodoItem? entity = _todoRepository.GetById(id);

            if (entity == null)
            {
                return OperationResult.Error($"ChangeStatus operation failed! Todo item id '{id}' doesn't exists.");
            }

            return _todoRepository.ChangeStatusTo(entity, status);
        }

        private ValidationResult ValidateOperationFor(TodoItem entity, OperationType operationType)
        {
            ITodoItemValidationFactory validationFactory;
            
            switch (operationType)
            {
                case OperationType.Add:
                    validationFactory = new AddTodoItemValidationFactory(_validationService);
                    break;

                case OperationType.Edit:
                    validationFactory = new EditTodoItemValidationFactory(_validationService);
                    break;

                case OperationType.Delete:
                    validationFactory = new DeleteTodoItemValidationFactory(_validationService);
                    break;
                    
                default:
                    throw new NotImplementedException();
            }

            return validationFactory.Validate(entity); 
        }
    }
}