using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.BrandDtos;
using UdemyCarBook.Dto.RegisterDto;

namespace UdemyCarBook.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateAppUser(string returnUrl = null)
        {
            // 1. ADIM: Login'den veya başka yerden gelen dönüş adresini yakalayıp View'a gönderiyoruz
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateRegisterDto createRegisterDto, string returnUrl = null)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createRegisterDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7125/api/Registers", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                // 2. ADIM: Kayıt başarılı! Kullanıcıyı Login'e gönderirken returnUrl'i de yanına veriyoruz
                return RedirectToAction("Index", "Login", new { returnUrl = returnUrl });
            }

            // Hata durumunda adresi tekrar ViewBag'e koyuyoruz ki kaybolmasın
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}