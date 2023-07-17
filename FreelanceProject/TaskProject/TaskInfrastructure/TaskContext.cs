using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task = TaskDomain.Task;

namespace TaskInfrastructure
{
    public class TaskContext: DbContext
    {
            public DbSet<Task> Tasks => Set<Task>();
            public TaskContext(DbContextOptions options) : base(options)
        {
                Database.Migrate();
            }
    }
}