using System.ComponentModel.DataAnnotations;

namespace BtaApplication.Todo.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NameIdDto Priority { get; set; }
        public NameIdDto Status { get; set; }
    }

    public class TodoPriorityItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}