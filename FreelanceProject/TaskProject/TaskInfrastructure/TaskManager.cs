using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain;
using Task = TaskDomain.Task;

namespace TaskInfrastructure
{
    public class TaskManager: ITaskManager
    {
        private readonly TaskContext _context;

        public TaskManager(TaskContext context)
        {
            _context = context;
        }

        public List<Task> GetAll() 
        {
            return _context.Tasks.ToList();
        }

        public Task? GetById(long id)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        public Task Create(Task task)
        {
            var entry = _context.Add(task);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Task? Delete(long id)
        {
            var existingTask = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (existingTask is null)
            {
                return null;
            }

            var entry = _context.Remove(existingTask);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Task? Update(Task task)
        {
            var existingTask = _context.Tasks.FirstOrDefault(x => x.Id == task.Id);
            if (existingTask is null)
            {
                return null;
            }

            existingTask.taskName = task.taskName;
            existingTask.devName = task.devName;
            existingTask.taskDescript = task.taskDescript;
            existingTask.compl = task.compl;
            existingTask.level = task.level;
            existingTask.price = task.price;
            existingTask.complete = task.complete;

            var entry = _context.Update(task);
            _context.SaveChanges();
            return entry.Entity;
        }
    }
}
