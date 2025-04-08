
# 📘 Learning Plan: OS Fundamentals & Platform Internals (C#)

## 🔧 1. Operating System Fundamentals

### 📚 Theoretical Topics

- [ ] **Application structure**
  - [ ] Understand .exe/.dll layout, Entry Point, PE format
  - [ ] Explore Windows Registry and environment variables
- [ ] **OS Resources & Handles**
  - [ ] Learn about handles and resource management via WinAPI
- [ ] **User interaction (Event Loop, Messages)**
  - [ ] Understand message loops in WinForms/WPF
  - [ ] Learn about Windows Messages (e.g., WM_COMMAND)
- [ ] **Process and Thread Management**
  - [ ] Thread creation, priorities, context switching
- [ ] **Thread synchronization primitives**
  - [ ] Practice with Mutex, Semaphore, Monitor, lock, EventWaitHandle, Interlocked
- [ ] **I/O (file access & concurrency control)**
  - [ ] FileStream vs File, FileShare, file locks
- [ ] **Memory management**
  - [ ] Stack vs Heap, .NET GC, P/Invoke, VirtualAlloc/VirtualFree

### 💻 Practical Project: Windows System Monitor

**Description:** Build a console app that:
- [ ] Lists active processes and threads
- [ ] Displays environment variables
- [ ] Can stop/restart a process
- [ ] Logs to a file with support for concurrent access

**Technologies:**
- `System.Diagnostics`
- `Environment`
- `Thread`, `Task`, `Mutex`, `Semaphore`
- `FileStream`, `FileShare`
- `P/Invoke`, `VirtualAlloc`