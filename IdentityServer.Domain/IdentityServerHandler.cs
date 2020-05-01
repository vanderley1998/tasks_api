using IdentyServer.Domain.Utils;
using System.Threading.Tasks;

namespace IdentityServer.Domain
{
    public class IdentityServerHandler
    {
        public readonly IdentityServerDbContext IdentityServerContext;

        public IdentityServerHandler(IdentityServerDbContext identityServerDb)
        {
            IdentityServerContext = identityServerDb;
        }

        public async Task<OperationResult<T>> ExecuteAsync<T>(IOperation<T> operation)
        {
            var error = await operation.GetError(this);

            if (error == null)
                return await operation.ExecuteOperationAsync(this);

            return error;
        }
    }
}
