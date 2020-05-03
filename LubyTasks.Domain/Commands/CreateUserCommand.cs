using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.Domain.Commands
{
    public class CreateUserCommand : IOperation<User>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public async Task<OperationResult<User>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var user = new User
            {
                Name = Name,
                Login = Login,
                Password = Password.GetSHA512()
            };

            handler.LubyTasksContext.Users.Add(user);
            var result = await handler.LubyTasksContext.SaveChangesAsync();
            return new OperationResult<User>(HttpStatusCode.Created, result);
        }

        public async Task<OperationResult<User>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Name) } is required");

            if (Name.Length > Convert.ToInt32(ELimitCaracteres.Sort))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Name) } must have a maximum of {Convert.ToInt32(ELimitCaracteres.Sort)} caracteres");

            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login) } is required");

            if(await handler.LubyTasksContext.Users.AnyAsync(u => u.Login == Login))
                return new OperationResult<User>(HttpStatusCode.Conflict, $"{nameof(Login) } {Login} already exists");

            if (string.IsNullOrWhiteSpace(Password))
                return new OperationResult<User>(HttpStatusCode.BadRequest, $"Parameter {nameof(Password) } is required");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
