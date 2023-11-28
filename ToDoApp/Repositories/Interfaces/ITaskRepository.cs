using ToDoApp.Models;

namespace ToDoApp.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Todo>> GetAllTasks();
        Task<IEnumerable<Todo>> GetCompletedTasks();
        Task<IEnumerable<Todo>> GetNotCompletedTasks();
        Task<Todo> GetTaskById(int id);
        void Add(Todo task);
        void Update(Todo task);
        void Delete(Todo task);
    }
}
