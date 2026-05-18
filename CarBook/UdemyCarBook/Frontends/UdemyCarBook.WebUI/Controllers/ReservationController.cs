using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.AppUserDtos;
using UdemyCarBook.Dto.CarDtos;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.ReservationDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    [Area("Admin")]
    [Route("Reservation")] // Ana Rota
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:7125/api/Reservations";

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string search)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_baseUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);

                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    values = values.Where(x => x.Name.ToLower().Contains(search) || x.Surname.ToLower().Contains(search)).ToList();
                }
                ViewBag.CurrentSearch = search;
                return View("~/Views/Reservation/Index.cshtml", values);
            }
            return View("~/Views/Reservation/Index.cshtml");
        }

        [HttpGet]
        [Route("CreateReservation/{id}")]
        public async Task<IActionResult> CreateReservation(int id)
        {
            await LoadViewBags();

            // Görseldeki 'id' claim'ini yakalıyoruz
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value
                         ?? User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            var model = new CreateReservationDto { CarID = id };

            if (!string.IsNullOrEmpty(userId))
            {
                var client = _httpClientFactory.CreateClient();
                // API tarafında bu ID'ye sahip kullanıcıyı getiren endpoint:
                var response = await client.GetAsync($"https://localhost:7125/api/AppUsers/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    // Yeni oluşturduğun DTO'yu burada kullanıyoruz
                    var userDetail = JsonConvert.DeserializeObject<UserDetailDto>(jsonData);

                    // Verileri modele aktarıyoruz
                    model.Name = userDetail.Name;
                    model.Surname = userDetail.Surname;
                    model.Email = userDetail.Email;
                    model.AppUserId = int.Parse(userId); // Rezervasyonu yapanın ID'si
                }
            }

            return View("~/Views/Reservation/CreateReservationForm.cshtml", model);
        }

        [HttpPost]
        [Route("CreateReservation/{id}")]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_baseUrl, stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                // BAŞARILI: Kullanıcıyı doğrudan profilindeki kiralama geçmişine gönderiyoruz.
                // Area bilgisini "" (boş) vererek Admin alanından ana dizine çıkmasını sağlıyoruz.
                return RedirectToAction("Index", "Profile", new { area = "" });
            }
            else
            {
                // HATA: API'den gelen mesajı al ve formu tekrar göster.
                var apiErrorMessage = await responseMessage.Content.ReadAsStringAsync();

                ViewBag.ErrorMessage = !string.IsNullOrEmpty(apiErrorMessage)
                                       ? apiErrorMessage
                                       : "Seçilen tarihlerde bu araç maalesef dolu.";

                await LoadViewBags();
                return View("~/Views/Reservation/CreateReservationForm.cshtml", dto);
            }
        }
        [HttpGet]
        [Route("CheckCarAvailability")]
        public async Task<IActionResult> CheckCarAvailability(int carId, DateTime pickup, DateTime dropoff)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Tarih formatını API'nin (Model Binding) en sağlıklı anlayacağı yyyy-MM-ddTHH:mm formatına zorluyoruz.
                // Bu sayede .0000000 gibi milisaniye hatalarının önüne geçiyoruz.
                string url = $"https://localhost:7125/api/Reservations/CheckAvailability?carId={carId}&pickup={pickup:yyyy-MM-ddTHH:mm}&dropoff={dropoff:yyyy-MM-ddTHH:mm}";

                var response = await client.GetAsync(url);

                // API 200 OK döndüğünde araç müsaittir.
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { available = true });
                }

                // API 400 veya 500 dönerse (Araç dolu veya sistem hatası), mesajı yakalıyoruz.
                var message = await response.Content.ReadAsStringAsync();

                return Json(new
                {
                    available = false,
                    message = string.IsNullOrEmpty(message) ? "Seçilen tarihlerde araç müsait değil." : message
                });
            }
            catch (Exception ex)
            {
                // API kapalıysa veya bağlantı koparsa burası çalışır.
                return Json(new { available = false, message = "Bağlantı hatası: " + ex.Message });
            }
        }
        [HttpGet]
        [Route("EditReservation/{id}")] // Baştaki /Admin/ kısımlarını temizledik çünkü yukarıda [Route] var
        public async Task<IActionResult> EditReservation(int id)
        {
            await LoadViewBags();
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_baseUrl}/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateReservationDto>(jsonData);
                return View("~/Views/Reservation/EditReservation.cshtml", values);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("EditReservation/{id}")]
        public async Task<IActionResult> EditReservation(UpdateReservationDto updateReservationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateReservationDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync(_baseUrl, stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            await LoadViewBags();
            return View("~/Views/Reservation/EditReservation.cshtml", updateReservationDto);
        }

 [HttpGet] // Linke tıklandığında çalışması için ekle
[Route("ApproveReservation/{id}")]
public async Task<IActionResult> ApproveReservation(int id)
{
    var client = _httpClientFactory.CreateClient();
    // API tarafındaki adresin doğruluğundan emin ol (StatusChangeApproved mu yoksa ChangeApproved mu?)
    var response = await client.GetAsync($"https://localhost:7125/api/Reservations/ReservationStatusChangeApproved/{id}");
    
    if (response.IsSuccessStatusCode)
    {
        return RedirectToAction("Index");
    }
    
    // Eğer API tarafında hata varsa hatayı görmek için:
    return RedirectToAction("Index"); 
}

        [Route("CancelReservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"{_baseUrl}/ReservationStatusChangeCancelled/{id}");
            return RedirectToAction("Index");
        }

        private async Task LoadViewBags()
        {
            var client = _httpClientFactory.CreateClient();

            var responseCar = await client.GetAsync("https://localhost:7125/api/Cars/GetCarWithBrand");
            if (responseCar.IsSuccessStatusCode)
            {
                var jsonData = await responseCar.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDtos>>(jsonData);
                ViewBag.CarList = values.Select(x => new SelectListItem
                {
                    Text = x.BrandName + " " + x.Model,
                    Value = x.CarID.ToString()
                }).ToList();
            }

            var responseLoc = await client.GetAsync("https://localhost:7125/api/Locations");
            if (responseLoc.IsSuccessStatusCode)
            {
                var jsonData = await responseLoc.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
                ViewBag.LocationList = values.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.LocationID.ToString()
                }).ToList();
            }
        }
    }
}