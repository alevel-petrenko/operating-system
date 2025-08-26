using System.Diagnostics;
using SystemMonitor.Api.Models;

namespace SystemMonitor.Api.Extensions;

public static class PriorityClassExtensions
{
    public static ProcessPriority ToMyPriority(this ProcessPriorityClass value)
    {
        return value switch
        {
            ProcessPriorityClass.Idle => ProcessPriority.Idle,
            ProcessPriorityClass.BelowNormal => ProcessPriority.BelowNormal,
            ProcessPriorityClass.Normal => ProcessPriority.Normal,
            ProcessPriorityClass.High => ProcessPriority.High,
            ProcessPriorityClass.RealTime => ProcessPriority.RealTime,
            ProcessPriorityClass.AboveNormal => ProcessPriority.AboveNormal,
            _ => throw new NotImplementedException(),
        };
    }

    public static ProcessPriorityClass ToMicrosoftPriority(this ProcessPriority value)
    {
        return value switch
        {
            ProcessPriority.Idle => ProcessPriorityClass.Idle,
            ProcessPriority.BelowNormal => ProcessPriorityClass.BelowNormal,
            ProcessPriority.Normal => ProcessPriorityClass.Normal,
            ProcessPriority.High => ProcessPriorityClass.High,
            ProcessPriority.RealTime => ProcessPriorityClass.RealTime,
            ProcessPriority.AboveNormal => ProcessPriorityClass.AboveNormal,
            _ => throw new NotImplementedException(),
        };
    }
}
