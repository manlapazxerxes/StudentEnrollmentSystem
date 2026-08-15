using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentEnrollmentSystem.ViewModels;

public class EnrollmentCreateViewModel
{
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; } = DateTime.Today;

    public string StudentName { get; set; } = string.Empty;

    public List<SelectListItem> Courses { get; set; } = new();
}