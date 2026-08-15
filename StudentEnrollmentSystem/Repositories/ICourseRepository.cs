using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public interface ICourseRepository
{
    List<Course> GetAll();

    Course? GetById(int id);
}