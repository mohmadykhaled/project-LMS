using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
namespace LMS_Project.Controllers
{
    [Authorize(Roles = "Admin")]   
    public class CourseController : Controller
    {
        private readonly ICourseRepository  courseRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public CourseController(
            ICourseRepository _courseRepository ,IInstructorRepository instructorRepository,
            UserManager<ApplicationUser> _userManager)
        {
            this.instructorRepository = instructorRepository;
            this.courseRepository = _courseRepository;      
            this.userManager = _userManager;    
        }

        [HttpGet]
        public IActionResult Create ()
        {
            var viewmodel = new CreateCourseViewModel();
            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel createCourseViewModel ,IFormFile courseImage)
        {

            if (ModelState.IsValid)
            {
                string imageUrl = null;

                if (courseImage != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/courses", courseImage.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseImage.CopyToAsync(stream);
                    }

                    imageUrl = courseImage.FileName;
                }

                var course = new Course
                {
                    CourseName = createCourseViewModel.CourseName,
                    Description = createCourseViewModel.Description,
                    Title = createCourseViewModel.Title,
                    Price = createCourseViewModel.Price,
                    ImageUrl = imageUrl
                };
               await courseRepository.Add(course);
                await courseRepository.Save();
                TempData["Massage"] = $"Course {createCourseViewModel.CourseName} was Created ";
                return RedirectToAction("Index" ,"Home");
            }
            return View(createCourseViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCourse(int Id)
        {
             
            var coures = await courseRepository.GetById(Id);
            if (coures == null)
            {
                return NotFound();
            }
            var viewModel = new CreateCourseViewModel();
            viewModel.CourseName = coures.CourseName;
            viewModel.Description = coures.Description;
            viewModel.Price = coures.Price;
            viewModel.ImageUrl = coures.ImageUrl;
            viewModel.Title = coures.Title;
            return View("Edit", viewModel); 
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int Id, CreateCourseViewModel createCourseViewModel, IFormFile courseImage)
        {
            if (Id != 0 && ModelState.IsValid)
            {
                var course = await courseRepository.GetById(Id);
                if (course == null)
                {
                    return NotFound();
                }

                // Update course properties
                course.CourseName = createCourseViewModel.CourseName;
                course.Description = createCourseViewModel.Description;
                course.Price = createCourseViewModel.Price;
                course.Title = createCourseViewModel.Title;

                // Handle image upload only if a new image is provided
                if (courseImage != null && courseImage.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/courses", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseImage.CopyToAsync(stream);
                    }

                    course.ImageUrl = courseImage.FileName;
                }
                else
                {
                    // Retain the ImageUrl from the view model if no new image is uploaded
                    course.ImageUrl = !string.IsNullOrEmpty(createCourseViewModel.ImageUrl) ? createCourseViewModel.ImageUrl : course.ImageUrl;
                }

                await courseRepository.Update(course);
                await courseRepository.Save();
                TempData["Massage"] = $"Course {createCourseViewModel.CourseName} was Updated";
                return RedirectToAction("Index", "Home");
            }

            // If ModelState is invalid or Id is 0, return the view with the model
            return View(createCourseViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse (int id )
        {
            var course = await courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View("Delete",course);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
           
            await  courseRepository.Delete(id);
            await  courseRepository.Save();
            TempData["Massage"] = $"Course {course.CourseName} was Deleted";
            return RedirectToAction("GetAllCourses", "Admin");
        }

    }
}
