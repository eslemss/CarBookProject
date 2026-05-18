using MediatR;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries
{
    public class GetReservationQuery : IRequest<List<Results.ReservationResults.GetReservationQueryResult>>
    {
    }
}