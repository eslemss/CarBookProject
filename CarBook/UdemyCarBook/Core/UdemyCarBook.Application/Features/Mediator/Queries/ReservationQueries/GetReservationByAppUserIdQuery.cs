using MediatR;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;

public class GetReservationByAppUserIdQuery(int id) : IRequest<List<GetReservationByAppUserIdQueryResult>>
{
    public int Id { get; set; } = id;
}