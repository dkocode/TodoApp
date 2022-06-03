using BtaDomain.Common;

namespace BtaDomain.ToDo
{
    public class TodoItem : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        
        public int Priority { get; set; } = (int)TodoPriority.Low;
        
        // Status is private settable for the security reasons!
        public TodoStatus Status { get; private set; } = TodoStatus.Notstarted;

        public void SetState(TodoStatus targetStatus)
        {
            Status = targetStatus;
        }
        
        public bool CanEdit()
        {
            return true; // Every item status is editable
        }

        public bool CanDelete()
        {
            return Status == TodoStatus.Completed;
        }
    }
}