using System;
using System.Threading.Tasks;
using BuisinessLogic.HelperServices;
using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class AccountController : Controller
    {

        private SignInManager<DomainLayer.User.User> _signManager;
        private UserManager<DomainLayer.User.User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;

        public AccountController(UserManager<DomainLayer.User.User> userManager,
            SignInManager<DomainLayer.User.User> signManager, IEmailService emailService, ISMSService smsService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpPost] 
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new DomainLayer.User.User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };

                //await Task.Run(() => _smsService.SendSMS(model.PhoneNumber));

            
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    DomainLayer.User.User tempUser = await _userManager.FindByEmailAsync(user.Email);
                    string callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = tempUser.Id, code = code },
                        protocol: Request.Scheme);

                    _emailService.SendConfirmationEmail(user, callbackUrl);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new Models.LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username).ConfigureAwait(false);
                    if(user != null)
                    {
                        await _signManager.SignInAsync(user, model.RememberMe);
                    }

                    if (user.UserName.Equals("admin") || user.UserName.Equals("Admin"))
                        return RedirectToAction("Index", "Admin");

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
