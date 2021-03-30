using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobLetters
    {
        Task<LetterResponse> CreateAsync(LetterRequest letter, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<LetterResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ListResponse<LetterResponse>> ListAsync(LetterFilter filter = null, CancellationToken cancellationToken = default);
        IAsyncEnumerable<LetterResponse> ListObjectsAsync(LetterFilter filter = null, CancellationToken cancellationToken = default);
    }
}
