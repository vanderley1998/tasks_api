using LubyTasks.Domain.Utils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class CreateTaskCommand : IOperation<Entities.Task>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public async Task<OperationResult<Entities.Task>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var task = new Entities.Task()
            {
                Title = Title,
                Description = Description,
                UserId = handler.CurrentUser.Id
            };

            handler.LubyTasksContext.Tasks.Add(task);
            var result = await handler.LubyTasksContext.SaveChangesAsync();
            return new OperationResult<Entities.Task>(HttpStatusCode.Created, result);
        }

        public async Task<OperationResult<Entities.Task>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (handler.CurrentUser.Id == 0)
                return new OperationResult<Entities.Task>(HttpStatusCode.Unauthorized, $"There's no opened session. Please, get the token and try again");

            if (string.IsNullOrWhiteSpace(Title))
                return new OperationResult<Entities.Task>(HttpStatusCode.BadRequest, $"Parameter {nameof(Title)} is required");

            if (Title.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<Entities.Task>(HttpStatusCode.BadRequest, $"Parameter {nameof(Title)} must have a maximum of {Convert.ToInt32(ELimitCaracteres.Sort)} characters");

            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > Convert.ToInt32(ELimitCaracteres.Long))
                return new OperationResult<Entities.Task>(HttpStatusCode.BadRequest, $"Parameter {nameof(Description)} must have a maximum of {Convert.ToInt32(ELimitCaracteres.Long)} characters");

            return await Task.FromResult<OperationResult<Entities.Task>>(null);
        }
    }
}
