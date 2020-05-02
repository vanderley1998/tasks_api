using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Commands;
using LubyTasks.Domain.Queries;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LubyTasks.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CurrentUserFilter))]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class TasksController : ControllerBase
    {
        private readonly LubyTasksHandler _lubyTasksHandler;

        public TasksController(LubyTasksHandler handler)
        {
            _lubyTasksHandler = handler;
        }

        [HttpGet]
        public async Task<OperationResult<Domain.Queries.ViewModels.Task>> List()
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new ListTasksQuery());
            return result;
        }

        [HttpPost]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Post([FromBody]CreateTaskCommand command)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Delete(int id)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new RemoveTaskCommand() { Id = id});
            return result;
        }
        
        [HttpPut("{id}")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Update(int id, [FromBody]UpdateTaskCommand command)
        {
            command.Id = id;
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }
    }
}