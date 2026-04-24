using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.TestimonialDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.v = new List<SelectListItem>(); // Çökmeyi engellemek için şart

            var client = _httpClientFactory.CreateClient();
            // Token şartı aramadan doğrudan API'ye git (Swagger'da çalıştığı için)
            var responseMessage = await client.GetAsync("https://localhost:7125/api/Locations");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                ViewBag.v = values.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.LocationID.ToString()
                }).ToList();
            }
            return View();
        }


        [HttpPost]
        public IActionResult Index(string book_pick_date, string book_off_date, string time_pick, string time_off, string locationID)
        {
            TempData["bookpickdate"] = book_pick_date;
            TempData["bookoffdate"] = book_off_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["locationID"] = locationID;
            return RedirectToAction("Index", "RentACarList");
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            if (createTestimonialDto.ImageFile != null)
            {
                var extension = Path.GetExtension(createTestimonialDto.ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;

                // Klasör yolunu al
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

                // --- BU KISMI EKLE: Klasör yoksa oluştur ---
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // -------------------------------------------

                var saveLocation = Path.Combine(directoryPath, imageName);

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await createTestimonialDto.ImageFile.CopyToAsync(stream);
                }

                createTestimonialDto.ImageUrl = "/images/" + imageName;
            }
            else
            {
                createTestimonialDto.ImageUrl = "/images/default-user.png";
            }

            createTestimonialDto.ImageFile = null;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7125/api/Testimonial", stringContent);

            return RedirectToAction("Index");
        }
    }
}