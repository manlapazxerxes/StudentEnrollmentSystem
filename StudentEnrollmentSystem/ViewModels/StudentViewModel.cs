namespace StudentEnrollmentSystem.ViewModels;

public class StudentViewModel
{
    public int Id { get; set; }

    public string StudentNumber { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}