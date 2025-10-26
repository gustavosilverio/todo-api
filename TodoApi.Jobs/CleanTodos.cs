using Serilog;
using TodoApi.Jobs.Attributes;
using TodoApi.Jobs.Interfaces;
using TodoApi.Service.Interfaces;

namespace TodoApi.Jobs
{
    [Job("0 0 * * 0", "clean-todos")]
    public class CleanTodos(ITodoService todoService, ILogger logger) : IJob
    {
        public async Task Execute()
        {
            logger.Information("Running job CleanTodos");

            var todos = await todoService.GetAll();

            foreach(var todo in todos)
                await todoService.Delete(todo.Id);

            logger.Information("Finished job CleanTodos, {0} to-dos deleted", todos.Count);
        }
    }
}
