using System.Threading.Tasks;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Repository;
using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace LMS_Project.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICourseRepository courseRepo;
        private readonly IStudentRepository studentRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentCourseRepository studentCourseRepo;

        public StudentController(UserManager<ApplicationUser> _userManager ,IStudentRepository _studentRepository,
            IStudentCourseRepository studentCourseRepository , ICourseRepository courseRepository)
        {
            studentRepo = _studentRepository;
            userManager = _userManager;
            studentCourseRepo = studentCourseRepository;
            courseRepo = courseRepository;
        }
        public async Task<IActionResult> Details(int id)
        {
            var course = await courseRepo.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Enroll (int courseId)
        {
            var userId = userManager.GetUserId(User);   
            var student = await studentRepo.GetByApplicationUserId(userId);
            if (student == null)
            {
                return NotFound();
            }
            var course = await courseRepo.GetById(courseId);
            
            return View(course);    
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollConfirm (int courseId)
        {
            var userId =  userManager.GetUserId(User);
            var student = await studentRepo.GetByApplicationUserId(userId);
            if (student == null)
            {
                return NotFound();
            }
            var course =await courseRepo.GetById(courseId);
            if (course == null)
            {
                return NotFound();
            }

            await studentCourseRepo.EnrollStudentAsync(student.StudentId, courseId);
            await studentCourseRepo.Save();
            TempData["Massage"] = "You have been successfully enrolled in the course.";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            var student = await studentRepo.GetByApplicationUserId(user.Id);
            if (student == null) return NotFound("Student profile not found.");

            var viewModel = new StudentProfileViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                DateOfBirth = student.DateOfBirth,
                EnrolledCourses = student.StudentCourses.Select(sc => new CourseViewModel
                {
                    CourseName = sc.Course.Title,
                    Description = sc.Course.Description,
                    InstructorFullName = "Mohamed Ahmed"
                }).ToList()
            };

            return View("StudentProfile", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await studentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("Delete", student);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await studentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            await studentRepo.Delete(id);
            await studentRepo.Save();
            TempData["Massage"] = $"Student {student.User.UserName} was Deleted Successfully";
            return RedirectToAction("GetAllStudents", "Admin");
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> StudentDetails(int id)
        {
            var student = await studentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
