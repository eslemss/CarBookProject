using MediatR;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers;

public class GetReservationByIdQueryHandler(IRepository<Reservation> repository) : IRequestHandler<GetReservationByIdQuery, GetReservationByIdQueryResult>
{
    public async Task<GetReservationByIdQueryResult> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var x = await repository.GetByIdAsync(request.Id);
        return new GetReservationByIdQueryResult
        {
            ReservationID = x.ReservationID,
            CarID = x.CarID,
            Name = x.Name,
            Surname = x.Surname,
            Email = x.Email,
            Phone = x.Phone,
            PickUpLocationID = x.PickUpLocationID ?? 0,
            DropOffLocationID = x.DropOffLocationID ?? 0,
            Age = x.Age,
            DriverLicenseNumber = x.DriverLicenseNumber,
            Description = x.Description,
            Status = x.Status,
            PickUpDate = x.PickUpDate,
            DropOffDate = x.PickUpDate,

        };
    }
}