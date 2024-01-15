using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public interface IMongoDal
{
    public Lecturer? GetLecturer(Guid id);
    public IEnumerable<Lecturer> GetAllLecturers();
    public DBResult SetLecturer(Lecturer lecturer, Guid uuid);
    public DBResult DeleteLecturer(Guid id);
    public DBResult AddLecturer(Lecturer lecturer);
}