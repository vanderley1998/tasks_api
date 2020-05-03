using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class UpdateUserCommand : IOperation<User>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public async Task<OperationResult<User>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var user = await handler.LubyTasksContext.Users.FirstOrDefaultAsync(u => u.Id == handler.CurrentUser.Id);
            user.SetData(Name, Login);
            var result = await handler.LubyTasksContext.SaveChangesAsync();

            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (string.IsNullOrEmpty(handler.CurrentUser.Login))
                return new OperationResult<User>(HttpStatusCode.Unauthorized, $"There's no opened session. Please, get the token and try again");

            if (!(await handler.LubyTasksContext.Users.AnyAsync(u => u.Password == Password.GetSHA512() && u.Login == handler.CurrentUser.Login)))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"User with wrong {nameof(Password)}");

            if (string.IsNullOrWhiteSpace(Name))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Name) } is required");

            if (Name.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Name) } must be only {Convert.ToInt32(ELimitCaracteres.Sort)} caracteres");

            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login)} is required");

            if (Login.Length > Convert.ToInt32(ELimitCaracteres.Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login) } must be only {Convert.ToInt32(ELimitCaracteres.Login)} caracteres");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
