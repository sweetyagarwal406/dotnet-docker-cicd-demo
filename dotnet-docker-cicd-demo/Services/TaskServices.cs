using dotnet_docker_cicd_demo.Models;

namespace dotnet_docker_cicd_demo.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public IEnumerable<TaskItem> GetAll() => _tasks;

        public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public TaskItem Create(TaskItem task)
        {
            task.Id = _nextId++;
            task.CreatedAt = DateTime.UtcNow;
            _tasks.Add(task);
            return task;
        }

        public bool Update(int id, TaskItem updated)
        {
            var existing = GetById(id);
            if (existing is null) return false;

            existing.Title = updated.Title;
            existing.IsCompleted = updated.IsCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            var task = GetById(id);
            if (task is null) return false;

            _tasks.Remove(task);
            return true;
        }
    }
}