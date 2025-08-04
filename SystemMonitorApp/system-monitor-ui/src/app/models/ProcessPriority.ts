export enum ProcessPriority
{
    //
    // Summary:
    //     Specifies that the process has no special scheduling needs.
    Normal = 32,
    //
    // Summary:
    //     Specifies that the threads of this process run only when the system is idle,
    //     such as a screen saver. The threads of the process are preempted by the threads
    //     of any process running in a higher priority class. This priority class is inherited
    //     by child processes.
    Idle = 64,
    //
    // Summary:
    //     Specifies that the process performs time-critical tasks that must be executed
    //     immediately, such as the Task List dialog, which must respond quickly when called
    //     by the user, regardless of the load on the operating system. The threads of the
    //     process preempt the threads of normal or idle priority class processes.
    //     Use extreme care when specifying High for the process's priority class, because
    //     a high priority class application can use nearly all available processor time.
    High = 128,
    //
    // Summary:
    //     Specifies that the process has the highest possible priority.
    //     The threads of a process with RealTime priority preempt the threads of all other
    //     processes, including operating system processes performing important tasks. Thus,
    //     a RealTime priority process that executes for more than a very brief interval
    //     can cause disk caches not to flush or cause the mouse to be unresponsive.
    RealTime = 256,
    //
    // Summary:
    //     Specifies that the process has priority above Idle but below Normal.
    BelowNormal = 16384,
    //
    // Summary:
    //     Specifies that the process has priority higher than Normal but lower than High.
    AboveNormal = 32768
}