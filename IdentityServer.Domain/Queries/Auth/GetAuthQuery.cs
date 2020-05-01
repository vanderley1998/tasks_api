using Dapper;
using IdentityServer.Domain;
using IdentyServer.Domain;
using IdentyServer.Domain.Queries.ViewModels;
using IdentyServer.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LubyTasks.IdentyServer.Queries.Auth
{
    public class GetAuthQuery : IOperation<CredentialUser>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public async Task<OperationResult<CredentialUser>> ExecuteOperationAsync(IdentityServerHandler handler)
        {
            var sql = @"
                select u.id, u.login, u.password
                from users u
                where u.login=@Login and u.password=@Password
                and u.removed=0
            ";

            var conn = handler.IdentityServerContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<CredentialUser>(sql, new { Login, Password });
            if(result.Count() == 0)
                return new OperationResult<CredentialUser>(EErrorCode.NotFound, result);

            return new OperationResult<CredentialUser>(EErrorCode.None, result);
        }

        public async Task<OperationResult<CredentialUser>> GetError(IdentityServerHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<CredentialUser>(EErrorCode.InvalidParams, $"Parameter {nameof(Login) } is null or empty");

            if (string.IsNullOrWhiteSpace(Password))
                return new OperationResult<CredentialUser>(EErrorCode.InvalidParams, $"Parameter {nameof(Password) } is null or empty");

            return await Task.FromResult<OperationResult<CredentialUser>>(null);
        }
    }
}
