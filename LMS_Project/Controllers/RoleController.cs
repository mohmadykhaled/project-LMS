using System.Threading.Tasks;
using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    [Authorize (Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;  
        }

        [HttpGet]
        public IActionResult AddRole ()
        {
            return View("AddRole");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddRole (RoleViewModel roleviewmodel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleviewmodel.RoleName;
                IdentityResult result = await  roleManager.CreateAsync(role); 
                if (result.Succeeded == true)
                {
                    TempData["Massage"] = $"Role {role.Name} Added ";
                    return RedirectToAction("DashboardAsync", "Admin");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleviewmodel); 
        }
    }
}
