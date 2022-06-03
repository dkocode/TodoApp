using System.ComponentModel.DataAnnotations;
using BtaApplication.Todo.Dtos;
using BtaDomain.ToDo;

namespace BtaPresentationWeb.Models.Todo
{
    public class TodoItemEditViewModel : TodoItemAddViewModel
    {
        [Required(ErrorMessage = "Id is required!")]
        public int Id { get; set; }

        public int StatusId { get; set; }
        
        public string StatusName => ((TodoStatus)StatusId).ToString();

        public TodoItemDto ToDto()
        {
            var dto = base.ToDto();

            dto.Id = Id;
            dto.Status = new NameIdDto(StatusId, ((TodoStatus)StatusId).ToString());
        
            return dto;
        }

        public static TodoItemEditViewModel FromDto(TodoItemDto dto)
        {
            return new TodoItemEditViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                PriorityId = dto.Priority.Id,
                StatusId = dto.Status.Id
            };
        }
    }
}