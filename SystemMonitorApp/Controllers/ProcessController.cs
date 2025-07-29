using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ProcessController(IProcessService processService) : ControllerBase
{
    [HttpGet("all")]
    public IEnumerable<ProcessInfo> GetActiveProcesses()
    {
        return processService.GetActiveProcesses();
    }
}
