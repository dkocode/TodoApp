using BtaApplication.Todo.Dtos;
using BtaDomain.Common;
using BtaDomain.ToDo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BtaApplication.Todo.Services;

public interface ITodoItemApplicationService
{
    IList<TodoItemDto> GetList();
    IList<SelectListItem> GetTodoPriorityDataSource();
    
    bool IsEditable(TodoItemDto dto);
    bool IsDeletable(TodoItemDto dto);
    
    OperationResult<TodoItem> GetById(int id);
    
    OperationResult Add(TodoItem item);
    OperationResult Edit(TodoItem item);
    OperationResult Delete(int id);
    OperationResult ChangeStatus(int id, TodoStatus status);
}