using IdentityServer.API.Filters;
using IdentityServer.Domain;
using IdentityServer.Domain.Commands;
using IdentyServer.Domain.Commands.Auth.Entities;
using IdentyServer.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentyServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class UsersController : ControllerBase
    {
        private readonly IdentityServerHandler _identityServerHandler;

        public UsersController(IdentityServerHandler handler)
        {
            _identityServerHandler = handler;
        }

        [HttpPost]
        public async Task<OperationResult<User>> Post([FromBody] CreateUserCommand command)
        {
            var result = await _identityServerHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpPut("{login}")]
        public async Task<OperationResult<User>> Update(string login, [FromBody] UpdateUserCommand command)
        {
            command.Login = login;
            var result = await _identityServerHandler.ExecuteAsync(command);
            return result;
        }

        [Authorize]
        [HttpDelete("{login}")]
        public async Task<OperationResult<User>> Delete(string login)
        {
            var result = await _identityServerHandler.ExecuteAsync(new RemoveUserCommand { Login = login });
            return result;
        }
    }
}