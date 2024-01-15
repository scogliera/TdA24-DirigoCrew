using MongoDB.Driver;
using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public class MongoDal: IMongoDal
{
    private readonly IMongoCollection<Lecturer> _lecturersCollection;
    private readonly ILogger<MongoDal> _logger;


    public MongoDal(IConfiguration configuration, ILogger<MongoDal> logger)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var connectionString = configuration.GetConnectionString("MongoDb") ?? throw new ArgumentNullException(nameof(configuration));

        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(settings);
        _lecturersCollection = client.GetDatabase("tda-db").GetCollection<Lecturer>("lecturers");
        _logger = logger;
    }

    public Lecturer? GetLecturer(Guid id)
    {
        
        return _lecturersCollection.Find(lecturer => lecturer.Uuid == id).FirstOrDefault();
    }

    public IEnumerable<Lecturer> GetAllLecturers()
    {
        return _lecturersCollection.Find(_ => true).ToEnumerable();
    }

    public DBResult SetLecturer(Lecturer lecturer, Guid uuid) // našlo/úspěch, nenašlo, selhalo
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
      
            if (result.MatchedCount >= 0)
                return DBResult.Success; 
            else
                return DBResult.NotFound;
        }
        catch
        { return DBResult.Error; }
    }

    public DBResult DeleteLecturer(Guid id) // našlo/úspěch, nenašlo, selhalo
    {
        try
        {
            var result = _lecturersCollection.DeleteOne(lecturer => lecturer.Uuid == id);

            if (result.DeletedCount <= 0)
                return DBResult.NotFound;
            else
                return DBResult.Success; 
        }
        catch
        { return DBResult.Error; }
    }

    public DBResult AddLecturer(Lecturer lecturer) // úspěch, neúspěch
    {
        try
        {
            _lecturersCollection.InsertOne(lecturer);
            return DBResult.Success;
        }
        catch
        { return DBResult.Error; }
    }
}

public enum DBResult 
{
    Success,
    NotFound,
    Error
}