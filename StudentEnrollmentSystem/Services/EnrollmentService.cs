using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Repositories;

namespace StudentEnrollmentSystem.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    public bool Enroll(
        int studentId,
        int courseId,
        DateTime enrollmentDate,
        out string errorMessage)
    {
        errorMessage = string.Empty;

        var student = _studentRepository.GetById(studentId);

        if (student == null)
        {
            errorMessage = "Student does not exist.";
            return false;
        }

        var course = _courseRepository.GetById(courseId);

        if (course == null)
        {
            errorMessage = "Course does not exist.";
            return false;
        }

        if (_enrollmentRepository.Exists(studentId, courseId))
        {
            errorMessage = "Student is already enrolled in this course.";
            return false;
        }

        var currentEnrollments =
            _enrollmentRepository.GetByStudentId(studentId);

        int currentUnits = 0;

        foreach (var existingEnrollment in currentEnrollments)
        {
            var existingCourse =
                _courseRepository.GetById(existingEnrollment.CourseId);

            if (existingCourse != null)
            {
                currentUnits += existingCourse.Units;
            }
        }

        if (currentUnits + course.Units > 24)
        {
            errorMessage =
                "Student cannot enroll because the maximum of 24 units would be exceeded.";

            return false;
        }

        if (enrollmentDate.Date > DateTime.Today)
        {
            errorMessage = "Enrollment date cannot be in the future.";
            return false;
        }

        var newEnrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrollmentDate = enrollmentDate
        };

        _enrollmentRepository.Add(newEnrollment);

        return true;
    }

    public List<Enrollment> GetByStudentId(int studentId)
    {
        return _enrollmentRepository.GetByStudentId(studentId);
    }
}