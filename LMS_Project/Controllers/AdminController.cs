using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LMS_Project.Models;
using LMS_Project.Interfaces;

using LMS_Project.Repositories;
using Microsoft.AspNetCore.Identity;
using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAdminRepository _adminRepo;
        private readonly IInstructorRepository instructorRepo;
        private readonly ICourseRepository courseRepository;
        private readonly IStudentRepository studentRepo;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAdminRepository adminRepo,
            IInstructorRepository instructorRepo,
            ICourseRepository _courseRepository, IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _adminRepo = adminRepo;
            this.instructorRepo = instructorRepo;
            this.courseRepository = _courseRepository;
            this.studentRepo = studentRepository;   
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]    
        public async Task<IActionResult> DashboardAsync()
        {
            AdminDashBoardViewModel vm = new AdminDashBoardViewModel();
            vm.TotalCourses = await courseRepository.CountAsync();
            vm.TotalStudents = await studentRepo.Countasync();
            vm.TotalInstructors = await instructorRepo.CountAsync();
            return View("Dashboard", vm);
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
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            var admin = await _adminRepo.GetByApplicationUserId(user.Id);
            if (admin == null) return NotFound("Admin profile not found.");

            var viewModel = new AdminProfileViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email
            };

            return View("AdminProfile", viewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignInstructor(int id)
        {
            var course = await courseRepository.GetById(id);
            if (course == null) return NotFound();

            var instructors = (await instructorRepo.GetAllwithUser()).ToList();//_context.Instructors.Include(i => i.User).ToListAsync();

            var viewModel = new AssignInstructorViewModel
            {
                CourseId = id,
                CourseName = course.CourseName,
                Instructors = instructors.Select(i => new SelectListItem
                {
                    Value = i.InstructorId.ToString(),
                    Text = i.User.FullName
                }).ToList() 
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignInstructor(AssignInstructorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var course = await courseRepository.GetById(model.CourseId);
            if (course == null) return NotFound();

            course.InstructorId = model.InstructorId;
            await courseRepository.Save();

            return RedirectToAction("Courses");
        }

    }
}