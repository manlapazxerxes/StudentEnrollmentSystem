using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentSystem.ViewModels;

public class StudentEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string StudentNumber { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}