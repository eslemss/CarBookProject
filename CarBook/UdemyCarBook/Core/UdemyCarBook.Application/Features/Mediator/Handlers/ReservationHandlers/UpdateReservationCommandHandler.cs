using MediatR;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers;

public class UpdateReservationCommandHandler(IRepository<Reservation> repository) : IRequestHandler<UpdateReservationCommand>
{
    public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var x = await repository.GetByIdAsync(request.ReservationID);
        if (x != null)
        {
            x.Name = request.Name;
            x.Surname = request.Surname;
            x.Email = request.Email;
            x.Phone = request.Phone;
            x.CarID = request.CarID;

            // View'dan gelen ID'ler 0 ise mevcut veriyi koru, değilse güncelle
            x.PickUpLocationID = request.PickUpLocationID > 0 ? request.PickUpLocationID : x.PickUpLocationID;
            x.DropOffLocationID = request.DropOffLocationID > 0 ? request.DropOffLocationID : x.DropOffLocationID;

            x.Age = request.Age;
            x.DriverLicenseNumber = request.DriverLicenseNumber;
            x.Description = request.Description;
            x.Status = request.Status;

            // Kullanıcı ID'si 0 gelirse varsayılan bir değer ata (Staj projesinde 10 gibi)
            x.AppUserId = request.AppUserId > 0 ? request.AppUserId : x.AppUserId;

            await repository.UpdateAsync(x);
        }
    }
}