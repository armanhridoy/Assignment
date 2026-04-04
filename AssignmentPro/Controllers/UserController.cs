using AssignmentPro.FilesUpload;
using AssignmentPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static AssignmentPro.AuthIdentityModel.IdentityModel;

namespace AssignmentPro.Controllers
{
    public class UserController(UserManager<User> _userManager,IFileService fileService) : Controller
    {
       
        // Show current user details
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            return View(user);
        }

        // Upload Profile Image
        [HttpGet]
        public IActionResult EditProfile() => View();

        [HttpPost]
        public async Task<IActionResult> EditProfile(IFormFile? ProfileImage, string FullName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                user.ImageUrl = await fileService.Upload(ProfileImage, "Images");
            }

            if (!string.IsNullOrEmpty(FullName))
                user.UserName = FullName;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Details");
        }
    }
}