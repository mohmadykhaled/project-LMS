using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public ContentController(IContentRepository contentRepository, IInstructorRepository instructorRepository)
        {
            _contentRepository = contentRepository;
            _instructorRepository = instructorRepository;
        }

        // GET: Content
        public async Task<IActionResult> Index(int courseId)
        {
            // Verify the current instructor is the owner
            var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            if (!instructorCourses.Any(c => c.Id == courseId))
            {
                return Forbid();
            }

            var contents = await _contentRepository.GetContentsByCourseId(courseId);
            ViewBag.CourseId = courseId;
            return View(contents);
        }

        // GET: Content/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null)
            {
                return NotFound();
            }

            // Verify the current instructor is the owner
            var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            if (!instructorCourses.Any(c => c.Id == content.CourseId))
            {
                return Forbid();
            }

            return View(content);
        }

        // GET: Content Create
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        // POST: Content Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Type,FileUrl,CourseId")] Content content)
        {
            if (ModelState.IsValid)
            {
                // Verify the current  instructor is the owner
                var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
                if (!instructorCourses.Any(c => c.Id == content.CourseId))
                {
                    return Forbid();
                }

                await _contentRepository.Add(content);
                return RedirectToAction(nameof(Index), new { courseId = content.CourseId });
            }
            ViewBag.CourseId = content.CourseId;
            return View(content);
        }

        // GET: Content Edit by id
        public async Task<IActionResult> Edit(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null)
            {
                return NotFound();
            }

            // Verify the current instructor is the owner
            var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            if (!instructorCourses.Any(c => c.Id == content.CourseId))
            {
                return Forbid();
            }

            return View(content);
        }

        // POST: Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,FileUrl,CourseId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verify the current instructor is the owner
                var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
                if (!instructorCourses.Any(c => c.Id == content.CourseId))
                {
                    return Forbid();
                }

                await _contentRepository.Update(content);
                return RedirectToAction(nameof(Index), new { courseId = content.CourseId });
            }
            return View(content);
        }

        // POST: Content Delete by id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null)
            {
                return NotFound();
            }

            // Verify the current instructor is the owner
            var instructorCourses = await _instructorRepository.GetInstructorCourses(GetCurrentInstructorId());
            if (!instructorCourses.Any(c => c.Id == content.CourseId))
            {
                return Forbid();
            }

            await _contentRepository.Delete(content);
            return RedirectToAction(nameof(Index), new { courseId = content.CourseId });
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