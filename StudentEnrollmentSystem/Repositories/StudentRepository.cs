using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repositories;

public class StudentRepository : IStudentRepository
{
    private static readonly List<Student> _students = new()
    {
        new Student
        {
            Id = 1,
            StudentNumber = "2026-0001",
            FirstName = "Juan",
            LastName = "Dela Cruz",
            Email = "juan@gmail.com",
            DateOfBirth = new DateTime(2005, 5, 10)
        },
        new Student
        {
            Id = 2,
            StudentNumber = "2026-0002",
            FirstName = "Maria",
            LastName = "Santos",
            Email = "maria@gmail.com",
            DateOfBirth = new DateTime(2004, 8, 20)
        },
        new Student
        {
            Id = 3,
            StudentNumber = "2026-0003",
            FirstName = "Pedro",
            LastName = "Reyes",
            Email = "pedro@gmail.com",
            DateOfBirth = new DateTime(2005, 2, 15)
        }
    };

    public List<Student> GetAll()
    {
        return _students.ToList();
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public void Add(Student student)
    {
        student.Id = _students.Count == 0
            ? 1
            : _students.Max(s => s.Id) + 1;

        _students.Add(student);
    }

    public void Update(Student student)
    {
        var existingStudent = GetById(student.Id);

        if (existingStudent == null)
        {
            return;
        }

        existingStudent.StudentNumber = student.StudentNumber;
        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;
        existingStudent.DateOfBirth = student.DateOfBirth;
    }

    public void Delete(int id)
    {
        var student = GetById(id);

        if (student != null)
        {
            _students.Remove(student);
        }
    }

    public List<Student> Search(string searchTerm)
    {
        return _students
            .Where(s =>
                s.StudentNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                s.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                s.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}