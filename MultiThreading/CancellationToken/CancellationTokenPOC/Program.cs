namespace CancellationTokenPoc
{
    class Program
    {
        public static void Main()
        {
            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;
            Console.WriteLine("Press enter to cancel");

            ItIsAboutCancellationToken(cancellationToken);
            Console.ReadLine();
            cts.Cancel();
            Console.ReadLine();
        }

        public static async void ItIsAboutCancellationToken(CancellationToken cancellationToken)
        {
            
            Task doSomething = Task.Run(() => DoSomething(cancellationToken), cancellationToken);
            
            try
            {
                await doSomething;
                
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Stopped from doing something");
            }
        }

        public static async Task DoSomething(CancellationToken cancellationToken)
        {
            int counter = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("We are asked to cancel");
                    cancellationToken.ThrowIfCancellationRequested();
                }
                Console.WriteLine($"Doing something...{counter++}");
                await Task.Delay(1000);
            }
        }
    }
}