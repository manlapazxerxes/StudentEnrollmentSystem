using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Services;

public interface IEnrollmentService
{
    bool Enroll(
        int studentId,
        int courseId,
        DateTime enrollmentDate,
        out string errorMessage);

    List<Enrollment> GetByStudentId(int studentId);
}