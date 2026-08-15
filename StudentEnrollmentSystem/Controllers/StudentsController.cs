using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Services;
using StudentEnrollmentSystem.ViewModels;

namespace StudentEnrollmentSystem.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    private readonly IEnrollmentService _enrollmentService;

    public StudentsController(
        IStudentService studentService,
        ICourseService courseService,
        IEnrollmentService enrollmentService)
    {
        _studentService = studentService;
        _courseService = courseService;
        _enrollmentService = enrollmentService;
    }

    public IActionResult Index(string? searchTerm)
    {
        var students = string.IsNullOrWhiteSpace(searchTerm)
            ? _studentService.GetAll()
            : _studentService.Search(searchTerm);

        var viewModels = students.Select(s => new StudentViewModel
        {
            Id = s.Id,
            StudentNumber = s.StudentNumber,
            FullName = $"{s.FirstName} {s.LastName}",
            Email = s.Email
        }).ToList();

        ViewBag.SearchTerm = searchTerm;

        return View(viewModels);
    }

    public IActionResult Details(int id)
    {
        var student = _studentService.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        var viewModel = new StudentDetailsViewModel
        {
            Id = student.Id,
            StudentNumber = student.StudentNumber,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            DateOfBirth = student.DateOfBirth
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(StudentCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var student = new Student
        {
            StudentNumber = model.StudentNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth
        };

        if (!_studentService.Create(student, out string errorMessage))
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _studentService.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentEditViewModel
        {
            Id = student.Id,
            StudentNumber = student.StudentNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DateOfBirth = student.DateOfBirth
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(StudentEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var student = new Student
        {
            Id = model.Id,
            StudentNumber = model.StudentNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth
        };

        if (!_studentService.Update(student, out string errorMessage))
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var student = _studentService.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentDetailsViewModel
        {
            Id = student.Id,
            StudentNumber = student.StudentNumber,
            FullName = $"{student.FirstName} {student.LastName}",
            Email = student.Email,
            DateOfBirth = student.DateOfBirth
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_studentService.Delete(id, out string errorMessage))
        {
            TempData["Error"] = errorMessage;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Enroll(int id)
    {
        var student = _studentService.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        var model = new EnrollmentCreateViewModel
        {
            StudentId = student.Id,
            StudentName = $"{student.FirstName} {student.LastName}",
            EnrollmentDate = DateTime.Today,
            Courses = _courseService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CourseCode} - {c.CourseName} ({c.Units} units)"
                })
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Enroll(EnrollmentCreateViewModel model)
    {
        var student = _studentService.GetById(model.StudentId);

        if (student == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            model.StudentName =
                $"{student.FirstName} {student.LastName}";

            model.Courses = _courseService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CourseCode} - {c.CourseName} ({c.Units} units)"
                })
                .ToList();

            return View(model);
        }

        if (!_enrollmentService.Enroll(
            model.StudentId,
            model.CourseId,
            model.EnrollmentDate,
            out string errorMessage))
        {
            ModelState.AddModelError(
                string.Empty,
                errorMessage);

            model.StudentName =
                $"{student.FirstName} {student.LastName}";

            model.Courses = _courseService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CourseCode} - {c.CourseName} ({c.Units} units)"
                })
                .ToList();

            return View(model);
        }

        TempData["Success"] = "Student enrolled successfully.";

        return RedirectToAction(nameof(Index));
    }
}