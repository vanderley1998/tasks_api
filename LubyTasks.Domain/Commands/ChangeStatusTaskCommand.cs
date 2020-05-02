using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class ChangeStatusTaskCommand : IOperation<Entities.Task>
    {
        public int Id { get; set; }
        public bool? Concluded { get; set; }

        public async Task<OperationResult<Entities.Task>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var task = await handler.LubyTasksContext.Tasks
                .FirstOrDefaultAsync(t => t.Id == Id);

            task.Concluded = Concluded.Value;
            
            var result = await handler.LubyTasksContext.SaveChangesAsync();
            return new OperationResult<Entities.Task>(HttpStatusCode.OK, result);

        }

        public async Task<OperationResult<Entities.Task>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (Id == 0)
                return new OperationResult<Entities.Task>(HttpStatusCode.BadRequest, $"Parameter {nameof(Id)} is required");

            if (!Concluded.HasValue)
                return new OperationResult<Entities.Task>(HttpStatusCode.BadRequest, $"Parameter {nameof(Concluded)} is required");

            if (!(await handler.LubyTasksContext.Tasks.AnyAsync(a => a.Id == Id)))
                return new OperationResult<Entities.Task>(HttpStatusCode.NotFound, $"Task with {nameof(Id)} {Id} was not found");

            return await Task.FromResult<OperationResult<Entities.Task>>(null);
        }
    }
}
