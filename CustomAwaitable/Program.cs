using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace CustomAwaitable
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            //await Delay.Seconds(2);
            //await TimeSpan.FromSeconds(2);
            await 2.Seconds();

            Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        }
    }

    public struct Delay
    {
        public TimeSpan TimeSpan { get; }

        private Delay(TimeSpan timeSpan)
        {
            this.TimeSpan = timeSpan;
        }

        public static Delay Seconds(int seconds)
        {
            return new Delay(TimeSpan.FromSeconds(seconds));
        }
    }

    public static class CustomExtensions
    {
        public static TaskAwaiter GetAwaiter(this Delay delay)
        {
            return Task.Delay(delay.TimeSpan).GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }

        public static TimeSpan Seconds(this int integer)
        {
            return TimeSpan.FromSeconds(integer);
        }
    }
}