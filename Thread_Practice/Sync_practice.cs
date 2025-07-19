namespace Thread_Practice;

public class Sync_practice
{
    static readonly Mutex mutex = new();
    static readonly SemaphoreSlim semaphore = new(2);
    static readonly AutoResetEvent signal = new(false);

    public static async Task Start()
    {
        var counter = new SharedCounter();
        var timer = new System.Diagnostics.Stopwatch();

        Console.WriteLine("--- lock ---");
        timer.Start();
        await RunParallel(() => counter.IncrementWithLock());
        timer.Stop();
        Console.WriteLine($"Counter: {counter.Count}. {FormatTime(timer.Elapsed)}\n");

        Console.WriteLine("--- Monitor ---");
        counter = new SharedCounter();
        timer.Restart();
        await RunParallel(() => counter.IncrementWithMonitor());
        timer.Stop();
        Console.WriteLine($"Counter: {counter.Count}. {FormatTime(timer.Elapsed)}\n");

        Console.WriteLine("--- Interlocked ---");
        counter = new SharedCounter();
        timer.Restart();
        await RunParallel(() => counter.IncrementWithInterlocked());
        timer.Stop();
        Console.WriteLine($"Counter: {counter.Count}. {FormatTime(timer.Elapsed)}\n");

        Console.WriteLine("--- Mutex ---");
        counter = new SharedCounter();
        timer.Restart();
        await RunParallel(() =>
        {
            mutex.WaitOne();
            counter.IncrementWithLock();
            mutex.ReleaseMutex();
        });
        timer.Stop();
        Console.WriteLine($"Counter: {counter.Count}. {FormatTime(timer.Elapsed)}\n");

        Console.WriteLine("--- Semaphore ---");
        timer.Restart();
        await RunWithSemaphore();
        timer.Stop();
        Console.WriteLine($"--- Semaphore END {FormatTime(timer.Elapsed)} ---\n");

        Console.WriteLine("--- AutoResetEvent ---");
        timer.Restart();
        Thread t = new(() =>
        {
            Console.WriteLine("[Worker] Waiting for signal...");
            signal.WaitOne();
            Console.WriteLine("[Worker] Received signal!");
        });
        t.Start();
        Thread.Sleep(1000);
        signal.Set();
        t.Join();
        timer.Stop();
        Console.WriteLine($"--- AutoResetEvent END {FormatTime(timer.Elapsed)} ---\n");
    }

    static async Task RunParallel(Action action)
    {
        var tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            tasks[i] = Task.Run(action);
        }
        await Task.WhenAll(tasks);
    }

    static async Task RunWithSemaphore()
    {
        async Task DoWork(int id)
        {
            await semaphore.WaitAsync();
            try
            {
                Console.WriteLine($"Thread {id} entered");
                await Task.Delay(1000);
                Console.WriteLine($"Thread {id} exiting");
            }
            finally
            {
                semaphore.Release();
            }
        }

        var tasks = new Task[5];
        for (int i = 0; i < 5; i++)
        {
            tasks[i] = DoWork(i);
        }
        await Task.WhenAll(tasks);
    }

    private static string FormatTime(TimeSpan time) =>
        $"Time: {time.Minutes}m {time.Seconds}s {time.Milliseconds}ms {time.Nanoseconds}ns";
}
