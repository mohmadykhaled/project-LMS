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
                    TempData["Massage"] = $"Welcome {registerViewModel.UserName}";
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
                return RedirectToAction("Dashboard", "Admin");
            if (await userManager.IsInRoleAsync(appUser, "Student"))
                return RedirectToAction("Index", "Home");
            if (await userManager.IsInRoleAsync(appUser, "Instructor"))
                return RedirectToAction("Index", "Home");

            // Fallback redirection
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signinManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> RedirectToProfile()
        {
            var user = await userManager.GetUserAsync(User);

            if (await userManager.IsInRoleAsync(user, "Student"))
                return RedirectToAction("Profile", "Student");

            if (await userManager.IsInRoleAsync(user, "Instructor"))
                return RedirectToAction("Profile", "Instructor");

            if (await userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToAction("Profile", "Admin");

            return RedirectToAction("AccessDenied", "Account");
        }



    }
}
