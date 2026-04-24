using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.AboutDtos;

namespace UdemyCarBook.WebUI.ViewComponents.AboutViewComponents
{
    public class _AboutUsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AboutUsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            //GETASYNC () --> VERİLERİ ÇEKMEDE , LİSTEleme de  KULLANILAN METOT
            var responseMessage = await client.GetAsync("https://localhost:7125/api/Abouts");
            if (responseMessage.IsSuccessStatusCode) //CODE-> İKİYÜZLÜ (200-299)DURUM KODLARI api işleminde başarılı gerçekleştiğini gösterir
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultAboutDto>());
        }
    }
}
