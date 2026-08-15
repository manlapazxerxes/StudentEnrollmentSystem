using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public class CourseRepository : ICourseRepository
{
    private static readonly List<Course> _courses = new()
    {
        new Course
        {
            Id = 1,
            CourseCode = "CS101",
            CourseName = "Introduction to Programming",
            Units = 3
        },
        new Course
        {
            Id = 2,
            CourseCode = "CS102",
            CourseName = "Object Oriented Programming",
            Units = 3
        },
        new Course
        {
            Id = 3,
            CourseCode = "CS103",
            CourseName = "Database Systems",
            Units = 3
        },
        new Course
        {
            Id = 4,
            CourseCode = "CS104",
            CourseName = "Web Development",
            Units = 3
        },
        new Course
        {
            Id = 5,
            CourseCode = "CS105",
            CourseName = "Data Structures",
            Units = 3
        },
        new Course
        {
            Id = 6,
            CourseCode = "CS106",
            CourseName = "Computer Networks",
            Units = 3
        },
        new Course
        {
            Id = 7,
            CourseCode = "CS107",
            CourseName = "Software Engineering",
            Units = 3
        },
        new Course
        {
            Id = 8,
            CourseCode = "CS108",
            CourseName = "Operating Systems",
            Units = 3
        }
    };

    public List<Course> GetAll()
    {
        return _courses.ToList();
    }

    public Course? GetById(int id)
    {
        return _courses.FirstOrDefault(c => c.Id == id);
    }
}