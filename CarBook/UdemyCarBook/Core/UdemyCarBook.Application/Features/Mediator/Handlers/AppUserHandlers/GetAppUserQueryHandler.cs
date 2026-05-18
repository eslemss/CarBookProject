using MediatR;
using UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AppUserHandlers;

public class GetAppUserQueryHandler(IRepository<AppUser> repository)
    : IRequestHandler<GetAppUserQuery, GetAppUserQueryResult>
{
    public async Task<GetAppUserQueryResult> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
    {
        // ❗ 1. ID kontrolü (en kritik fix)
        if (request.Id <= 0)
            throw new ArgumentException("Geçersiz kullanıcı ID");

        var user = await repository.GetByIdAsync(request.Id);

        // ❗ 2. Null güvenli hata yönetimi
        if (user == null)
            throw new KeyNotFoundException($"User not found: {request.Id}");

        // ❗ 3. Mapping
        return new GetAppUserQueryResult
        {
            AppUserId = user.AppUserId,
            Name = user.Name,
            Surname = user.Surname,
            UserName = user.UserName,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        };
    }
}