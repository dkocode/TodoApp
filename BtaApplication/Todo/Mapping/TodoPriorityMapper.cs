using BtaDomain.ToDo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BtaApplication.Todo.Mapping
{
    public static class TodoPriorityMapper
    {
        public static SelectListItem ToSelectListItem(this TodoPriority priority)
        {
            return new SelectListItem(priority.ToString(), ((int)priority).ToString());
        }
        
    }
}