using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.FooterAddressesDtos;
using UdemyCarBook.Dto.TestimonialDtos;

namespace UdemyCarBook.WebUI.ViewComponents.UILayoutWiewComponents
{
    public class _FooterUlLayoutComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUlLayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7125/api/FooterAddresses");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // DTO isminiz "ResulTestimonialDto" olduğu için buna uygun eşleştirme yapıldı.
                var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResulTestimonialDto>());
        }
    }
}
