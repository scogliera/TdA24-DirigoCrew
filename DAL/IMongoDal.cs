using TeacherDigitalAgency.Data;
using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public interface IMongoDal
{
    public Lecturer? GetLecturer(Guid id);
    public IEnumerable<Lecturer> GetAllLecturers();
    public DbResult SetLecturer(Lecturer lecturer, Guid uuid);
    public DbResult DeleteLecturer(Guid id);
    public DbResult AddLecturer(Lecturer lecturer);
}