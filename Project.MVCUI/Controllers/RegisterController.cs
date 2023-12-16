using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstarcts;
using Project.BLL.ManagerServices.Concretes;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Extensions;
using Project.MVCUI.ViewModels.WrapperClasses;
using System.Net;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class RegisterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppUserManager _appUserManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterController(IMapper mapper, IAppUserManager appUserManager, UserManager<AppUser> userManager, IAppUserProfileManager appUserProfileManager, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _appUserManager = appUserManager;
            _userManager = userManager;
            _appUserProfileManager = appUserProfileManager;
            _signInManager = signInManager;
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            bool checkBox = Request.Form.Keys.Contains("termCheckBox");
            if (!checkBox)
            {
                ModelState.AddModelErrorWithOutKey("Üyelik sözleşmesini kabul etmelisiniz");
                return View(request);
            }

            AppUser appUser = _mapper.Map<AppUser>(request.AppUser);
            AppUserProfile appUserProfile = _mapper.Map<AppUserProfile>(request.AppUserProfile);

            appUser.Id = Guid.NewGuid().ToString();
            var (errors, error) = await _appUserManager.AddUserByIdentityAsync(appUser);
            if (errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                return View(request);
            }
            else if (error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            //Oluşan token'da özel karakterler veya Url ile uyumsuz karakterler olmasına karşın Encode'ladık, Activation action'ında da Decode'ladık.
            string encodedToken = WebUtility.UrlEncode(confirmationToken);
            string encodedAppUserId = WebUtility.UrlEncode(appUser.Id);

            string confirmationLink = Url.Action("Activation", "Register", new { id = encodedAppUserId, token = encodedToken }, HttpContext.Request.Scheme)!;

            string mailMessage = $@"
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
    <div style='text-align: center;'>
        <img src='https://cdn0.iconfinder.com/data/icons/web-seo-outline-1/32/user_interface_OUTLINE_GRADIENT-01-512.png' alt='Logo' style='width: 15%; height: auto;'>
    </div>
    <div style='margin-top: 30px;'>
        <h2 style='text-align: center; color: rgb(46, 74, 150); font-family: sans-serif;'>Aramıza Hoşgeldiniz!</h2>
        <p style='text-align: center;'><a href='{confirmationLink}' style='color:#fff; display: inline-block;padding: 10px 20px;background-color: rgb(46, 74, 150);text-decoration: none;border-radius: 5px; font-family: sans-serif;'>Üyeliğinizi aktifleştirmek için tıklayınız</a></p>

        <div style='text-align: center; font-family: sans-serif;'>
          <p>Merhaba {appUserProfile.FullName}, hesabınız başarıyla oluşturuldu.</p>
          <p>Yeni nesil mum dünyası <strong>CandleProject.com</strong>'a hoş geldiniz!</p>
          <p>Üyelik işleminizin tamamlanabilmesi için yukarıdaki bağlantıdan hesabınızı aktif etmeniz gerekmektedir.</p>
        </div>
    </div>
</div>  ";

            await MailService.SendMailAsync(request.AppUser.Email, mailMessage, "CandleProject | Hesap Aktivasyon");

            appUserProfile.Id = appUser.Id;
            string? alert = await _appUserProfileManager.AddAsync(appUserProfile);
            if(alert != null)
            {
                ModelState.AddModelErrorWithOutKey(alert);
                return View(request);
            }

            IdentityResult result = await _userManager.AddToRoleAsync(appUser, "Member");
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorListWithOutKey(result.Errors);
                return View(request);
            }

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(RegistrationCompleted), "Register", new { appUser.Email });
        }

        [HttpGet("{email}")]
        public IActionResult RegistrationCompleted(string email)
        {
            return View(model: email);
        }

        [HttpGet("{id}/{token}")]
        public async Task<IActionResult> Activation(string id, string token)
        {
            string decodedId = WebUtility.UrlDecode(id);
            string decodedToken = WebUtility.UrlDecode(token);

            AppUser? appUser = await _appUserManager.FindAsync(decodedId);
            if (appUser == null)
            {
                ViewBag.message = "Kullanıcı bulunamadı";
                return View();
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(appUser, decodedToken);
            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;

                foreach (IdentityError error in result.Errors)
                {
                    errorMessage += $"{error.Description}\n";
                }

                ViewBag.message = errorMessage;
            }

            ViewBag.message = "Mail adresiniz başarıyla aktive edilmiştir";
            return View();
        }
    }
}
