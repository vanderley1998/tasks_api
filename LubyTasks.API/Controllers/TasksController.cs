using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Commands;
using LubyTasks.Domain.Queries;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Tasks were listed with success")]
        public async Task<OperationResult<Domain.Queries.ViewModels.Task>> List()
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new ListTasksQuery());
            return result;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Task successfully created")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(OperationResult<>), Description = "Task was not created because the user is not logged")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Post([FromBody]CreateTaskCommand command)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Task successfully removed")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(OperationResult<>), Description = "Task was not removed because was not found")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Delete(int id)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new RemoveTaskCommand() { Id = id });
            return result;
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Task successfully updated")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(OperationResult<>), Description = "Task was not updated because was not found")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> Update(int id, [FromBody]UpdateTaskCommand command)
        {
            command.Id = id;
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [HttpPatch("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Status successfully changed")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(OperationResult<>), Description = "Task was not removed because was not found")]
        public async Task<OperationResult<Domain.Commands.Entities.Task>> ChangeStatus(int id, [FromBody]ChangeStatusTaskCommand command)
        {
            command.Id = id;
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }
    }
}