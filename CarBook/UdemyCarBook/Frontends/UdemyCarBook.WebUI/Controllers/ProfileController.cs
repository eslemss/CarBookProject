using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using UdemyCarBook.WebUI.Models;
using System.Text;

namespace UdemyCarBook.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int id = GetCurrentUserId();
            if (id <= 0) return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7125/api/AppUsers/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var userProfile = JsonConvert.DeserializeObject<UserViewModel>(jsonData);

                // Kiralama geçmişini çekme kısmı
                var rentalResponse = await client.GetAsync($"https://localhost:7125/api/AppUsers/GetUserRentals/{id}");
                if (rentalResponse.IsSuccessStatusCode)
                {
                    var rentalData = await rentalResponse.Content.ReadAsStringAsync();
                    var apiRentals = JsonConvert.DeserializeObject<List<dynamic>>(rentalData);
                    var mappedRentals = apiRentals.Select(x => new UserRentalViewModel
                    {
                        ReservationID = (int)x.reservationID,
                        CarBrand = (string)x.carName,
                        PickUpDate = (DateTime)x.pickUpFull,
                        DropOffDate = (DateTime)x.dropOffFull,
                        Status = (string)x.status,
                        PickUpLocationName = (string)x.pickUpLocationName,
                        DropOffLocationName = (string)x.dropOffLocationName
                    }).ToList();
                    ViewBag.Rentals = mappedRentals;
                }
                return View(userProfile);
            }
            return View(new UserViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7125/api/AppUsers/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UserViewModel>(jsonData);
                return View(value);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel model, IFormFile? ImageFile)
        {
            var client = _httpClientFactory.CreateClient();
            var currentResponse = await client.GetAsync($"https://localhost:7125/api/AppUsers/{model.AppUserId}");
            if (!currentResponse.IsSuccessStatusCode) return View(model);

            var dbUser = JsonConvert.DeserializeObject<UserViewModel>(await currentResponse.Content.ReadAsStringAsync());

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", imageName);
                using (var stream = new FileStream(location, FileMode.Create)) { await ImageFile.CopyToAsync(stream); }
                model.ImageUrl = "/images/" + imageName;
            }
            else if (string.IsNullOrEmpty(model.ImageUrl))
            {
                model.ImageUrl = dbUser.ImageUrl;
            }

            var updateDto = new
            {
                AppUserId = model.AppUserId,
                Name = model.Name ?? dbUser.Name,
                Surname = model.Surname ?? dbUser.Surname,
                UserName = model.UserName ?? dbUser.UserName,
                Email = model.Email ?? dbUser.Email,
                ImageUrl = model.ImageUrl,
                Password = string.IsNullOrEmpty(model.Password) ? dbUser.Password : model.Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(updateDto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7125/api/AppUsers/", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            return View(model);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ?? User.Claims.FirstOrDefault(x => x.Type == "sub");
            if (userIdClaim == null) return -1;
            return int.TryParse(userIdClaim.Value, out int id) ? id : -1;
        }
    }
}