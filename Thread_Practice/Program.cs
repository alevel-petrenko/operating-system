using Thread_Practice;

//
// 1. Example with new thread
//

//Utility.PrintWithTime("== Start ==");

//Thread thread = new Thread(() =>
//{
//    Utility.PrintWithTime("[Helper Thread] Start");
//    Thread.Sleep(5000);
//    Utility.PrintWithTime("[Helper Thread] Done");
//});

////thread.IsBackground = true;
//thread.Start();

//for (int i = 0; i < 10; i++)
//{
//    Utility.PrintWithTime($"[Main] Tick {i}");
//    Thread.Sleep(500);
//}

//Utility.PrintWithTime("== End ==");

//
// 2. Example with task and wait
//

//Utility.PrintWithTime("== Start ==");

//Task task = Task.Run(() =>
//{
//    Utility.PrintWithTime("[Task] Start");
//    Thread.Sleep(3000);
//    Utility.PrintWithTime("[Task] Done");
//});

//task.Wait();

//for (int i = 0; i < 6; i++)
//{
//    Utility.PrintWithTime($"[Main] Tick {i}");
//    Thread.Sleep(500);
//}

//Utility.PrintWithTime("== End ==");

//
// 3. Example with async task (cooperative multitasking)
//

//Utility.PrintWithTime("== Start async task ==");

//var task = AsyncExample();
//for (int i = 0; i < 10; i++)
//{
//    Utility.PrintWithTime($"[Main] Tick {i}");
//    Thread.Sleep(500);
//}
//await task;

//Utility.PrintWithTime("== End ==");

//
// 4. Example with thread priority
//

//Utility.PrintWithTime("== Start thread priority ==");
//var threadPriority = new Thread_Practice.ThreadPriority();
//threadPriority.Run();
//Utility.PrintWithTime("== End ==");

//
// 5. Example with context switching
//

//Utility.PrintWithTime("== Start context switching ==");
//var threadSwitching = new ThreadSwitching();
////threadSwitching.RunBasic();
//threadSwitching.Run();
//Utility.PrintWithTime("== End ==");

//
// 6. Example with thread and task
//

//var lib = new Thread_Task();

//for (int i = 1; i < 16; i++)
//{
//    Console.WriteLine($"[Thread {i}]");
//    var thread = new Thread(lib.ThreadMethod);
//    thread.IsBackground = false;
//    thread.Priority = System.Threading.ThreadPriority.Highest;
//    thread.Start();
//    thread.Join();
//}

//var tasks = new List<Task>();
//for (int i = 0; i < 15; i++)
//{
//    Console.WriteLine($"[Thread {i}]");
//    var task = Task.Run(lib.TaskMethod);
//    tasks.Add(task);
//}

//Task.WaitAll([.. tasks]);

//
// 7. Thread_vs_Task
//

//var threadVsTask = new Thread_vs_Task();
//await threadVsTask.RunWithTasks();
//threadVsTask.RunWithThreads();

//
// 8. Thread synchronization primitives
//

//await Sync_practice.Start();

//
// 9. FileStream vs File
//
FileComparison.Run();

static async Task AsyncExample()
{
    Utility.PrintWithTime("[Async] Start");
    await Task.Delay(3000);
    Utility.PrintWithTime("[Async] Done");
}