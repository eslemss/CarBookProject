using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUI.Controllers
{
    public class ULayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
