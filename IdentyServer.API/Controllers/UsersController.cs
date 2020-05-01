using IdentityServer.Domain;
using IdentityServer.Domain.Commands;
using IdentyServer.Domain.Commands.Auth.Entities;
using IdentyServer.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentyServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpDelete]
        public async Task<OperationResult<User>> Delete([FromBody] RemoveUserCommand command)
        {
            var result = await _identityServerHandler.ExecuteAsync(command);
            return result;
        }
    }
}