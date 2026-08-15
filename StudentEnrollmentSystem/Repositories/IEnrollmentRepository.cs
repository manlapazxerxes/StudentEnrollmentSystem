using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public interface IEnrollmentRepository
{
    List<Enrollment> GetAll();

    Enrollment? GetById(int id);

    List<Enrollment> GetByStudentId(int studentId);

    bool Exists(int studentId, int courseId);

    void Add(Enrollment enrollment);
}