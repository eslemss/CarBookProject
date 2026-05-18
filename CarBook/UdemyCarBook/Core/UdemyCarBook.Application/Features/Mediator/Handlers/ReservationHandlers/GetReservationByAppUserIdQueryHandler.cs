using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;
using UdemyCarBook.Application.Interfaces.ReservationInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers;

public class GetReservationByAppUserIdQueryHandler(IReservationRepository _repository)
    : IRequestHandler<GetReservationByAppUserIdQuery, List<GetReservationByAppUserIdQueryResult>>
{
    public async Task<List<GetReservationByAppUserIdQueryResult>> Handle(GetReservationByAppUserIdQuery request, CancellationToken cancellationToken)
    {
        // Repository üzerinden ilişkili verilerle birlikte listeyi çekiyoruz
        var valuesList = await _repository.GetReservationsByAppUserIdWithRelations(request.Id);

        if (valuesList == null)
        {
            return new List<GetReservationByAppUserIdQueryResult>();
        }

        return valuesList.Select(x => new GetReservationByAppUserIdQueryResult
        {
            ReservationID = x.ReservationID,

            // ARAÇ ADI VE MODELİ
            CarName = (x.Car != null && x.Car.Brand != null)
              ? $"{x.Car.Brand.Name} {x.Car.Model}"
              : "Araç Bilgisi Yok",

            // LOKASYON VERİLERİ
            PickUpLocationName = x.PickUpLocation?.Name ?? "Belirtilmedi",
            DropOffLocationName = x.DropOffLocation?.Name ?? "Belirtilmedi",

            // TARİH VE ZAMAN (01.01.0001 hatasını önler)
            PickUpFull = x.PickUpDate,
            DropOffFull = x.DropOffDate,

            // DİĞER VERİLER
            Status = x.Status,
            Email = x.Email,
            Phone = x.Phone,

            // TUTAR (Eğer tablonda fiyat alanı varsa bağla, yoksa manuel hesaplanabilir)
            // Örnek: TotalPrice = x.TotalPrice 
        }).ToList();
    }
}