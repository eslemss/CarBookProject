using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;

public class UpdateReservationCancelCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
