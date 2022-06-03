using System.ComponentModel.DataAnnotations;
using BtaApplication.Todo.Dtos;
using BtaDomain.ToDo;

namespace BtaPresentationWeb.Models.Todo
{
    public class TodoItemAddViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Range((int)TodoPriority.Low, (int)TodoPriority.High)]
        public int PriorityId { get; set; }

        public TodoItemDto ToDto()
        {
            return new TodoItemDto()
            {
                Name = Name,
                Priority = new NameIdDto(PriorityId, ((TodoPriority)PriorityId).ToString())
            };
        }
    }
}