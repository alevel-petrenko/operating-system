using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace SystemMonitor.Api.Services;

public class ProcessEnvironmentReader : IEnvironmentService
{
    public Dictionary<string, string> GetEnvironmentVariablesForProcess(int processId)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        using var processHandle = OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.VirtualMemoryRead, false, processId);
        if (processHandle.IsInvalid)
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Cannot open process");

        PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
        int returnLength = 0;
        int status = NtQueryInformationProcess(processHandle, 0, ref pbi, Marshal.SizeOf(pbi), ref returnLength);
        if (status != 0)
            throw new Win32Exception(status, "NtQueryInformationProcess failed");

        PEB peb = ReadStructFromProcessMemory<PEB>(processHandle, pbi.PebBaseAddress);

        RTL_USER_PROCESS_PARAMETERS rupp = ReadStructFromProcessMemory<RTL_USER_PROCESS_PARAMETERS>(processHandle, peb.ProcessParameters);

        // Виділяємо місце для даних оточення
        int envSize = rupp.EnvironmentSize.ToInt32();
        byte[] envData = new byte[envSize];

        if (!ReadProcessMemory(processHandle, rupp.Environment, envData, envData.Length, out _))
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Cannot read Environment");

        // Парсимо UTF-16 рядки
        string envBlock = Encoding.Unicode.GetString(envData);
        string[] vars = envBlock.Split('\0', StringSplitOptions.RemoveEmptyEntries);

        foreach (var v in vars)
        {
            int idx = v.IndexOf('=');
            if (idx > 0)
            {
                string key = v.Substring(0, idx);
                string value = v.Substring(idx + 1);
                result[key] = value;
            }
        }

        return result;
    }

    private static T ReadStructFromProcessMemory<T>(SafeProcessHandle hProcess, IntPtr address) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        byte[] buffer = new byte[size];
        if (!ReadProcessMemory(hProcess, address, buffer, size, out _))
            throw new Win32Exception(Marshal.GetLastWin32Error(), $"Cannot read memory at address {address}");

        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try
        {
            return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
        }
        finally
        {
            handle.Free();
        }
    }

    #region WinAPI Structures

    [StructLayout(LayoutKind.Sequential)]
    struct PROCESS_BASIC_INFORMATION
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr Reserved3;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct PEB
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved1;
        public byte BeingDebugged;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Reserved2;
        public IntPtr Reserved3_0;
        public IntPtr Reserved3_1;
        public IntPtr Reserved3_2;
        public IntPtr Ldr;
        public IntPtr ProcessParameters;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RTL_USER_PROCESS_PARAMETERS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Reserved1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public IntPtr[] Reserved2;
        public IntPtr Environment;
        public IntPtr EnvironmentSize;
    }

    [Flags]
    enum ProcessAccessFlags : uint
    {
        QueryInformation = 0x0400,
        VirtualMemoryRead = 0x0010
    }

    #endregion

    #region WinAPI Imports

    [DllImport("ntdll.dll")]
    private static extern int NtQueryInformationProcess(
        SafeProcessHandle processHandle,
        int processInformationClass,
        ref PROCESS_BASIC_INFORMATION processInformation,
        int processInformationLength,
        ref int returnLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern SafeProcessHandle OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(SafeProcessHandle hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    #endregion
}
