using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Utils;
using LubyTasks.LubyTasks.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class AuthController : ControllerBase
    {
        private readonly LubyTasksHandler _lubyTasksHandler;

        public AuthController(LubyTasksHandler handler)
        {
            _lubyTasksHandler = handler;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Token generated with success")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(OperationResult<>), Description = "Token was not generated because the user credentials are wrong")]
        public async Task<object> GetToken([FromBody] GetAuthQuery query)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(query);
            var token = Authentication.GetToken(result.Data.FirstOrDefault());
            return result.GetTokenResult(token);
        }
    }
}