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
    public ActionResult<Dictionary<string, string>> OnBaseApiCall()
    {
        _logger.LogDebug("[GET] Base /api called");
        
        var result = new Dictionary<string, string>
        {
            { "secret", "The cake is a lie" }
        };

        return Ok(result);
    }
    
    [HttpPost("/lecturers")]
    public ActionResult<string> AddLecturer([FromBody] Lecturer lecturer)
    {
        _logger.LogDebug("[POST] AddLecturer called");
        
        try
        {
            _mongoDal.AddLecturer(lecturer);
            return Ok("Lecturer přidán");
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return Problem(statusCode: 500);
        }
    }

    [HttpGet("/lecturers")]
    public ActionResult<IEnumerable<Lecturer>> GetAllLecturers()
    {
        _logger.LogDebug("[GET] GetAllLecturers called");
        
        try
        {
            var lecturers = _mongoDal.GetAllLecturers();
            return Ok(lecturers);
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return Problem(statusCode: 500);
        }
    }

    [HttpGet("/lecturers/{uuid}")]
    public ActionResult<Lecturer> GetLecturerByUuid([FromRoute] string uuid)
    {
        _logger.LogDebug("[GET] GetLecturerByUuid called");

        try
        {
            var lecturer = _mongoDal.GetLecturer(new Guid(uuid));
            if (lecturer == null)
                return NotFound("Lecturer nenalezen - UUID");

            return Ok(lecturer);
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return Problem(statusCode: 500);
        }
    }

    [HttpPut("/lecturers/{uuid}")]
    public ActionResult<string> AddLecturerByUuid([FromBody] Lecturer lecturerNew, [FromRoute] string uuid)
    {
        _logger.LogDebug("[PUT] AddLecturerByUuid called");

        try
        {
            _mongoDal.SetLecturer(lecturerNew, new Guid(uuid));
            return Ok("Lecturer nastaven, pokud existoval");
        }
        catch(Exception ex)
        {
            _logger.LogError("Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            
            //TODO: Odlišovat error u nás od nenalezena (Problem x NotFound)
            //return Problem(statusCode: 500);
            return NotFound("UUID nenalezeno");
        }
    }

    [HttpDelete("/lecturers/{uuid}")]
    public ActionResult<string> DeleteLecturerByUuid([FromRoute] string uuid)
    {
        _logger.LogDebug("[DELETE] DeleteLecturerByUuid called");

        try
        {
            var success = _mongoDal.DeleteLecturer(new Guid(uuid));
            if (!success)
                return NotFound("Lecturer nenalezen - UUID");
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return Problem(statusCode: 500);
        }
    }
}