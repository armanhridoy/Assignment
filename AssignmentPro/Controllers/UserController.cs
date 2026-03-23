using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentPro.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index( CancellationToken cancellationToken)
        {

            var data = await _userRepository.GetAllUserAsync(cancellationToken);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new User());
            }
            else
            {
                var data = await _userRepository.GetUserByIdAsync(id, cancellationToken);
                if (data != null)
                {
                    return View(data);
                }
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(User user, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(user.UserID))
            {
                
                await _userRepository.AddUserAsync(user, cancellationToken);
                return RedirectToAction("Index");
            }
            else
            {
                
                await _userRepository.UpdateUserAsync(user, cancellationToken);
                return RedirectToAction("Index");
            }

           

        }
        [HttpGet]
        public async Task<IActionResult> Details(string id, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetUserByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        //[HttpGet]
        //public async Task<IActionResult> Details(string id, CancellationToken cancellationToken)
        //{
        //    var data = await _userRepository.GetUserByIdAsync(id, cancellationToken);
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(data);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetUserByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUserAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
