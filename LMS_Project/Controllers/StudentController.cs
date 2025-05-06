using System.Threading.Tasks;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICourseRepository courseRepo;
        private readonly IStudentRepository studetnrepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentCourseRepository studentCourseRepo;

        public StudentController(UserManager<ApplicationUser> _userManager ,IStudentRepository _studentRepository,
            IStudentCourseRepository studentCourseRepository , ICourseRepository courseRepository)
        {
            studetnrepo = _studentRepository;
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
            var student = await studetnrepo.GetByApplicationUserId(userId);
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
            var student = await studetnrepo.GetByApplicationUserId(userId);
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
            TempData["Message"] = "You have been successfully enrolled in the course.";
            return RedirectToAction("Index", "Home");
        }


    }
}
