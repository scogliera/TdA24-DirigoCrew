using Microsoft.AspNetCore.Mvc;
using TeacherDigitalAgency.Models;

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
    
    [HttpGet("/api")]
    public ActionResult<Dictionary<string, string>> OnGet()
    {
        _logger.LogDebug("Base api endpoint called");
        
        var result = new Dictionary<string, string>
        {
            { "secret", "The cake is a lie" }
        };

        return Ok(result);
    }
    
    [HttpPost("/api")]
    public ActionResult<Dictionary<string, string>> OnPost()
    {
        _logger.LogDebug("Base api post endpoint called");
        
        var result = new Dictionary<string, string>
        {
            { "secret", "The cake is a lie" }
        };

        return Ok(result);
    }

    [HttpPost("/prikladvseho/{vek:int}")]
    public ActionResult<IEnumerable<string>> PrikladVseho([FromRoute] int vek, [FromBody] Lecturer lecturer)
    {
        var anotherLecturer = new Lecturer
        {
            FirstName = "Jozef",
            LastName = "Novák",
            TitleBefore = "Ing."
        };
        
        var informationList = new List<string>();
        
        informationList.Add($"{lecturer.FirstName} má {vek} let.");
        informationList.Add($"Jeho ID je {lecturer.Uuid}");
        informationList.Add($"{lecturer.FirstName} má kamaráda {anotherLecturer.FirstName} {anotherLecturer.LastName}");
        
        return Ok(informationList);
    }
}