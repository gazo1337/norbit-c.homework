using TaskDomain;
using Task = TaskDomain.Task;

namespace TaskProject.Routes
{
    public static class TaskRouter
    {
        public static WebApplication AddTaskRouter(this WebApplication application)
        {
            // Производим группировку логики.
            var taskGroup = application.MapGroup("/api/tasks");

            taskGroup.MapGet(pattern: "/all", handler: GetAllTasks);
            taskGroup.MapGet(pattern: "/{id:long}", handler: GetTaskById);
            taskGroup.MapPost(pattern: "/create", handler: CreateTask);
            taskGroup.MapPut(pattern: "/update", handler: UpdateTask);
            taskGroup.MapDelete(pattern: "/delete/{id:long}", handler: DeleteTask);

            return application;
        }

        private static IResult GetAllTasks(ITaskManager taskManager)
        {
            var tasks = taskManager.GetAll();
            return Results.Ok(tasks);
        }

        private static IResult GetTaskById(long id, ITaskManager taskmanager)
        {
            var task = taskmanager.GetById(id);
            return task is null
                ? Results.NotFound()
                : Results.Ok(task);
        }

        private static IResult CreateTask(Task task, ITaskManager taskManager)
        {
            var createdTask = taskManager.Create(task);
            return Results.Ok(createdTask);
        }

        private static IResult UpdateTask(Task task, ITaskManager taskManager)
        {
            var updatedTask = taskManager.Update(task);
            return updatedTask is null
                ? Results.NotFound()
                : Results.Ok(updatedTask);
        }

        private static IResult DeleteTask(long id, ITaskManager taskManager)
        {
            var deletedTask = taskManager.Delete(id);
            return deletedTask is null
                ? Results.NotFound()
                : Results.Ok(deletedTask);
        }
    }
}
