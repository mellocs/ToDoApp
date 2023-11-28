using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Todo> tasks = await _taskRepository.GetAllTasks();
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo updatedTask)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit task");
                return View("Index");
            }

            _taskRepository.Update(updatedTask);

            return Json(new { result = "Success" });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Todo task)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to add task");
                return View("Index");
            }

            task.IsCompleted = false;
            _taskRepository.Add(task);

            return Json(new { title = task.Title });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Todo task = await _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return View("Error");
            }


            _taskRepository.Delete(task);

            return Json(new { result = "Success" });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCheck(int id)
        {
            Todo task = await _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return View("Error");
            }

            if (task.IsCompleted)
            {
                task.IsCompleted = false;
            } 
            else
            {
                task.IsCompleted = true;
            }

            _taskRepository.Update(task);

            return Json(new { result = "Success" });
        }
    }
}
