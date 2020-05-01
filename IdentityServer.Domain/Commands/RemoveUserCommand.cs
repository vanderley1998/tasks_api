using IdentyServer.Domain.Commands.Auth.Entities;
using IdentyServer.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Commands
{
    public class RemoveUserCommand : IOperation<User>
    {
        public string Login { get; set; }
        public async Task<OperationResult<User>> ExecuteOperationAsync(IdentityServerHandler handler)
        {
            var user = await handler.IdentityServerContext.Users
                .Where(u => u.Login == Login)
                .FirstOrDefaultAsync();

            user.Remove();

            var result = await handler.IdentityServerContext.SaveChangesAsync();
            if (result == 0)
                return new OperationResult<User>(HttpStatusCode.NotModified, result);

            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetError(IdentityServerHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login)} is required");

            if (!(await handler.IdentityServerContext.Users.AnyAsync(u => u.Login == Login)))
                return new OperationResult<User>(HttpStatusCode.NotFound, $"User with {nameof(Login)} {Login} was not found");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
