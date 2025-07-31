using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Api.Models;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ComputerController : ControllerBase
{
    [HttpGet("name")]
    public IActionResult GetComputerName() => Ok(new ComputerInfo { Name = Environment.MachineName });
}
