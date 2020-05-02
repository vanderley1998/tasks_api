using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class RemoveActionCommand : IOperation<Entities.Action>
    {
        public int Id { get; set; }

        public async Task<OperationResult<Entities.Action>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var action = await handler.LubyTasksContext.Actions
                .FirstOrDefaultAsync(a => a.Id == Id);

            action.Remove();

            var result = await handler.LubyTasksContext.SaveChangesAsync();
            if(result == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.NotModified, result);

            return new OperationResult<Entities.Action>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<Entities.Action>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (Id == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.BadRequest, $"Parameter {nameof(Id)} is required");

            var exists = await handler.LubyTasksContext.Actions.AnyAsync(a => a.Id == Id);
            if (!exists)
                return new OperationResult<Entities.Action>(HttpStatusCode.NotFound, $"Action with {nameof(Id)} = {Id} was not found");

            return await Task.FromResult<OperationResult<Entities.Action>>(null);
        }
    }
}
