namespace StudentEnrollmentSystem.Models;

public class Course
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = string.Empty;

    public string CourseName { get; set; } = string.Empty;

    public int Units { get; set; }
}