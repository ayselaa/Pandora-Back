using Domain;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandoraWebMvc.ViewModels.Account;
using Service.Helpers.Enums;
using Service.Interfaces;

namespace PandoraWebMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IEmailService emailService,
                                 RoleManager<IdentityRole> roleManager,
                                 AppDbContext context
                                )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _context = context;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //string uniqueUsername = await GenerateUniqueUsername(model.FirstName, model.LastName);
                //if (uniqueUsername == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Unable to generate a unique username.");
                //    return View(model);
                //}
                string uniqueUsername = await GenerateUniqueUsername(model.FirstName, model.LastName);

                AppUser newUser = new()
                {
                    //UserName = string.Join("_", model.FirstName, model.LastName),
                    //UserName =model.Username,
                    UserName = uniqueUsername,
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,

                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    TempData["errors"] = model.ErrorMessages;
                    return RedirectToAction("Index", model);
                }

                //await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

                string subject = "Register confirmation";

                string html = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
                {
                    html = reader.ReadToEnd();
                }

                html = html.Replace("{{link}}", link);
                html = html.Replace("{{headerText}}", "Welcome");

                _emailService.Send(newUser.Email, subject, html);

                return RedirectToAction(nameof(VerifyEmail));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index", model);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

              
                AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.EmailOrUsername);
                }

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    if (!ModelState.IsValid) return View(model);

                }

                var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

                if (!res.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());


            string subject = "Verify password reset email";

            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Welcome");

            _emailService.Send(existUser.Email, subject, html);
            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return NotFound();
            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                await _signInManager.RefreshSignInAsync(user);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }





        private async Task<string> GenerateUniqueUsername(string firstName, string lastName)
        {
            string username = $"{firstName.ToLower()}.{lastName.ToLower()}";

            while (await _userManager.FindByNameAsync(username) != null)
            {
                string randomSuffix = Guid.NewGuid().ToString().Substring(0, 6);
                username = $"{firstName.ToLower()}.{lastName.ToLower()}.{randomSuffix}";
            }

            return await Task.FromResult(username);
        }

    }
}


