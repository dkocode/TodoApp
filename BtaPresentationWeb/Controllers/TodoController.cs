using BtaApplication.Todo.Mapping;
using BtaApplication.Todo.Services;
using BtaDomain.Common;
using BtaDomain.ToDo;
using BtaPresentationWeb.Models;
using BtaPresentationWeb.Models.Todo;
using Microsoft.AspNetCore.Mvc;


namespace BtaPresentationWeb.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemApplicationService _todoItemService;
        
        public TodoController(ITodoItemApplicationService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = _todoItemService.GetById(id);

            if (!result.IsOk)
            {
                return View("Error404");
            }

            return View(TodoItemEditViewModel.FromDto(result.Data.ToDto()));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add", new TodoItemAddViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSave(TodoItemAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            OperationResult result = _todoItemService.Add(model.ToDto().ToEntity());

            if (!result.IsOk)
            {
                ModelState.AddModelError("AddFailed", result.ErrorMessage);
                return View("Add", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSave(TodoItemEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", model);
            }
            
            var entity = _todoItemService.GetById(model.Id);

            if (!entity.IsOk)
            {
                return View("Error404");
            }

            OperationResult result = _todoItemService.Edit(model.ToDto().ToEntity());

            if (!result.IsOk)
            {
                ModelState.AddModelError("EditFailed", result.ErrorMessage);
                return View("Detail", model);
            }

            return RedirectToAction("Detail", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entity = _todoItemService.GetById(id);

            if (!entity.IsOk)
            {
                return View("Error404");
            }
            
            OperationResult result = _todoItemService.Delete(id);

            if (!result.IsOk)
            {
                ModelState.AddModelError("DeleteFailed", result.ErrorMessage);
                return View("Detail", TodoItemEditViewModel.FromDto(entity.Data.ToDto()));
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id, int status)
        {
            _ = _todoItemService.ChangeStatus(id, (TodoStatus)status);

            return RedirectToAction("Detail", new { id });
        }
    }
} 