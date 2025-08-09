using Microsoft.AspNetCore.SignalR;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.SignalR;

public class ProcessHub(IProcessService processService) : Hub
{
}
