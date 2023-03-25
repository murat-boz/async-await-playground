using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AwaitablePattern
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new (10000);

            Console.WriteLine("Starting...");

            var sw = Stopwatch.StartNew();

            await cts.Token;

            Console.WriteLine($"Done - Elapsed time: {sw.Elapsed}");
        }
    }

    public static class CancellationTokenExtensions
    {
        public static TaskAwaiter GetAwaiter(this CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask.GetAwaiter();
            }

            TaskCompletionSource taskCompletionSource = new();

            cancellationToken.Register(() => taskCompletionSource.SetResult());

            return taskCompletionSource.Task.GetAwaiter();
        }
    }
}