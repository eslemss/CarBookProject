using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Enums;
using UdemyCarBook.Application.Features.Mediator.Commands.AppUserCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand>
    {
        private readonly IRepository<AppUser> _repository;
        public CreateAppUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                Password = request.Password,

                // DEĞİŞİKLİK BURADA: Artık Username değil, UserName kullanıyoruz.
                UserName = request.Username,

                AppRoleId = (int)RolesType.Member,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,

                // Eğer AppUser entity sınıfında ImageUrl alanı zorunluysa buraya varsayılan ekleyebilirsin:
                ImageUrl = "/images/default-avatar.png"
            });
        }
    }
}