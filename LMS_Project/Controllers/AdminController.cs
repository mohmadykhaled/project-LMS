using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LMS_Project.Models;
using LMS_Project.Interfaces;

using LMS_Project.Repositories;
using Microsoft.AspNetCore.Identity;
using LMS_Project.ViewModel;

namespace LMS_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAdminRepository _adminRepo;
        private readonly IInstructorRepository instructorRepo;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAdminRepository adminRepo,
            IInstructorRepository instructorRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _adminRepo = adminRepo;
            this.instructorRepo = instructorRepo;
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAdmin(RegsiterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);


            var user = new ApplicationUser();
            user.Email = vm.Email;
            user.FullName = vm.FullName;
            user.PasswordHash = vm.Password;
            user.UserName = vm.UserName;
            

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(vm);
            }


           var rresult2 =  await _userManager.AddToRoleAsync(user, "Admin");
            if (!rresult2.Succeeded)
            {
                foreach (var err in rresult2.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(vm);
            }

            var admin = new Admin();
            admin.ApplicationUserId = user.Id;
           
            _adminRepo.Add(admin);
            _adminRepo.Save();



            TempData["Message"] = $"Admin account '{user.UserName}' created.";
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult CreateInstructor()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateInstructor (InstructorViewModel instructorVM)
        {
           if(ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = instructorVM.Email;
                appUser.PasswordHash = instructorVM.Password;
                appUser.FullName = instructorVM.FullName;
                appUser.UserName = instructorVM.UserName;
              IdentityResult result =  await _userManager.CreateAsync(appUser , instructorVM.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Instructor");
                    var instructor = new Instructor();
                    instructor.ApplicationUserId = appUser.Id;
                    instructor.HireDate = instructorVM.HireDate.Value; 
                    instructorRepo.Add(instructor); 
                    instructorRepo.Save();
                    await _signInManager.SignInAsync(appUser, false);
                    TempData["Massage"] = $"Welcome ${instructorVM.UserName}";
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
       
    }
}