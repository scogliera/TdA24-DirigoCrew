using TeacherDigitalAgency.Models;

namespace TeacherDigitalAgency.DAL;

public interface IMongoDal
{
    public Lecturer? GetLecturer(Guid id);
    public IEnumerable<Lecturer> GetAllLecturers();
    public void SetLecturer(Lecturer lecturer);
    public bool DeleteLecturer(Guid id);
    public void AddLecturer(Lecturer lecturer);
}