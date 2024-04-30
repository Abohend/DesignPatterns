using System.Diagnostics.Metrics;

namespace Singleton
{
	/*
	 Interesting Note:
	        Thread Safety: A singleton implementation needs to be thread-safe to ensure that 
	         only one instance is created across multiple threads, 
			whereas a static class is inherently thread-safe because it has no instance-specific data.

			Singleton Class only looded in memory when needed "Static class don't" => this is called "lazy initialization"
	 */
	internal class Program
	{
		static void Main(string[] args)
		{
			var task1 = Task.Run(() => {
				Counter counter1 = Counter.GetInstance();
				counter1.count++;
				Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}, count from counter1 = {counter1.count}");
			});

			var task2 = Task.Run(() => {
				Counter counter2 = Counter.GetInstance();
				counter2.count++;
				Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}, count from counter2 = {counter2.count}");
			});

			task1.Wait();
			task2.Wait();
		}
	}

	public class Counter
	{
		private Counter() { }
		private static Counter? Instance = null;
		public int count = 0;
		private static Object locker = new();
		public static Counter GetInstance()
		{
			var result = Instance; // for optimization instead of accessing the Instance from the main memory each time "Complier did that for thread safety"
			if (result == null ) // Optimization technique called "double check locking"
			{
				lock (locker)
				{
					result = Instance;
					if (result == null)
						result = Instance = new Counter();
				}
			}
			return result;
		}
	}
}
