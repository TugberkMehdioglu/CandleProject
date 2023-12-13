using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/")]
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel request, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(request);

            returnUrl ??= "/Home";

            AppUser? appUser = await _userManager.FindByEmailAsync(request.Email);
            if(appUser == null)
            {
                ModelState.AddModelErrorWithOutKey("Email veya şifre hatalı");
                return View(request);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(appUser, request.Password, request.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorWithOutKey("20 dakika boyunca giriş yapamazsınız");
                return View();
            }
            else if (!result.Succeeded)
            {
                ModelState.AddModelErrorWithOutKey("Email veya şifre hatalı", $"Başarısız giriş sayısı => {await _userManager.GetAccessFailedCountAsync(appUser)}");
                return View();
            }
            else if (!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                ModelState.AddModelErrorWithOutKey("Hesabınız aktif değildir, lütfen mail'inize yolladığımız onay mail'ine gidiniz");
                await _signInManager.SignOutAsync();//So that the cookie still does not remain in the browser
                return View();
            }
            else return Redirect(returnUrl);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}