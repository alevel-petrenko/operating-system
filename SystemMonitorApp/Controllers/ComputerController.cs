using Microsoft.AspNetCore.Mvc;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ComputerController : ControllerBase
{
    [HttpGet("name")]
    public string GetComputerName() => Environment.MachineName;
}
