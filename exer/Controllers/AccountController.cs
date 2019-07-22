using System;
using System.Threading.Tasks;
using BuisinessLogic.HelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using www.karmasms.ir;

namespace Main.Controllers
{
    public class AccountController : Controller
    {

        private SignInManager<DomainLayer.User.User> _signManager;
        private UserManager<DomainLayer.User.User> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<DomainLayer.User.User> userManager,
            SignInManager<DomainLayer.User.User> signManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
        }

        [HttpPost] 
        public async Task<IActionResult> SignUp(Models.SignUpViewModel model)
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
                /*if (KarmaSMSCore.SMSHelper.SendSMS("300087777", "rastin", "!@#123", "09355905880", "Hi From WalletAPI"))
                {
                    Console.WriteLine("SMS Sent\n\n");
                }
                else
                {
                    Console.WriteLine("SMS Failed to send\n\n");
                }*/

                await _emailService.SendEmail(model.Email, "WalletAPI", "Hi from wallet API");
                //var result = await _userManager.CreateAsync(user, model.Password);
                
                
                /*if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }*/
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
        public async Task<IActionResult> Login(Models.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username).ConfigureAwait(false);
                    if(user != null)
                    {
                        Console.WriteLine("\nlogging in " + user.UserName + "\n");
                        await _signManager.SignInAsync(user, model.RememberMe);
                    }

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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
