using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LMS_Project.Models;
using LMS_Project.Interfaces;
using LMS_Project.Repositories;

namespace LMS_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var userStats = await _adminRepository.GetUserStatisticsAsync();
            var courseStats = await _adminRepository.GetCourseStatisticsAsync();
            
            ViewBag.UserStats = userStats;
            ViewBag.CourseStats = courseStats;
            
            return View();
        }

        // User Management
        public async Task<IActionResult> Users()
        {
            var users = await _adminRepository.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

       // [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateUser(Student user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _adminRepository.CreateUserAsync(user);
        //        if (result)
        //        {
        //            return RedirectToAction(nameof(Users));
        //        }
        //    }
        //    return View(user);
        //}

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditUser(Student user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _adminRepository.UpdateUserAsync(user);
        //        if (result)
        //        {
        //            return RedirectToAction(nameof(Users));
        //        }
        //    }
        //    return View(user);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    var result = await _adminRepository.DeleteUserAsync(id);
        //    if (!result)
        //    {
        //        return NotFound();
        //    }
        //    return RedirectToAction(nameof(Users));
        //}

        // Course Management
        public async Task<IActionResult> Courses()
        {
            var courses = await _adminRepository.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await _adminRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminRepository.CreateCourseAsync(course);
                if (result)
                {
                    return RedirectToAction(nameof(Courses));
                }
            }
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _adminRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminRepository.UpdateCourseAsync(course);
                if (result)
                {
                    return RedirectToAction(nameof(Courses));
                }
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _adminRepository.DeleteCourseAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Courses));
        }

        // Role Management
        //public async Task<IActionResult> Roles()
        //{
        //    var roles = await _adminRepository.GetAllRolesAsync();
        //    return View(roles);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string roleId)
        {
            var result = await _adminRepository.AssignRoleToUserAsync(userId, roleId);
            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string userId, string roleId)
        {
            var result = await _adminRepository.RemoveRoleFromUserAsync(userId, roleId);
            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        //System Settings
        //public async Task<IActionResult> Settings()
        //{
        //    var settings = await _adminRepository.GetSystemSettingsAsync();
        //    return View(settings);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateSettings(SystemSettings settings)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _adminRepository.UpdateSystemSettingsAsync(settings);
        //        if (result)
        //        {
        //            return RedirectToAction(nameof(Settings));
        //        }
        //    }
        //    return View(nameof(Settings), settings);
        //}
    }
}