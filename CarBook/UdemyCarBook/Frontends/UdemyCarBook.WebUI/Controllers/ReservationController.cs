using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // UserManager için gerekli
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims; // ClaimTypes için
using System.Text;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.ReservationDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Identity bilgilerini çekmek için kullanacağız (Eğer projenizde Identity varsa)
        // Eğer bu kısım hata verirse, projenizde Identity kurulumuna göre düzenlenmelidir.
        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task LoadViewBags(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1. LOKASYONLAR
            var responseMessage = await client.GetAsync("https://localhost:7125/api/Locations");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                ViewBag.LocationList = values.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.LocationID.ToString()
                }).ToList();
            }

            // 2. ARAÇ BİLGİSİ
            var carResponse = await client.GetAsync($"https://localhost:7125/api/Cars/{id}");
            if (carResponse.IsSuccessStatusCode)
            {
                var carJson = await carResponse.Content.ReadAsStringAsync();
                var carData = JsonConvert.DeserializeObject<dynamic>(carJson);
                string brand = carData?.brandName ?? "Marka";
                string model = carData?.model ?? "Model";
                ViewBag.v3 = $"{brand} {model}";
            }

            ViewBag.CarID = id;
            ViewBag.v1 = "Araç Kiralama";
            ViewBag.v2 = "Araç Rezervasyon Formu";
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, int locationId, int dropOffLocationId, DateTime pickupDate, string pickupTime, DateTime dropoffDate, string dropoffTime)
        {
            await LoadViewBags(id);

            // 1. AD BİLGİSİNİ YAKALAMA (Tüm ihtimalleri deniyoruz)
            var name = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value
                       ?? User.Claims.FirstOrDefault(x => x.Type == "name")?.Value
                       ?? User.Claims.FirstOrDefault(x => x.Type == "given_name")?.Value; // API'den böyle gelebilir

            // 2. SOYAD BİLGİSİNİ YAKALAMA
            var surname = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value
                          ?? User.Claims.FirstOrDefault(x => x.Type == "family_name")?.Value
                          ?? User.Claims.FirstOrDefault(x => x.Type == "surname")?.Value;

            var email = User.Identity.Name;

            var model = new CreateReservationDto
            {
                CarID = id,
                PickUpLocationID = locationId,
                DropOffLocationID = dropOffLocationId > 0 ? dropOffLocationId : locationId,
                PickUpFull = DateTime.Parse($"{pickupDate:yyyy-MM-dd} {pickupTime ?? "12:00"}"),
                DropOffFull = DateTime.Parse($"{dropoffDate:yyyy-MM-dd} {dropoffTime ?? "12:00"}"),

                Name = name,
                Surname = surname,
                Email = email
            };

            ViewBag.pickupFull = model.PickUpFull.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.dropoffFull = model.DropOffFull.ToString("yyyy-MM-ddTHH:mm");

            return View(model);
        }
    }
}