using MongoDB.Driver;
using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public class MongoDal: IMongoDal
{
    private readonly IMongoCollection<Lecturer> _lecturersCollection;

    public MongoDal(IConfiguration configuration)
    {
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

    public void SetLecturer(Lecturer lecturer, Guid uuid)
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

        _lecturersCollection.UpdateOne(filter, update);
    }

    public bool DeleteLecturer(Guid id)
    {
        // TODO: Refactor return value - distinguish NotFound, Error and Success
        var deleteResult = _lecturersCollection.DeleteOne(lecturer => lecturer.Uuid == id);
        return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged;
    }

    public void AddLecturer(Lecturer lecturer)
    {
        _lecturersCollection.InsertOne(lecturer);
    }
}