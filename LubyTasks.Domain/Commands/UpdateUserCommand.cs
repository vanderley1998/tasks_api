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
            var user = await handler.LubyTasksContext.Users.FirstOrDefaultAsync(u => u.Login == Login);
            user.SetData(Name, Login, Password);
            var result = await handler.LubyTasksContext.SaveChangesAsync();

            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (!string.IsNullOrWhiteSpace(Name) && Name.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Name) } must be only {Convert.ToInt32(ELimitCaracteres.Sort)} caracteres");

            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login)} is required");

            if (!(await handler.LubyTasksContext.Users.AnyAsync(u => u.Login == Login)))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"{nameof(Login) } {Login} does not exists");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
