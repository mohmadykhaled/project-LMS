using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Repositories;
using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IInstructorRepository instructorRepo;

        public InstructorController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IInstructorRepository instructorRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.instructorRepo = instructorRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateInstructor()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstructor(InstructorViewModel instructorVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = instructorVM.Email;
                appUser.PasswordHash = instructorVM.Password;
                appUser.FullName = instructorVM.FullName;
                appUser.UserName = instructorVM.UserName;
                IdentityResult result = await _userManager.CreateAsync(appUser, instructorVM.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Instructor");
                    var instructor = new Instructor();
                    instructor.ApplicationUserId = appUser.Id;
                    instructor.HireDate = instructorVM.HireDate.Value;
                    instructorRepo.Add(instructor);
                    instructorRepo.Save();
                    await _signInManager.SignInAsync(appUser, false);
                    TempData["Massage"] = $"Instructor  {instructorVM.UserName} has Created successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(instructorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            var instructor = await instructorRepo.GetByApplicationUserId(user.Id);
            if (instructor == null) return NotFound("Instructor profile not found.");

            var viewModel = new InstructorProfileViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                HireDate = instructor.HireDate,
                CoursesTaught = instructor.Courses.Select(c => new CourseViewModel
                {
                    CourseName = c.Title,
                    Description = c.Description
                }).ToList()
            };

            return View("InstructorProfile", viewModel);
        }

    }
}
