using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers;

public class UpdateReservationApproveCommandHandler(IRepository<Reservation> repository) : IRequestHandler<UpdateReservationApproveCommand>
{
    public async Task Handle(UpdateReservationApproveCommand request, CancellationToken cancellationToken)
    {
        var values = await repository.GetByIdAsync(request.Id);
        if (values != null)
        {
            values.Status = "Onaylandı";
            await repository.UpdateAsync(values);
        }
    }
}
