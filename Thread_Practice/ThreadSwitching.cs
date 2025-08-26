namespace Thread_Practice;

internal class ThreadSwitching
{
    Thread thread1 = new(() =>
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"[Thread 1] Tick {i}");
            Thread.Sleep(500);
        }
    });

    Thread thread2 = new(() =>
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"[Thread 2] Tick {i}");
            Thread.Sleep(500);
        }
    });

    public void RunBasic()
    {
        thread1.Start();
        thread2.Start();
    }

    Thread highPriorityThread = new(() =>
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"[HIGH] Tick {i}");
            // Без Sleep, щоб тримати CPU
        }
    })
    {
        Priority = ThreadPriority.Highest,
        Name = "HighPriority"
    };

    Thread normalThread = new(() =>
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"[NORMAL] Tick {i}");
            Thread.Sleep(10); // CPU віддається іншим
        }
    })
    {
        Priority = ThreadPriority.Normal,
        Name = "NormalPriority"
    };

    Thread lowPriorityThread = new(() =>
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"[LOW] Tick {i}");
            Thread.Sleep(10);
        }
    })
    {
        Priority = ThreadPriority.Lowest,
        Name = "LowPriority"
    };

    public void Run()
    {
        Console.WriteLine("== Start Threads ==");
        highPriorityThread.Start();
        normalThread.Start();
        lowPriorityThread.Start();

        highPriorityThread.Join();
        normalThread.Join();
        lowPriorityThread.Join();
        Console.WriteLine("== All Done ==");
    }
}
