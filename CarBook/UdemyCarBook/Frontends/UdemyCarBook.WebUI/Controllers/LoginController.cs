using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UdemyCarBook.Dto.LoginDtos;
using UdemyCarBook.WebUI.Models;

namespace UdemyCarBook.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            // Kullanıcı zaten giriş yapmışsa tekrar login sayfasına girmesin
            if (User.Identity.IsAuthenticated)
            {
                TempData["AlreadyLoggedIn"] = "Zaten oturumunuz açık. Keyifli sürüşler dileriz!";

                // Giriş yapmış kullanıcının rolüne göre yönlendirme yapıyoruz
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Default");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto, string returnUrl = null)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7125/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null && tokenModel.Token != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    // --- TOKEN İÇİNDEKİ VERİLERİ YAKALAYALIM ---
                    var name = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.GivenName)?.Value;
                    var surname = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName)?.Value;

                    // ÖNEMLİ: API'den gelen rolü hem 'role' hem de 'ClaimTypes.Role' olarak kontrol ediyoruz
                    var userRole = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role || x.Type == "role")?.Value;

                    // --- STANDART CLAIM'LERİ EKLEYELİM ---
                    // Navbar'da @User.Identity.Name yazdığında ismin görünmesi için 'Name' claim'i şarttır
                    if (!string.IsNullOrEmpty(name)) claims.Add(new Claim(ClaimTypes.Name, name));

                    // Sistemin [Authorize(Roles="Admin")] ve User.IsInRole("Admin") yapılarını tanıması için 'Role' claim'i şarttır
                    if (!string.IsNullOrEmpty(userRole)) claims.Add(new Claim(ClaimTypes.Role, userRole));

                    if (!string.IsNullOrEmpty(name)) claims.Add(new Claim(ClaimTypes.GivenName, name));
                    if (!string.IsNullOrEmpty(surname)) claims.Add(new Claim(ClaimTypes.Surname, surname));

                    // API isteklerinde kullanmak üzere token'ı da saklıyoruz
                    claims.Add(new Claim("carbooktoken", tokenModel.Token));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties
                    {
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true
                    };

                    // Oturumu açıyoruz
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                    // Görsel bildirim mesajı
                    TempData["LoginSuccess"] = $"Hoş geldin {name} {surname}!";

                    // --- YÖNLENDİRME STRATEJİSİ ---

                    // 1. Eğer Admin ise her zaman Dashboard'a gitsin
                    if (userRole == "Admin")
                    {
                        return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
                    }

                    // 2. Eğer kısıtlı bir sayfadan (Örn: Rezervasyon) yönlendirildiyse oraya dönsün
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    // 3. Hiçbiri değilse ana sayfaya
                    return RedirectToAction("Index", "Default");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            TempData["LoginError"] = "Error";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }
}