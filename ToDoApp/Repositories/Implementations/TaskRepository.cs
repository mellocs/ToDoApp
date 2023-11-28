using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Todo task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void Delete(Todo task)
        {
            _context.Remove(task);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Todo>> GetAllTasks()
        {
            return await _context.Todo.OrderByDescending(t => t.Id).ToListAsync();
        }


        public async Task<IEnumerable<Todo>> GetCompletedTasks()
        {
            return await _context.Todo.Where(t => t.IsCompleted == true).ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetNotCompletedTasks()
        {
            return await _context.Todo.Where(t => t.IsCompleted != true).ToListAsync();
        }

        public async Task<Todo> GetTaskById(int id)
        {
            return await _context.Todo.FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(Todo task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }
    }
}
