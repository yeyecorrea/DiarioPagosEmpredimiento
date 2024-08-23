using DiarioPagosApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiarioPagosApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        
        }

        public IActionResult UserValidation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult UserRegistration()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegistration(UserRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User() { FirstName = model.FirstName, LastName = model.LastName, UserEmail = model.UserEmail };

            var result = await _userManager.CreateAsync(user, password: model.PassWord);
            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("UserValidation");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(UserLoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserEmail, model.PassWord, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
