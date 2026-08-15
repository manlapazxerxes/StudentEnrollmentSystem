using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public interface IStudentRepository
{
    List<Student> GetAll();

    Student? GetById(int id);

    void Add(Student student);

    void Update(Student student);

    void Delete(int id);

    List<Student> Search(string searchTerm);
}