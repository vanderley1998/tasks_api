using IdentityServer.API.Filters;
using IdentityServer.Domain;
using IdentityServer.Domain.Utils;
using LubyTasks.IdentyServer.Queries.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LubyTasks.IdentyServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class AuthController : ControllerBase
    {
        private readonly IdentityServerHandler _identityServerHandler;

        public AuthController(IdentityServerHandler handler)
        {
            _identityServerHandler = handler;
        }

        [HttpPost]
        public async Task<object> GetToken([FromBody] GetAuthQuery query)
        {
            var result = await _identityServerHandler.ExecuteAsync(query);
            var token = Authentication.GetToken(result.Data.FirstOrDefault());
            return result.GetResult(token);
        }
    }
}