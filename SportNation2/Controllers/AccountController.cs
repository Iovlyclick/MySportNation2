using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportNation2.Models;
using SportNation2.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportNation2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                await accountService.LoginAsync(model.Email, model.Password, model.RememberMe);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Email", e.Message);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                await accountService.SignupAsync(model.Email, model.Password, model.BirthDate, model.Genre);

                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Email", e.Message);
                return View();
            }
        }

        [Authorize]
        public IActionResult Profile()
        {
            // Récupérez les informations de l'utilisateur connecté à partir des claims
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var birthDate = User.FindFirst(System.Security.Claims.ClaimTypes.DateOfBirth)?.Value;

            // Utilisez les informations pour construire le modèle de vue
            var viewModel = new ProfileViewModel
            {
                Email = email,
                BirthDate = DateTime.Parse(birthDate)
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            // Récupérez les informations de l'utilisateur connecté à partir des claims
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var birthDate = User.FindFirst(System.Security.Claims.ClaimTypes.DateOfBirth)?.Value;

            // Utilisez les informations pour construire le modèle de vue d'édition
            var editProfileViewModel = new EditProfileViewModel
            {
                Email = email,
                BirthDate = DateTime.Parse(birthDate)
            };

            return View(editProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfileAsync(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Récupérez l'utilisateur connecté à partir des claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Récupérez l'utilisateur à partir de la base de données
            var user = await accountService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Mettez à jour les informations du profil de l'utilisateur
            user.BirthDate = model.BirthDate;
            user.Genre = model.Genre;

            // Enregistrez les modifications dans la base de données
            await accountService.UpdateUserProfileAsync(user.Email, user.BirthDate, (Infrastructure.UserGenre)user.Genre);

            return RedirectToAction("Profile");
        }
    }
}
