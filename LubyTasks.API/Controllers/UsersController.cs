using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Commands;
using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Queries;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using UserQuery = LubyTasks.Domain.Queries.ViewModels.User;

namespace LubyTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CurrentUserFilter))]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class UsersController : ControllerBase
    {
        private readonly LubyTasksHandler _lubyTasksHandler;

        public UsersController(LubyTasksHandler handler)
        {
            _lubyTasksHandler = handler;
        }

        [Authorize]
        [HttpGet("session")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Get user successfully")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        public async Task<OperationResult<UserQuery>> Get()
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new GetUserQuery());
            return result;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "User successfully created")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        public async Task<OperationResult<User>> Post([FromBody] CreateUserCommand command)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpPut("{login}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "User successfully updated")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Request parameters are not valid")]
        public async Task<OperationResult<User>> Update(string login, [FromBody] UpdateUserCommand command)
        {
            command.Login = login;
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpDelete("{login}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "User successfully removed")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(OperationResult<>), Description = "Required parameter is missing")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(OperationResult<>), Description = "User was not found")]
        public async Task<OperationResult<User>> Delete(string login)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new RemoveUserCommand { Login = login });
            return result;
        }
    }
}