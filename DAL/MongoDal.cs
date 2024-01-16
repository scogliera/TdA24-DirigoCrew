using MongoDB.Driver;
using TeacherDigitalAgency.Data;
using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public class MongoDal: IMongoDal
{
    private readonly IMongoCollection<Lecturer> _lecturersCollection;
    private readonly ILogger<MongoDal> _logger;

    public MongoDal(IConfiguration configuration, ILogger<MongoDal> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var connectionString = configuration.GetConnectionString("MongoDb") ?? throw new ArgumentNullException(nameof(configuration));

        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(settings);
        _lecturersCollection = client.GetDatabase("tda-db").GetCollection<Lecturer>("lecturers");
    }

    public Lecturer? GetLecturer(Guid id)
    {
        return _lecturersCollection.Find(lecturer => lecturer.Uuid == id).FirstOrDefault();
    }

    public IEnumerable<Lecturer> GetAllLecturers()
    {
        return _lecturersCollection.Find(_ => true).ToEnumerable();
    }

    public DbResult SetLecturer(Lecturer lecturer, Guid uuid)
    {
        try
        {
            var filter = Builders<Lecturer>.Filter.Eq(l => l.Uuid, uuid);
            var update = Builders<Lecturer>.Update
                .Set(l => l.TitleBefore, lecturer.TitleBefore)
                .Set(l => l.FirstName, lecturer.FirstName)
                .Set(l => l.MiddleName, lecturer.MiddleName)
                .Set(l => l.LastName, lecturer.LastName)
                .Set(l => l.TitleAfter, lecturer.TitleAfter)
                .Set(l => l.PictureUrl, lecturer.PictureUrl)
                .Set(l => l.Location, lecturer.Location)
                .Set(l => l.Claim, lecturer.Claim)
                .Set(l => l.Bio, lecturer.Bio)
                .Set(l => l.Tags, lecturer.Tags)
                .Set(l => l.PricePerHour, lecturer.PricePerHour)
                .Set(l => l.ContactInfo, lecturer.ContactInfo);

            var result = _lecturersCollection.UpdateOne(filter, update);
      
            if (result.MatchedCount <= 0)
                return DbResult.NotFound;
            
            return DbResult.Success; 
        }
        catch(Exception ex)
        {
            _logger.LogError("SetLecturer Error - Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return DbResult.Error; 
        }
    }

    public DbResult DeleteLecturer(Guid id)
    {
        try
        {
            var result = _lecturersCollection.DeleteOne(lecturer => lecturer.Uuid == id);
            if (result.DeletedCount <= 0)
                return DbResult.NotFound;
                
            return DbResult.Success; 
        }
        catch(Exception ex)
        {
            _logger.LogError("DeleteLecturer Error - Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return DbResult.Error; 
        }
    }

    public DbResult AddLecturer(Lecturer lecturer)
    {
        try
        {
            _lecturersCollection.InsertOne(lecturer);
            return DbResult.Success;
        }
        catch(Exception ex)
        { 
            _logger.LogError("AddLecturer Error - Message: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            return DbResult.Error; 
        }
    }
}
