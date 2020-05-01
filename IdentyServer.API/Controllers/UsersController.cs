using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Commands;
using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LubyTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class UsersController : ControllerBase
    {
        private readonly LubyTasksHandler _lubyTasksHandler;

        public UsersController(LubyTasksHandler handler)
        {
            _lubyTasksHandler = handler;
        }

        [HttpPost]
        public async Task<OperationResult<User>> Post([FromBody] CreateUserCommand command)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpPut("{login}")]
        public async Task<OperationResult<User>> Update(string login, [FromBody] UpdateUserCommand command)
        {
            command.Login = login;
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpDelete("{login}")]
        public async Task<OperationResult<User>> Delete(string login)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new RemoveUserCommand { Login = login });
            return result;
        }
    }
}