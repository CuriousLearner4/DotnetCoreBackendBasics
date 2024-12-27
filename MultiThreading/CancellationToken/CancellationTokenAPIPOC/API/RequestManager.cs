using System.Collections.Concurrent;

namespace API
{
    public class RequestManager
    {
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> UserOperationTokens = new();
        public CancellationToken RegisterOperation(string userId)
        {
            CancellationTokenSource existingCts;
            UserOperationTokens.TryGetValue(userId, out existingCts!);
            if (existingCts!=null)
            {
                existingCts.Cancel();
                existingCts.Dispose();
            }
            var cts = new CancellationTokenSource();
            UserOperationTokens[userId] = cts;
            return cts.Token;
        }
        public void RemoveOperation(string userId)
        {
            CancellationTokenSource existingCts;
            UserOperationTokens.Remove(userId, out existingCts!);
            if (existingCts!=null)
            {
                existingCts.Dispose();
            }
        }
    }
}
