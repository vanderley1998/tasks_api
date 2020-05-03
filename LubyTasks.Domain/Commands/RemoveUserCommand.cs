using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class RemoveUserCommand : IOperation<User>
    {
        public async Task<OperationResult<User>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var user = await handler.LubyTasksContext.Users
                .Where(u => u.Id == handler.CurrentUser.Id)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync();

            user.Remove();

            var result = await handler.LubyTasksContext.SaveChangesAsync();
            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (string.IsNullOrWhiteSpace(handler.CurrentUser.Login))
                return new OperationResult<User>(HttpStatusCode.Unauthorized, $"There's no opened session. Please, get the token and try again");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
