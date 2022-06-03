using BtaDomain.Common;
using BtaDomain.ToDo;

namespace BtaDomainInterfaces.Todo
{
    public interface ITodoRepository : ICrudRepository<TodoItem, int>
    {
        /// <summary>
        /// Check, if there is persistent record of entity with given name.
        /// </summary>
        bool NameExists(TodoItem item);
        
        /// <summary>
        /// Change entity status.
        /// </summary>
        /// <remarks>
        /// No exception thrown here. If the <see cref="status"/> cannot be applied, method return <see cref="OperationResult"/> with error reason message.
        /// </remarks>
        OperationResult ChangeStatusTo(TodoItem item, TodoStatus status);
    }
}