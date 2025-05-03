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
            (UserManager<ApplicationUser> _userManger ,SignInManager<ApplicationUser> _signInManager ,IAdminRepository _adminRepository ,
               IStudentRepository _studentRepoitory ,IInstructorRepository _instructorRepository )
        {
            userManager = _userManger ;  
            signinManger = _signInManager;
            adminRepo = _adminRepository;
            studentRepo = _studentRepoitory ;
            instructorRepo = _instructorRepository ;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegsiterViewModel regsiterViewModel)
        {
            if (ModelState.IsValid)
            {
                // Validate role-specific fields
                if (regsiterViewModel.Role == "Student" && (!regsiterViewModel.EnrollmentDate.HasValue || !regsiterViewModel.DateOfBirth.HasValue))
                {
                    ModelState.AddModelError(string.Empty, "Enrollment Date and Date of Birth are required for Students.");
                    return View(regsiterViewModel);
                }
                if (regsiterViewModel.Role == "Instructor" && !regsiterViewModel.HireDate.HasValue)
                {
                    ModelState.AddModelError(string.Empty, "Hire Date is required for Instructors.");
                    return View(regsiterViewModel);
                }
                // Restrict Admin role assignment to authenticated admins
                if (regsiterViewModel.Role == "Admin" && !User.IsInRole("Admin"))
                {
                    ModelState.AddModelError(string.Empty, "Only admins can create admin accounts.");
                    return View(regsiterViewModel);
                }
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = regsiterViewModel.Email;
                appUser.PasswordHash = regsiterViewModel.Password;
                appUser.UserName = regsiterViewModel.UserName;
                appUser.FullName = regsiterViewModel.FullName;
                IdentityResult Result = await userManager.CreateAsync(appUser , regsiterViewModel.Password);
                if (Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, regsiterViewModel.Role);
                    
                    if (regsiterViewModel.Role == "Student")
                    {
                        var student = new Student();
                        student.ApplicationUserId = appUser.Id;
                        student.DateOfBirth = regsiterViewModel.DateOfBirth.Value;
                        student.EnrollmentDate = regsiterViewModel.EnrollmentDate.Value;
                        studentRepo.Add(student);
                        studentRepo.Save();
                    }
                    else if (regsiterViewModel.Role == "Admin")
                    {
                        Admin admin = new Admin();
                        admin.ApplicationUserId = appUser.Id;
                        adminRepo.Add(admin);
                        adminRepo.Save();
                    }
                    else if (regsiterViewModel.Role == "Instructor")
                    {
                        var instructor = new Instructor();
                        instructor.ApplicationUserId = appUser.Id;
                        instructor.HireDate = regsiterViewModel.HireDate.Value;    
                        instructorRepo.Add(instructor);
                        instructorRepo.Save();
                    }
                    await signinManger.SignInAsync(appUser , isPersistent: false);

                    if (regsiterViewModel.Role == "Admin")
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else if (regsiterViewModel.Role == "Student")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (regsiterViewModel.Role == "Instructor")
                    {
                        return RedirectToAction("Index", "InstructorDashboard");
                    }
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", regsiterViewModel);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login ()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUsersViewModel loginUsersView)
        {
            if (!ModelState.IsValid)
                return View("Login");

            var appUser = await userManager.FindByNameAsync(loginUsersView.Name);
            if (appUser == null || !await userManager.CheckPasswordAsync(appUser, loginUsersView.Password))
            {
                ModelState.AddModelError("", "Username or password is incorrect.");
                return View("Login", loginUsersView);
            }

            await signinManger.SignInAsync(appUser, loginUsersView.RememberMe);

            // Role-based redirection
            if (await userManager.IsInRoleAsync(appUser, "Admin"))
                return RedirectToAction("Index", "AdminDashboard");

            if (await userManager.IsInRoleAsync(appUser, "Student"))
                return RedirectToAction("Index", "Home");

            if (await userManager.IsInRoleAsync(appUser, "Instructor"))
                return RedirectToAction("Index", "InstructorDashboard");

            // Fallback
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> SignOut ()
        {
           await  signinManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
