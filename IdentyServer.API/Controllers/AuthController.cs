using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Utils;
using LubyTasks.LubyTasks.Queries.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<object> GetToken([FromBody] GetAuthQuery query)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(query);
            var token = Authentication.GetToken(result.Data.FirstOrDefault());
            return result.GetResult(token);
        }
    }
}