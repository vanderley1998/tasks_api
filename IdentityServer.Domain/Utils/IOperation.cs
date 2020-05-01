using IdentityServer.Domain;
using System.Threading.Tasks;

namespace IdentyServer.Domain.Utils
{
    public interface IOperation<T>
    {
        public Task<OperationResult<T>> GetError(IdentityServerHandler handler);
        public Task<OperationResult<T>> ExecuteOperationAsync(IdentityServerHandler handler);
    }
}