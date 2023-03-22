namespace AsyncVoid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            BackgroundQueue.FireAndForget(async () =>
            {
                await httpClient.GetAsync("http://test/api/1");
            });

            Console.ReadLine();
        }
    }

    public class BackgroundQueue
    {
        /// <summary>
        /// ❌ Bad approach
        /// This implementation cause to crash the application
        /// </summary>
        /// <param name="action"></param>
        public static void FireAndForget(Action action) 
        { 
            action?.Invoke();
        }

        /// <summary>
        /// ✅ Good approach
        /// </summary>
        /// <param name="action"></param>
        public static void FireAndForget(Func<Task> action)
        {
            action?.Invoke();
        }
    }
}