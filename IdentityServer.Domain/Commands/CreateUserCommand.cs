using IdentityServer.Domain.Utils;
using IdentyServer.Domain;
using IdentyServer.Domain.Commands.Auth.Entities;
using IdentyServer.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Commands
{
    public class CreateUserCommand : IOperation<User>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public async Task<OperationResult<User>> ExecuteOperationAsync(IdentityServerHandler handler)
        {
            var user = new User
            {
                Name = Name,
                Login = Login,
                Password = Password
            };

            handler.IdentityServerContext.Users.Add(user);
            var result = await handler.IdentityServerContext.SaveChangesAsync();
            
            if(result == 0)
                return new OperationResult<User>(EErrorCode.UnsuccessfulOperation, result);

            return new OperationResult<User>(EErrorCode.None, result);

        }

        public async Task<OperationResult<User>> GetError(IdentityServerHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new OperationResult<User>(EErrorCode.InvalidParams, $"Parameter {nameof(Name) } is required");

            if (Name.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<User>(EErrorCode.InvalidParams, $"Parameter {nameof(Name) } must be only {Convert.ToInt32(ELimitCaracteres.Sort)} caracteres");

            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(EErrorCode.InvalidParams, $"Parameter {nameof(Login) } is required");

            if(await handler.IdentityServerContext.Users.AnyAsync(u => u.Login == Login))
                return new OperationResult<User>(EErrorCode.InvalidParams, $"{nameof(Login) } {Login} already exists");

            if (string.IsNullOrWhiteSpace(Password))
                return new OperationResult<User>(EErrorCode.InvalidParams, $"Parameter {nameof(Password) } is required");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
