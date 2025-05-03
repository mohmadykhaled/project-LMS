using LMS_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            return View("Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Future: Save message to DB or send email

            TempData["SuccessMessage"] = "Thank you for contacting us! We'll respond soon.";
            return RedirectToAction(nameof(Contact));
        }
    }
}
