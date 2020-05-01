using IdentyServer.Domain;
using IdentyServer.Domain.Commands.Auth.Entities;
using IdentyServer.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Commands
{
    public class RemoveUserCommand : IOperation<User>
    {
        public int Id { get; set; }
        public async Task<OperationResult<User>> ExecuteOperationAsync(IdentityServerHandler handler)
        {
            var user = await handler.IdentityServerContext.Users
                .Where(u => u.Id == Id)
                .FirstOrDefaultAsync();

            user.Removed = true;

            var result = await handler.IdentityServerContext.SaveChangesAsync();
            if (result == 0)
                return new OperationResult<User>(EErrorCode.UnsuccessfulOperation, result);

            return new OperationResult<User>(EErrorCode.None, result);
        }

        public async Task<OperationResult<User>> GetError(IdentityServerHandler handler)
        {
            if (Id == 0)
                return new OperationResult<User>(EErrorCode.InvalidParams, $"Parameter {nameof(Id)} is required");

            var exists = await handler.IdentityServerContext.Users.AnyAsync(u => u.Id == Id);

            if(!exists)
                return new OperationResult<User>(EErrorCode.NotFound, $"User with {nameof(Id)} {Id} was not found");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
