using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private static readonly List<Enrollment> _enrollments = new();

    public List<Enrollment> GetAll()
    {
        return _enrollments.ToList();
    }

    public Enrollment? GetById(int id)
    {
        return _enrollments.FirstOrDefault(e => e.Id == id);
    }

    public List<Enrollment> GetByStudentId(int studentId)
    {
        return _enrollments
            .Where(e => e.StudentId == studentId)
            .ToList();
    }

    public bool Exists(int studentId, int courseId)
    {
        return _enrollments.Any(e =>
            e.StudentId == studentId &&
            e.CourseId == courseId);
    }

    public void Add(Enrollment enrollment)
    {
        enrollment.Id = _enrollments.Count == 0
            ? 1
            : _enrollments.Max(e => e.Id) + 1;

        _enrollments.Add(enrollment);
    }
}