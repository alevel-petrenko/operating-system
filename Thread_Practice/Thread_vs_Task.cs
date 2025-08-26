using System.Diagnostics;

namespace Thread_Practice;

public class Thread_vs_Task
{
    public void RunWithThreads()
    {
        Utility.PrintWithTime("== Start RunWithThreads ==");
        List<Thread> threads = [];
        for (int i = 0; i < 10; i++)
        {
            var t = new Thread(() => HeavyComputation(i));
            threads.Add(t);
            t.Start();
        }
        foreach (var t in threads)
            t.Join();
    }

    public async Task RunWithTasks()
    {
        Utility.PrintWithTime("== Start RunWithTasks ==");
        List<Task> tasks = [];
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => HeavyComputation(i)));
        }
        await Task.WhenAll(tasks);
    }

    private void HeavyComputation(int index)
    {
        var sw = Stopwatch.StartNew();
        long result = 0;
        for (int i = 0; i < 500_000_000; i++)
            result += i % 3;

        Console.WriteLine($"[{index}] Done in {sw.ElapsedMilliseconds} ms");
    }
}
