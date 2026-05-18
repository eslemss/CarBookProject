using MediatR;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries;

public class GetAppUserQuery : IRequest<GetAppUserQueryResult>
{
    public int Id { get; set; }
    public GetAppUserQuery(int id)
    {
        Id = id;
    }
}