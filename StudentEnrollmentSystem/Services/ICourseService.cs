using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Services;

public interface ICourseService
{
    List<Course> GetAll();

    Course? GetById(int id);
}