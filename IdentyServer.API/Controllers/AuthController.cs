using IdentityServer.Domain.Queries;
using IdentityServer.Domain.Utils;
using LubyTasks.IdentyServer.Queries.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LubyTasks.IdentyServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LubyTasksQueriesHandler _queriesHandler;

        public AuthController(LubyTasksQueriesHandler queriesHandler)
        {
            _queriesHandler = queriesHandler;
        }

        [HttpPost()]
        public async Task<object> GetToken([FromBody] GetAuthQuery query)
        {
            var result = await _queriesHandler.RunQueryAsync(query);

            if (result.Data.Count() == 1)
            {
                return Ok(Token.GetToken(result.Data.First()));
            }

            return BadRequest();
        }
    }
}