using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using UdemyCarBook.Dto.ReservationDtos;
using Microsoft.AspNetCore.Authorization; // Authorize için eklendi

namespace UdemyCarBook.WebUI.Controllers
{
    [Authorize] // Sadece giriş yapanlar geçmişini görebilsin
    public class MyReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MyReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            // --- GÜNCEL ID OKUMA MANTĞI ---
            // LoginController'da hangi isimle (ClaimType) kaydedildiyse hepsini deniyoruz.
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value
                        ?? User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
                        ?? User.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            // Eğer ID hala bulunamazsa (Giriş yapılmamışsa veya claim yoksa) 
            // Sayfa boş dönsün veya hata vermesin diye kontrol ekliyoruz.
            if (string.IsNullOrEmpty(userId))
            {
                return View(new List<ResultReservationDto>());
            }

            // API'ye isteğimizi gönderiyoruz
            var responseMessage = await client.GetAsync($"https://localhost:7125/api/Reservations/GetReservationByAppUserId?id={userId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
                return View(values);
            }

            return View(new List<ResultReservationDto>());
        }
    }
}