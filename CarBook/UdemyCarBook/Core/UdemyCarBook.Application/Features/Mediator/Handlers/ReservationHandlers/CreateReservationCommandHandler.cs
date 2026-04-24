using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces; // Repo arayüzü için ekle
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IRentACarRepository _rentACarRepository; // Yeni repo'yu enjekte ediyoruz

        public CreateReservationCommandHandler(IRepository<Reservation> repository, IRentACarRepository rentACarRepository)
        {
            _repository = repository;
            _rentACarRepository = rentACarRepository;
        }

        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // 1. Rezervasyon kaydını oluşturuyoruz
            await _repository.CreateAsync(new Reservation
            {
                Age = request.Age,
                CarID = request.CarID,
                Description = request.Description,
                DriverLicenseYear = request.DriverLicenseYear,
                DropOffLocationID = request.DropOffLocationID,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                PickUpLocationID = request.PickUpLocationID,
                Surname = request.Surname,
                Status = "Rezervasyon Alındı"
            });

            // 2. KRİTİK ADIM: Aracın müsaitliğini kapatıyoruz
            // Kullanıcının seçtiği CarID ve PickUpLocationID (Alış Lokasyonu) üzerinden status'u false yapıyoruz.
            await _rentACarRepository.UpdateRentACarAvailableStatusAsync(request.CarID, request.PickUpLocationID, false);
        }
    }
}