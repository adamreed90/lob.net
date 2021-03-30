using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobChecks
    {
        Task<CheckResponse> CreateAsync(CheckRequest check, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<CheckResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ListResponse<CheckResponse>> ListAsync(CheckFilter filter = null, CancellationToken cancellationToken = default);
        IAsyncEnumerable<CheckResponse> ListObjectsAsync(CheckFilter filter = null, CancellationToken cancellationToken = default);
    }
}
