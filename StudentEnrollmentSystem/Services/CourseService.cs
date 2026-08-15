using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Repositories;

namespace StudentEnrollmentSystem.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public List<Course> GetAll()
    {
        return _courseRepository.GetAll();
    }

    public Course? GetById(int id)
    {
        return _courseRepository.GetById(id);
    }
}