using IdentityServer.Domain;
using IdentityServer.Domain.Queries;
using System.Threading.Tasks;

namespace IdentyServer.Domain.Utils
{
    public interface IQuery<T>
    {
        public bool IsValid();
        public Task<QueryResult<T>> ExecuteQueryAsync(LubyTasksQueriesHandler handlerQuery);
    }
}