using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain
{
    public interface ITaskManager
    {
        List<Task> GetAll();

        Task? GetById(long id);

        Task Create(Task task);

        Task? Update(Task task);

        Task? Delete(long id);
    }
}
