using MediatR;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;

public class GetReservationByIdQuery(int id) : IRequest<GetReservationByIdQueryResult>
{
    public int Id { get; set; } = id;
}