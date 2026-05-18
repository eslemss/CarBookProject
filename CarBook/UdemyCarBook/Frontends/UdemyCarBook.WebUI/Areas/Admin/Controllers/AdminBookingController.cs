using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.ReservationDtos;
using UdemyCarBook.Dto.CarDtos;

namespace UdemyCarBook.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/AdminBooking")]
public class AdminBookingController(IHttpClientFactory httpClientFactory) : Controller
{
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var client = httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7125/api/Reservations");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
            return View("~/Areas/Admin/Views/AdminBooking/Index.cshtml", values);
        }
        return View("~/Areas/Admin/Views/AdminBooking/Index.cshtml", new List<ResultReservationDto>());
    }

    [HttpGet("UpdateReservation/{id}")]
    public async Task<IActionResult> UpdateReservation(int id)
    {
        var client = httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:7125/api/Reservations/{id}");
        var carResponse = await client.GetAsync("https://localhost:7125/api/Cars");
        var locationResponse = await client.GetAsync("https://localhost:7125/api/Locations");

        if (responseMessage.IsSuccessStatusCode)
        {
            var carData = await carResponse.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDtos>>(carData);
            ViewBag.CarList = new SelectList(cars, nameof(ResultCarWithBrandsDtos.CarID), nameof(ResultCarWithBrandsDtos.Model));

            var locData = await locationResponse.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<List<ResultLocationDto>>(locData);
            ViewBag.LocationList = new SelectList(locations, nameof(ResultLocationDto.LocationID), nameof(ResultLocationDto.Name));

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateReservationDto>(jsonData);
            return View("~/Areas/Admin/Views/AdminBooking/UpdateReservation.cshtml", values);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("UpdateReservation/{id}")]
    public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
    {
        var client = httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateReservationDto);
        var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PutAsync("https://localhost:7125/api/Reservations", stringContent);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(updateReservationDto);
    }
}