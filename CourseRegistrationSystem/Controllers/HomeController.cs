using System.Diagnostics;
using CourseRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using CourseRegistrationSystem.Models;

namespace CourseRegistrationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static StudentResponse _studentDetails = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index(string? courseFilter, string? search)
        {
            var students = _studentDetails.Students;

            if (!string.IsNullOrEmpty(courseFilter))
            {
                students = students
                    .Where(x => x.CourseName == courseFilter)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                students = students
                    .Where(x =>
                        x.FirstName.Contains(search) ||
                        x.LastName.Contains(search) ||
                        x.EmailAddress.Contains(search))
                    .ToList();
            }

            var result = new StudentResponse
            {
                Students = students,
                Status = _studentDetails.Status,
                Message = _studentDetails.Message
            };

            return View(result);
        }
        [HttpPost]
        public IActionResult SubmitData(AddStudentRequest request)
        {
            var exists = _studentDetails.Students
           .Any(x => x.EmailAddress == request.EmailAddress
             && x.CourseName == request.CourseName);

            if (!exists)
            {
                _studentDetails.Students.Add(request);

                _studentDetails.Status = StatusEnum.Success;
                _studentDetails.Message = "Student registered successfully";
            }
            else
            {
                _studentDetails.Status = StatusEnum.AlreadyExists;
                _studentDetails.Message = "Student already registered in this course";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteStudent(string email, string course)
        {
            var student = _studentDetails.Students
                .FirstOrDefault(x => x.EmailAddress == email && x.CourseName == course);

            if (student != null)
            {
                _studentDetails.Students.Remove(student);
                _studentDetails.Status = StatusEnum.Success;
                _studentDetails.Message = "Student deleted successfully";
            }
            else
            {
                _studentDetails.Status = StatusEnum.Failed;
                _studentDetails.Message = "Student not found";
            }

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
