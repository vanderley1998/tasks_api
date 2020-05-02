using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class UpdateActionCommand : IOperation<Entities.Action>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public async Task<OperationResult<Entities.Action>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var action = await handler.LubyTasksContext.Actions
                .FirstOrDefaultAsync(a => a.Id == Id);

            action.SetData(Title, Description);

            var result = await handler.LubyTasksContext.SaveChangesAsync();
            if (result == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.NotModified, result);

            return new OperationResult<Entities.Action>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<Entities.Action>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (Id == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.BadRequest, $"Parameter {nameof(Id)} is required");

            if (!(await handler.LubyTasksContext.Actions.AnyAsync(a => a.Id == Id)))
                return new OperationResult<Entities.Action>(HttpStatusCode.NotFound, $"Action with {nameof(Id)} {Id} was not found");

            if (string.IsNullOrWhiteSpace(Title))
                return new OperationResult<Entities.Action>(HttpStatusCode.BadRequest, $"Parameter {nameof(Title)} is required");

            if (Title.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<Entities.Action>(HttpStatusCode.BadRequest, $"Parameter {nameof(Title)} must have a maximum of {Convert.ToInt32(ELimitCaracteres.Sort)} characters");

            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > Convert.ToInt32(ELimitCaracteres.Long))
                return new OperationResult<Entities.Action>(HttpStatusCode.BadRequest, $"Parameter {nameof(Description)} must have a maximum of {Convert.ToInt32(ELimitCaracteres.Long)} characters");

            return await Task.FromResult<OperationResult<Entities.Action>>(null);
        }
    }
}
