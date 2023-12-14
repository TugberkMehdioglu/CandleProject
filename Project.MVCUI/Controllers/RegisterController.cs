using Microsoft.AspNetCore.Mvc;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class RegisterController : Controller
    {
        public IActionResult Signup()
        {
            return View();
        }
    }
}
