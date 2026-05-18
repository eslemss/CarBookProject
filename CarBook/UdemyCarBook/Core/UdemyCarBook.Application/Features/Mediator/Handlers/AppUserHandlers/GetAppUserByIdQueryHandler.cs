// GetAppUserByIdQueryHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;
using UdemyCarBook.Application.Interfaces; // IRepository için
using UdemyCarBook.Domain.Entities; // AppUser entity'si için

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, GetAppUserByIdQueryResult>
    {
        private readonly IRepository<AppUser> _repository;

        public GetAppUserByIdQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<GetAppUserByIdQueryResult> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetAppUserByIdQueryResult
            {
                AppUserId = values.AppUserId,
                Name = values.Name,
                Surname = values.Surname,
                UserName = values.UserName,
                Email = values.Email,
                ImageUrl = values.ImageUrl
            };
        }
    }
}