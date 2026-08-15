using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Services;

public interface IStudentService
{
    List<Student> GetAll();

    Student? GetById(int id);

    List<Student> Search(string searchTerm);

    bool Create(Student student, out string errorMessage);

    bool Update(Student student, out string errorMessage);

    bool Delete(int id, out string errorMessage);
}