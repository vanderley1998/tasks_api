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
        public string Login { get; set; }
        public async Task<OperationResult<User>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var user = await handler.LubyTasksContext.Users
                .Where(u => u.Login == Login)
                .FirstOrDefaultAsync();

            user.Remove();

            var result = await handler.LubyTasksContext.SaveChangesAsync();
            if (result == 0)
                return new OperationResult<User>(HttpStatusCode.NotModified, result);

            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetError(LubyTasksHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login)} is required");

            if (!(await handler.LubyTasksContext.Users.AnyAsync(u => u.Login == Login)))
                return new OperationResult<User>(HttpStatusCode.NotFound, $"User with {nameof(Login)} {Login} was not found");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
