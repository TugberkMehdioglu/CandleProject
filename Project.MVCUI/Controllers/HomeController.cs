using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Project.BLL.ManagerServices.Abstarcts;
using Project.COMMON.Extensions;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels;
using System.Diagnostics;
using System.Net;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserManager _appUserManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserManager appUserManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserManager = appUserManager;
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
            if (appUser == null || appUser.Status == DataStatus.Deleted)
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

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirm");
            if (!ModelState.IsValid) return View(request);

            AppUser? appUser = await _userManager.FindByEmailAsync(request.Email);
            if(appUser == null)
            {
                ModelState.AddModelErrorWithOutKey("Bu email adresine sahip kullanıcı bulunamadı");
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string encodedPasswordResetToken = WebUtility.UrlEncode(passwordResetToken);
            string encodedUserId = WebUtility.UrlEncode(appUser.Id);
            string passwordResetLink = Url.Action("ResetPassword", "Home", new { Id = encodedUserId, token = encodedPasswordResetToken }, HttpContext.Request.Scheme)!;

            string mailMessage = $@"
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
    <div style='text-align: center;'>
        <img src='https://cdn-icons-png.flaticon.com/512/6357/6357059.png' alt='Logo' style='width: 15%; height: auto;'>
    </div>
    <div style='margin-top: 30px;'>
        <h2 style='text-align: center; color: rgb(46, 74, 150); font-family: sans-serif;'>Şifre değiştirme talebini aldık!</h2>
        <p style='text-align: center;'><a href='{passwordResetLink}' style='color:#fff; display: inline-block;padding: 10px 20px;background-color: rgb(46, 74, 150);text-decoration: none;border-radius: 5px; font-family: sans-serif;'>Şifremi Değiştir</a></p>

        <div style='text-align: center; font-family: sans-serif;'>
          <p>Eğer bu talebi siz gerçekleştirmediyseniz herhangi bir işlem yapmanıza gerek yoktur.</p>
        </div>
    </div>
</div>  ";

            await MailService.SendMailAsync(appUser.Email, mailMessage, "Şifre Sıfırlama | CandleProject");
            ViewBag.success = "Şifre sıfırlama linki mail adresinize gönderilmiştir";

            return View();
        }

        [HttpGet("{Id}/{token}")]
        public IActionResult ResetPassword(string Id, string token)
        {
            if (Id == null || token == null)
            {
                TempData["resetPasswordAlert"] = "Gerekli bilgiler bulunamadı";
                return RedirectToAction(nameof(ForgetPassword));
            }

            TempData["decodedId"] = WebUtility.UrlDecode(Id);
            TempData["decodedToken"] = WebUtility.UrlDecode(token);

            IEnumerable<IdentityError>? errors = HttpContext.Session.GetSession<IEnumerable<IdentityError>>("identityErros");
            if (errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                HttpContext.Session.Remove("identityErros");

                return View();
            }

            return View();
        }

        [HttpPost("{Id}/{token}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ForgetPasswordViewModel request)
        {
            ModelState.Remove("Email");
            if (!ModelState.IsValid) return View(request);

            object? Id = TempData["decodedId"];
            object? token = TempData["decodedToken"];
            if(Id == null || token == null)
            {
                TempData["resetPasswordAlert"] = "Gerekli bilgiler bulunamadı";
                return RedirectToAction(nameof(ForgetPassword));
            }

            AppUser? appUser = await _appUserManager.FindAsync(Id.ToString()!);
            if(appUser == null)
            {
                TempData["resetPasswordAlert"] = "Kullanıcı bulunamadı";
                return RedirectToAction(nameof(ForgetPassword));
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(appUser, token.ToString(), request.Password);
            if (!result.Succeeded)
            {
                string decodedId = WebUtility.UrlEncode(Id.ToString())!;
                string decodedToken = WebUtility.UrlEncode(token.ToString())!;

                HttpContext.Session.SetSession("identityErros", result.Errors);

                return RedirectToAction(nameof(ResetPassword), new { Id = decodedId, Token = decodedToken });
            }

            TempData["success"] = "Şifreniz başarıyla yenilendi";
            return RedirectToAction(nameof(Login));
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