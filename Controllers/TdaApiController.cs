using Microsoft.AspNetCore.Mvc;

namespace TeacherDigitalAgency.Controllers;

[ApiController]
[Route("/api")]
public class TdaApiController: ControllerBase
{
    private readonly ILogger<TdaApiController> _logger;

    public TdaApiController(ILogger<TdaApiController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "api")]
    public Dictionary<string, string> OnGet()
    {
        _logger.LogDebug("Base api endpoint called");
        return new Dictionary<string, string>
        {
            { "secret", "The cake is a lie" }
        };
    }
    
    [HttpPost(Name = "api")]
    public Dictionary<string, string> OnPost()
    {
        _logger.LogDebug("Base api post endpoint called");
        return new Dictionary<string, string>
        {
            { "secret", "The cake is a lie" }
        };
    }
}