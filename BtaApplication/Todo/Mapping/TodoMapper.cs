using BtaApplication.Todo.Dtos;
using BtaDomain.ToDo;

namespace BtaApplication.Todo.Mapping
{
    public static class TodoMapper
    {
        public static TodoItemDto ToDto(this TodoItem entity)
        {
            return new TodoItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Priority = new NameIdDto(entity.Priority, ((TodoPriority)entity.Priority).ToString()),
                Status = new NameIdDto((int)entity.Status, entity.Status.ToString()),
            };
        }

        public static TodoItem ToEntity(this TodoItemDto dto)
        {
            var entity = new TodoItem
            {
                Id = dto.Id,
                Name = dto.Name,
                Priority = dto.Priority.Id
            };

            if (dto.Status != null)
            {
                entity.SetState((TodoStatus)dto.Status.Id);
            }

            return entity;
        }
    }
}