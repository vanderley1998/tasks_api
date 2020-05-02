using LubyTasks.API.Filters;
using LubyTasks.Domain;
using LubyTasks.Domain.Commands;
using LubyTasks.Domain.Queries;
using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LubyTasks.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CurrentUserFilter))]
    [ServiceFilter(typeof(StatusRequestFilter))]
    public class ActionsController : ControllerBase
    {
        private readonly LubyTasksHandler _lubyTasksHandler;

        public ActionsController(LubyTasksHandler handler)
        {
            _lubyTasksHandler = handler;
        }

        [HttpGet]
        public async Task<OperationResult<Domain.Queries.ViewModels.Action>> List()
        {
            var result = await _lubyTasksHandler.ExecuteAsync(new ListActionsQuery());
            return result;
        }

        [HttpPost]
        public async Task<OperationResult<Domain.Commands.Entities.Action>> Post([FromBody]CreateActionCommand command)
        {
            var result = await _lubyTasksHandler.ExecuteAsync(command);
            return result;
        }
    }
}