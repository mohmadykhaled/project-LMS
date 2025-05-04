using LMS_Project.ViewModel;
using LMS_Project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LMS_Project.Repositories;
using LMS_Project.Repository;
using LMS_Project.Interfaces;


namespace LMS_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManger;
        private readonly IAdminRepository adminRepo;
        private readonly IStudentRepository studentRepo;
        private readonly IInstructorRepository instructorRepo;

        public AccountController
            (UserManager<ApplicationUser> _userManger, SignInManager<ApplicationUser> _signInManager, IAdminRepository _adminRepository,
               IStudentRepository _studentRepoitory, IInstructorRepository _instructorRepository)
        {
            userManager = _userManger;
            signinManger = _signInManager;
            adminRepo = _adminRepository;
            studentRepo = _studentRepoitory;
            instructorRepo = _instructorRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegsiterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();


                appUser.Email = registerViewModel.Email;
                appUser.UserName = registerViewModel.UserName;
                appUser.FullName = registerViewModel.FullName;
                appUser.PasswordHash = registerViewModel.Password;

                IdentityResult result = await userManager.CreateAsync(appUser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "Student");

                    var student = new Student();

                    student.ApplicationUserId = appUser.Id;
                    student.EnrollmentDate = registerViewModel.EnrollmentDate.Value;
                    student.DateOfBirth = registerViewModel.DateOfBirth.Value;
                    
                    studentRepo.Add(student);
                    await studentRepo.Save();

                    await signinManger.SignInAsync(appUser, false);
                    TempData["Massage"] = $"Welcome ${registerViewModel.UserName}";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("Register", registerViewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginUsersViewModel loginUsersView)
        {
            if (!ModelState.IsValid)
                return View("Login", loginUsersView);

            var appUser = await userManager.FindByNameAsync(loginUsersView.Name);
            if (appUser == null || !await userManager.CheckPasswordAsync(appUser, loginUsersView.Password))
            {
                ModelState.AddModelError("", "The username or password is incorrect.");
                return View("Login");
            }

            await signinManger.SignInAsync(appUser, loginUsersView.RememberMe);

            // Redirect based on user role
            if (await userManager.IsInRoleAsync(appUser, "Admin"))
                return RedirectToAction("Index", "AdminDashboard");
            if (await userManager.IsInRoleAsync(appUser, "Student"))
                return RedirectToAction("Index", "Home");
            if (await userManager.IsInRoleAsync(appUser, "Instructor"))
                return RedirectToAction("Index", "InstructorDashboard");

            // Fallback redirection
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signinManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            // Get the currently logged-in user
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check user role and fetch appropriate profile
            if (await userManager.IsInRoleAsync(user, "Student"))
            {
                var student = await studentRepo.GetByApplicationUserId(user.Id); // Make this async
                if (student == null)
                {
                    return NotFound("Student profile not found.");
                }

                // Map to view model (optional but recommended)
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
                        InstructorFullName = sc.Course.Instructor.User.FullName
                    }).ToList()
                };

                return View("StudentProfile", viewModel);
            }
            else if (await userManager.IsInRoleAsync(user, "Instructor"))
            {
                var instructor = await instructorRepo.GetByApplicationUserId(user.Id); // Make async
                if (instructor == null)
                {
                    return NotFound("Instructor profile not found.");
                }
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

                // Optionally map to InstructorProfileViewModel
                return View("InstructorProfile", viewModel);
            }
            else if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var admin = await adminRepo.GetByApplicationUserId(user.Id); // Make async
                if (admin == null)
                {
                    return NotFound("Admin profile not found.");
                }
                var viewModel = new AdminProfileViewModel
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                };  
                // Optionally map to AdminProfileViewModel
                return View("AdminProfile", viewModel);
            }

            return Unauthorized("No valid role assigned to the user.");
        }


    }
}
