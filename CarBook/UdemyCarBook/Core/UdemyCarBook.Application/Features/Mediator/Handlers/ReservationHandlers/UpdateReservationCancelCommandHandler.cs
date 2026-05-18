using MediatR;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers;

public class UpdateReservationCancelCommandHandler(IRepository<Reservation> repository) : IRequestHandler<UpdateReservationCancelCommand>
{
    public async Task Handle(UpdateReservationCancelCommand request, CancellationToken cancellationToken)
    {
        // Baştaki alt tireyi ( _ ) kaldırdığından emin ol
        var x = await repository.GetByIdAsync(request.Id);

        if (x != null)
        {
            x.Status = "İptal Edildi";

            // AppUserId null ise veritabanı kaydı reddeder, bu yüzden 10 (Mustafa) atıyoruz
            if (x.AppUserId == 0 || x.AppUserId == null)
            {
                x.AppUserId = 10;
            }

            await repository.UpdateAsync(x);
        }
    }
}