using System.Diagnostics;
using LMS_Project.Models;
using LMS_Project.Repository;
using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using LMS_Project.Interfaces;   

namespace LMS_Project.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ICourseRepository courseRepository;

        public HomeController(ICourseRepository _courseRepository)
        {
            this.courseRepository = _courseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Courses()
        {
            List<Course> courseslist = (await courseRepository.GetCourseswithInstructor()).ToList();
            var courseList = courseslist.Select(c => new CourseListViewModel
            {
                CourseName = c.CourseName,
                InstructorName = c.Instructor.User.FullName,
                ImageUrl = c.ImageUrl   
            }).ToList();
            return View("Courses",courseList);
        }
        public IActionResult Contact()
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
