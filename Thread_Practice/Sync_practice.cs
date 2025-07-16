namespace Thread_Practice;

public class Sync_practice
{
    static readonly Mutex mutex = new();
    static readonly SemaphoreSlim semaphore = new(2);
    static readonly AutoResetEvent signal = new(false);

    public static async Task Start()
    {
        var counter = new SharedCounter();

        Console.WriteLine("--- lock ---");
        await RunParallel(() => counter.IncrementWithLock());
        Console.WriteLine($"Counter: {counter.Count}\n");

        Console.WriteLine("--- Monitor ---");
        counter = new SharedCounter();
        await RunParallel(() => counter.IncrementWithMonitor());
        Console.WriteLine($"Counter: {counter.Count}\n");

        Console.WriteLine("--- Interlocked ---");
        counter = new SharedCounter();
        await RunParallel(() => counter.IncrementWithInterlocked());
        Console.WriteLine($"Counter: {counter.Count}\n");

        Console.WriteLine("--- Mutex ---");
        counter = new SharedCounter();
        await RunParallel(() =>
        {
            mutex.WaitOne();
            counter.IncrementWithLock();
            mutex.ReleaseMutex();
        });
        Console.WriteLine($"Counter: {counter.Count}\n");

        Console.WriteLine("--- Semaphore ---");
        await RunWithSemaphore();

        Console.WriteLine("--- AutoResetEvent ---");
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
}
