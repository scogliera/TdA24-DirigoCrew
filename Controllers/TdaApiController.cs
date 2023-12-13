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


    // zde pravděpodobně začnou problémy --zw

    [HttpPost("/lecturers")]
    public ActionResult<string> AddLecturer()
    {
        /*
            VLOŽIT PŘIDÁVÁNÍ DO DATABÁZE
        */ 
        _logger.LogDebug("Post - Lecturer Add");

        return Ok();
    }

    [HttpGet("/lecturers")]
    public ActionResult<IEnumerable<Lecturer>> GetLecturers()
    {

        /*
            VLOŽIT ČTENÍ Z DATABÁZE (všichni lektoři)
        */
        _logger.LogDebug("Get - Lecturer Get All");

        return Ok();
    }

    [HttpGet("/lecturers/{uuid}")]
    public ActionResult<Lecturer> GetLecturerWithUUID([FromRoute] string uuid, [FromBody] Lecturer lecturer)
    {
        Lecturer Lecturer = lecturer;
        /*
            VLOŽIT ČTENÍ Z DATABÁZE (lektor pomocí uuid)
        */
        _logger.LogDebug("Get - Lecturer Get UUID");

        return Ok(Lecturer);

        // TAKÉ VLOŽIT 404 HANDLING
    }

    [HttpPut("/lecturers/{uuid}")]
    public ActionResult<string> AddLecturerWithUUID([FromRoute] string uuid, [FromBody] Lecturer lecturer)
    {
        Lecturer Lecturer = lecturer;
        /*
            VLOŽIT PŘIDÁVÁNÍ DO DATABÁZE POMOCÍ UUID
            "Pokud existuje pole v DB, ale neexistuje v těle požadavku, zůstane jeho hodnota v DB nezměněna."
        */ 
        _logger.LogDebug("Put - Lecturer Add/Edit With UUID");

        return Ok(Lecturer);

        // TAKÉ VLOŽIT 404 HANDLING
    }

    [HttpDelete("/lecturers/{uuid}")]
    public ActionResult<string> DeleteLecturerWithUUID([FromRoute] string uuid)
    {
        /*
            VLOŽIT DELETOVÁNÍ Z DATABÁZE POMOCÍ UUID
        */ 
        _logger.LogDebug("Delete - Lecturer Delete With UUID");

        return NoContent(); //kód 204, jako podle JSONu

        // TAKÉ VLOŽIT 404 HANDLING
    }


}