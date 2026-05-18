using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler(
        IRepository<Reservation> repository,
        IRentACarRepository rentACarRepository)
        : IRequestHandler<CreateReservationCommand>
    {
        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // 1. ADIM: ÇAKIŞMA KONTROLÜ
            var existingReservations = await repository.GetAllAsync() ?? new List<Reservation>();

            var isCarBusy = existingReservations.Any(x => x.CarID == request.CarID &&
       x.Status != "İptal Edildi" &&
       (
           (request.PickUpDate >= x.PickUpDate && request.PickUpDate <= x.DropOffDate) ||
           (request.DropOffDate >= x.PickUpDate && request.DropOffDate <= x.DropOffDate) ||
           (x.PickUpDate >= request.PickUpDate && x.PickUpDate <= request.DropOffDate)
       ));

            if (isCarBusy)
            {
                var lastReservation = existingReservations
                    .Where(x => x.CarID == request.CarID && x.Status != "İptal Edildi")
                    .OrderByDescending(x => x.DropOffDate)
                    .FirstOrDefault();

                string suggestion = lastReservation != null
                    ? $"Bu araç en erken {lastReservation.DropOffDate.AddHours(1):dd.MM.yyyy HH:mm} tarihinde müsait olacaktır."
                    : "Lütfen farklı bir tarih aralığı deneyiniz.";

                // BURASI ÖNEMLİ: Sadece Exception fırlatıyoruz.
                throw new Exception($"Seçilen tarihlerde araç dolu. Öneri: {suggestion}");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                return;
            }

            // 2. ADIM: REZERVASYON OLUŞTURMA (Eğer çakışma yoksa buraya geçer)
            await repository.CreateAsync(new Reservation
            {
                Age = request.Age,
                CarID = request.CarID,
                Description = request.Description,
                DriverLicenseNumber = request.DriverLicenseNumber,
                AppUserId = request.AppUserId,
                DropOffLocationID = request.DropOffLocationID,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                PickUpLocationID = request.PickUpLocationID,
                Surname = request.Surname,
                Status = "Rezervasyon Alındı",
                PickUpDate = request.PickUpDate,
                DropOffDate = request.DropOffDate
            });

            if (request.CarID > 0 && request.PickUpLocationID > 0)
            {
                await rentACarRepository.UpdateRentACarAvailableStatusAsync(request.CarID, request.PickUpLocationID, false);
            }
        }
    }
}