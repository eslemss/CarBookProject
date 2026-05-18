using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults; // Sadece bu kalsın, o uzun olanı sil
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUserRentalsQueryHandler(IRepository<RentACarProcess> repository)
        : IRequestHandler<GetAppUserRentalsQuery, List<GetAppUserRentalsQueryResult>>
    {
        public async Task<List<GetAppUserRentalsQueryResult>> Handle(GetAppUserRentalsQuery request, CancellationToken cancellationToken)
        {
            // Veri tabanından tüm kiralama süreçlerini çekiyoruz
            var values = await repository.GetAllAsync();

            // Kullanıcı ID'sine göre filtreleme yapıyoruz
            var userRentals = values.Where(x => x.AppUserId == request.Id).ToList();

            return userRentals.Select(x => new GetAppUserRentalsQueryResult
            {
                // Entity'deki RentACarProcessID ile eşleştiğinden emin ol
                RentalId = x.RentACarProcessID,
                PickUpDate = x.PickUpDate,
                DropOffDate = x.DropOffDate,
                TotalPrice = x.TotalPrice,
                Status = x.Status, // Entity'ye eklediğimiz Status alanı

                // Navigation Property'lerin (Car ve Brand) null gelme ihtimaline karşı kontrol
                CarBrand = x.Car?.Brand?.Name ?? "Belirtilmemiş",
                CarModel = x.Car?.Model ?? "Belirtilmemiş"
            }).ToList();
        }
    }
}