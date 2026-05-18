using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, List<GetReservationQueryResult>>
    {
        private readonly IRepository<Reservation> _repository;

        public GetReservationQueryHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReservationQueryResult>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            // Repository üzerinden IQueryable alarak ilişkili tabloları (Join) bağlıyoruz.
            // Bu sayede Application katmanı DbContext'e doğrudan bağımlı kalmaz.
            var values = await _repository.GetQueryable()
                .Include(x => x.Car)
                    .ThenInclude(c => c.Brand)
                .Include(x => x.PickUpLocation)
                .Include(x => x.DropOffLocation)
                .ToListAsync(cancellationToken);
            return values.Select(x => new GetReservationQueryResult
            {
                ReservationID = x.ReservationID,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                CarID = x.CarID,
                PickUpDate = x.PickUpDate,
                DropOffDate = x.DropOffDate,

                // Ekranda görünecek olan metinler (String alanlar)
                CarName = x.Car != null ? $"{x.Car.Brand.Name} {x.Car.Model}" : "Araç Yok",
                PickUpLocationName = x.PickUpLocation?.Name ?? "Belirtilmedi",
                DropOffLocationName = x.DropOffLocation?.Name ?? "Belirtilmedi",

                // Arka planda lazım olabilecek ID'ler (Hata veren yerler)
                PickUpLocationID = x.PickUpLocationID ?? 0,
                DropOffLocationID = x.DropOffLocationID ?? 0,

                Age = x.Age,
                DriverLicenseNumber = x.DriverLicenseNumber,
                Description = x.Description,
                Status = x.Status
            }).ToList();
        }
    }
}