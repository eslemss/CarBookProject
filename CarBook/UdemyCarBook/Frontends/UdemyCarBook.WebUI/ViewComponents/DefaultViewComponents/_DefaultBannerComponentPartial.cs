using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UdemyCarBook.Dto.BannerDtos;

namespace UdemyCarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultBannerComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = new List<ResultBannerDto>
            {
                new ResultBannerDto
                {
                    Title = "Hafta Sonu %25 İndirim!",
                    Description = "Şehir dışı kaçamaklarınız için en konforlu araçlar şimdi çok daha avantajlı. Rezervasyonunuzu hemen yapın!",
                    ImageUrl = "https://images.pexels.com/photos/5650026/pexels-photo-5650026.jpeg"
                },
                new ResultBannerDto
                {
                    Title = "Yeni Üyelere Özel 500 TL!",
                    Description = "Aramıza hoş geldin! Hemen üye ol, ilk kiralama deneyiminde 500 TL indirimini anında kullan.",
                    ImageUrl = "https://media.istockphoto.com/id/1205116274/tr/foto%C4%9Fraf/giri%C5%9F-web-formu.jpg?s=2048x2048&w=is&k=20&c=R4cchh28ee-W7LlvfHUIHaVkdwvp1w4y8e1j_FFyqR4="
                },
                new ResultBannerDto
                {
                    Title = "Son 48 Saat: Dev Filo İndirimi!",
                    Description = "Seçili araçlarda %40'a varan indirimleri kaçırmayın. Stoklar hızla tükeniyor, yerinizi şimdi ayırtın!",
                    ImageUrl = "https://images.pexels.com/photos/385997/pexels-photo-385997.jpeg"
                },
                new ResultBannerDto
                {
                    Title = "Yollar Sizi Bekliyor, Puanlar Birikiyor!",
                    Description = "Bu kiralama sana 200 TL değerinde puan kazandırıyor! Bir sonraki yolculuğun bedavaya gelsin ister misin?",
                    ImageUrl = "https://images.pexels.com/photos/164634/pexels-photo-164634.jpeg"
                }
            };

            return View(values);
        }
    }
}