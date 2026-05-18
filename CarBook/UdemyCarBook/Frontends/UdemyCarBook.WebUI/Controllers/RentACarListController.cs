using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UdemyCarBook.Dto.BrandDtos;
using UdemyCarBook.Dto.RentACarDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RentACarListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            // Peek kullanarak veriyi sadece 'dikizliyoruz', silinmiyor.
            var locationID = TempData.Peek("locationID");

            if (locationID == null)
            {
                return RedirectToAction("Index", "Default");
            }

            id = int.Parse(locationID.ToString());
            ViewBag.locationID = id;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7125/api/RentACars?locationID={id}&available=true");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FilterRentACarDto>>(jsonData);

                // KRİTİK: Rezervasyon sayfasına geçene kadar tüm TempData'yı koru
                TempData.Keep();

                return View(values);
            }
            return View();
        }
    }
}