namespace API
{
    public class ApiServices
    {
        public async Task ProcessEndpointAAsync(CancellationToken cancellationToken)
        {
            for(int i = 0; i < 10; ++i)
            {
                if (cancellationToken.IsCancellationRequested) return;
                Console.WriteLine($"processing Endpoint A: strp{i + 1}");
                await Task.Delay(1000, cancellationToken);
            }
        }
        public async Task ProcessEndpointBAsync(CancellationToken cancellationToken)
        {
            for(int i = 0; i < 10; ++i)
            {
                if (cancellationToken.IsCancellationRequested) return;
                Console.WriteLine($"processing Endpoint A: strp{i + 1}");
                await Task.Delay(1000, cancellationToken);
            }
        }
        public async Task ProcessEndpointAAsync()
        {
            for(int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"processing Endpoint A: strp{i + 1}");
                await Task.Delay(1000);
            }
        }
        public async Task ProcessEndpointBAsync()
        {
            for(int i = 0; i < 10; ++i)
            {
               
                Console.WriteLine($"processing Endpoint A: strp{i + 1}");
                await Task.Delay(1000);
            }
        }
    }
}
