using Dapper;
using IdentityServer.Domain.Queries;
using IdentyServer.Domain;
using IdentyServer.Domain.Queries.ViewModels;
using IdentyServer.Domain.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace LubyTasks.IdentyServer.Queries.Auth
{
    public class GetAuthQuery : IQuery<CredentialUser>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public async Task<QueryResult<CredentialUser>> ExecuteQueryAsync(LubyTasksQueriesHandler handlerQuery)
        {
            var sql = @"
                select u.id, u.login, u.password
                from users u
                where u.login=@Login and u.password=@Password
            ";

            using (var connection = handlerQuery.Database)
            {
                var result = await connection.QueryAsync<CredentialUser>(sql, new { Login, Password });
                return new QueryResult<CredentialUser>(EErrorCode.None, result.ToList());
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                return false;
            return true;
        }
    }
}
