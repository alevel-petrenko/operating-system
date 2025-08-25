using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Api.Services;

namespace SystemMonitor.Api.Controllers;

[Route("api/[controller]")]
public class EnvironmentController(ILoggingService logger, IEnvironmentService environmentService) : ControllerBase
{
    /// <summary>
    /// Retrieves the environment variables for a specific process.
    /// </summary>
    /// <param name="processId">The identifier of the process.</param>
    /// <returns>A dictionary containing the environment variables for the specified process.</returns>
    [HttpGet("{processId}")]
    public ActionResult<Dictionary<string, string>> GetEnviromnmentVariables(int processId)
    {
        try
        {
            var envVars = environmentService.GetEnvironmentVariablesForProcess(processId);
            return Ok(envVars);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to retrieve environment variables for process {processId}: {ex.Message}");
            return BadRequest($"Failed to retrieve environment variables for process {processId}.");
        }
    }

    /// <summary>
    /// Retrieves all environment variables.
    /// </summary>
    /// <returns>A dictionary containing the environment variables.</returns>
    [HttpGet("all")]
    public ActionResult<Dictionary<string, string>> GetEnviromnmentVariables()
    {
        try
        {
            var envVars = Environment.GetEnvironmentVariables();
            return Ok(envVars);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to retrieve environment variables: {ex.Message}");
            return BadRequest($"Failed to retrieve environment variables.");
        }
    }
}
