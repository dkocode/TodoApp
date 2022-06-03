using BtaDomain.Common;
using BtaDomain.ToDo;
using BtaDomainInterfaces.Todo;

namespace BtaInfrastructure.Todo
{
    internal class TodoRepository : ITodoRepository
    {
        private TodoInMemoryStorage _storage = TodoInMemoryStorage.Instance;

        private int _nextId;

        private int GetNextId()
        {
            _nextId++;
            
            return _nextId;
        }

        private IList<TodoItem> _list => _storage.List;
        
        public void Insert(TodoItem entity)
        {
            entity.Id = GetNextId();
            
            _list.Add(entity);
        }

        public IList<TodoItem> GetAll()
        {
            return _list;
        }
        
        /// <remarks>
        /// Doesn't update the status! If you want to update the status, please use <see cref="ChangeStatusTo"/> function.
        /// </remarks>
        public bool Update(TodoItem item)
        {
            TodoItem? entity = _list.FirstOrDefault(x => x.Id == item.Id);

            if (entity == null)
            {
                return false;
            }

            entity.Name = item.Name;
            entity.Priority = item.Priority;
            
            // Status cannot be modified here due the security reasons

            return true;
        }

        public bool Remove(TodoItem item)
        {
            TodoItem? entity = GetById(item.Id);

            if (entity == null)
            {
                return false;
            }

            return _list.Remove(entity);
        }

        public TodoItem? GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            
            return _list.SingleOrDefault(x => x.Id == id);
        }

        public bool NameExists(TodoItem item)
        {
            return _list.Where(x => x.Id != item.Id).Any(x => x.Name == item.Name);
        }

        public OperationResult ChangeStatusTo(TodoItem item, TodoStatus status)
        {
            TodoItem? entity = GetById(item.Id);

            if (entity == null)
            {
                return OperationResult.Error($"Todo item with Id = {item.Id} doesn't exist in data storage!");
            }

            entity.SetState(status);
            
            return OperationResult.Ok;
        }
    }
}