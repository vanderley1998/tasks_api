using LubyTasks.Domain.Utils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class CreateActionCommand : IOperation<Entities.Action>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public async Task<OperationResult<Entities.Action>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var action = new Entities.Action()
            {
                Title = Title,
                Description = Description,
                UserId = handler.CurrentUser.Id
            };

            handler.LubyTasksContext.Actions.Add(action);
            var result = await handler.LubyTasksContext.SaveChangesAsync();
            if (result == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.NotModified, result);

            return new OperationResult<Entities.Action>(HttpStatusCode.Created, result);

        }

        public async Task<OperationResult<Entities.Action>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (handler.CurrentUser.Id == 0)
                return new OperationResult<Entities.Action>(HttpStatusCode.Unauthorized, $"There's no opened session. Please, get the token and try again");

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
