using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Api.Models;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class ComputerController(ILoggingService logger) : ControllerBase
{
    /// <summary>
    /// Retrieves the name of the computer hosting the application.
    /// </summary>
    /// <remarks>This method returns the machine name of the server where the application is running.  The
    /// machine name is obtained from the <see cref="Environment.MachineName"/> property.</remarks>
    /// <returns>An <see cref="IActionResult"/> containing a <see cref="ComputerInfo"/> object with the computer's name.</returns>
    [HttpGet("name")]
    public IActionResult GetComputerName()
    {
        var name = Environment.MachineName;
        logger.LogInfo($"Retrieved computer name: {name}");

        return Ok(new ComputerInfo { Name = name });
    }
}
