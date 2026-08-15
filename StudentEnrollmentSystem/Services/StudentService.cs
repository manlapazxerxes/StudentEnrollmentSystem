using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Repositories;

namespace StudentEnrollmentSystem.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public List<Student> GetAll()
    {
        return _studentRepository.GetAll();
    }

    public Student? GetById(int id)
    {
        return _studentRepository.GetById(id);
    }

    public List<Student> Search(string searchTerm)
    {
        return _studentRepository.Search(searchTerm);
    }

    public bool Create(Student student, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(student.FirstName))
        {
            errorMessage = "First name is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(student.LastName))
        {
            errorMessage = "Last name is required.";
            return false;
        }

        var existingStudentNumber = _studentRepository
            .GetAll()
            .FirstOrDefault(s =>
                s.StudentNumber.Equals(
                    student.StudentNumber,
                    StringComparison.OrdinalIgnoreCase));

        if (existingStudentNumber != null)
        {
            errorMessage = "Student number already exists.";
            return false;
        }

        var existingEmail = _studentRepository
            .GetAll()
            .FirstOrDefault(s =>
                s.Email.Equals(
                    student.Email,
                    StringComparison.OrdinalIgnoreCase));

        if (existingEmail != null)
        {
            errorMessage = "Email address is already being used.";
            return false;
        }

        _studentRepository.Add(student);

        return true;
    }

    public bool Update(Student student, out string errorMessage)
    {
        errorMessage = string.Empty;

        var existingStudent = _studentRepository.GetById(student.Id);

        if (existingStudent == null)
        {
            errorMessage = "Student does not exist.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(student.FirstName))
        {
            errorMessage = "First name is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(student.LastName))
        {
            errorMessage = "Last name is required.";
            return false;
        }

        var duplicateStudentNumber = _studentRepository
            .GetAll()
            .FirstOrDefault(s =>
                s.Id != student.Id &&
                s.StudentNumber.Equals(
                    student.StudentNumber,
                    StringComparison.OrdinalIgnoreCase));

        if (duplicateStudentNumber != null)
        {
            errorMessage = "Student number already exists.";
            return false;
        }

        var duplicateEmail = _studentRepository
            .GetAll()
            .FirstOrDefault(s =>
                s.Id != student.Id &&
                s.Email.Equals(
                    student.Email,
                    StringComparison.OrdinalIgnoreCase));

        if (duplicateEmail != null)
        {
            errorMessage = "Email address is already being used.";
            return false;
        }

        _studentRepository.Update(student);

        return true;
    }

    public bool Delete(int id, out string errorMessage)
    {
        errorMessage = string.Empty;

        var student = _studentRepository.GetById(id);

        if (student == null)
        {
            errorMessage = "Student does not exist.";
            return false;
        }

        _studentRepository.Delete(id);

        return true;
    }
}