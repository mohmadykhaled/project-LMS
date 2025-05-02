using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    public class CourseController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;

        public CourseController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var courses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            return View(courses);
        }

        // GET: Course Details by ID
        public async Task<IActionResult> Details(int id)
        {
            var courses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            var course = courses.FirstOrDefault(c => c.Id == id);
            
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Duration,Price")] Course course)
        {
            if (ModelState.IsValid)
            {
                var result = await _instructorRepository.SubmitCourseForApproval(GetCurrentInstructorId(), course);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(course);
        }

        // Helper method to get current instructor ID (you'll need to implement this based on your authentication system)
        private int GetCurrentInstructorId()
        {
            // TODO: Implement this method to get the current instructor's ID from the authentication context
            // This is a placeholder - you should replace it with actual authentication logic
            return 1;
        }
    }
}