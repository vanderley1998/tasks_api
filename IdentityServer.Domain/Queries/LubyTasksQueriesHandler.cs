using IdentyServer.Domain;
using IdentyServer.Domain.Utils;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Queries
{
    public class LubyTasksQueriesHandler
    {
        public SqlConnection Database = new SqlConnection("Data Source=localhost; Initial Catalog=luby_tasks; Integrated Security=false; User Id=vand; Password=abc123##;");

        public async Task<QueryResult<T>> RunQueryAsync<T>(IQuery<T> query)
        {
            if (query.IsValid())
                return await query.ExecuteQueryAsync(this);

            return new QueryResult<T>(EErrorCode.InvalidParams, "Invalid params query");
        }
    }
}
