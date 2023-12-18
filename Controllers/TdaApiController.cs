using Microsoft.AspNetCore.Mvc;
using TeacherDigitalAgency.DAL;
using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.Controllers;

[ApiController]
[Route("/api")]
public class TdaApiController: ControllerBase
{
    private readonly ILogger<TdaApiController> _logger;
    private readonly IMongoDal _mongoDal;

    public TdaApiController(ILogger<TdaApiController> logger, IMongoDal mongoDal)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mongoDal = mongoDal ?? throw new ArgumentNullException(nameof(mongoDal));
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

//======
    
    [HttpPost("/lecturers")]
    public ActionResult<string> AddLecturer([FromBody] Lecturer lecturer)
    {
        _mongoDal.AddLecturer(lecturer);
        _logger.LogDebug("Post - Lecturer Add");

        return Ok("Lecturer přidán");
    }

    [HttpGet("/lecturers")]
    public ActionResult<IEnumerable<Lecturer>> GetLecturers()
    {
        IEnumerable<Lecturer> lecturer = _mongoDal.GetAllLecturers();
        _logger.LogDebug("Get - Lecturer Get All");

        return Ok(lecturer);
    }

    [HttpGet("/lecturers/{uuid}")]
    public ActionResult<Lecturer> GetLecturerWithUUID([FromRoute] string uuid)
    {
        Lecturer? lecturer;
        _logger.LogDebug("Get - Lecturer Get UUID");
        
        try
        { lecturer = _mongoDal.GetLecturer(new Guid(uuid)); }
        catch
        { return NotFound("UUID nenalezeno"); }

        return Ok(lecturer);
    }

    [HttpPut("/lecturers/{uuid}")]
    public ActionResult<string> AddLecturerWithUUID([FromBody] Lecturer lecturerNew)
    {
        _logger.LogDebug("Put - Lecturer Add/Edit With UUID");
        
        try         //"Pokud existuje pole v DB, ale neexistuje v těle požadavku, zůstane jeho hodnota v DB nezměněna."
        {           
            /*      //pokud bude třeba filtrovat input
            Lecturer? lecturerChecked = _mongoDal.GetLecturer(lecturerNew.Uuid);
            string empty = "string"; //prázdná (defaultní) hodnota, která nebude přepisována

            if(lecturerNew.TitleBefore != empty) { lecturerChecked.TitleBefore = lecturerNew.TitleBefore; }
            if(lecturerNew.FirstName != empty) { lecturerChecked.FirstName = lecturerNew.FirstName; }
            if(lecturerNew.MiddleName != empty) { lecturerChecked.MiddleName = lecturerNew.MiddleName; }
            if(lecturerNew.LastName != empty) { lecturerChecked.LastName = lecturerNew.LastName; }
            if(lecturerNew.TitleAfter != empty) { lecturerChecked.TitleAfter = lecturerNew.TitleAfter; }
            if(lecturerNew.PictureUrl != empty) { lecturerChecked.PictureUrl = lecturerNew.PictureUrl; }
            if(lecturerNew.Location != empty) { lecturerChecked.Location = lecturerNew.Location; }
            if(lecturerNew.Claim != empty) { lecturerChecked.Claim = lecturerNew.Claim; }
            if(lecturerNew.Bio != empty) { lecturerChecked.Bio = lecturerNew.Bio; }*/
        
            _mongoDal.SetLecturer(lecturerNew);
        }
        catch
        { return NotFound("UUID nenalezeno"); }

        return Ok("Lecturer nastaven");
    }

    [HttpDelete("/lecturers/{uuid}")]
    public ActionResult<string> DeleteLecturerWithUUID([FromRoute] string uuid)
    {
        _logger.LogDebug("Delete - Lecturer Delete With UUID");
        
        try{ _mongoDal.DeleteLecturer(new Guid(uuid)); }
        catch{ return NotFound("UUID nenalezeno"); }

        return NoContent(); //kód 204, jako podle JSONu
    }

}