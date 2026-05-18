using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AppUserCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAppUserCommandHandler(IRepository<AppUser> repository) : IRequestHandler<UpdateAppUserCommand>
    {
        public async Task Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var values = await repository.GetByIdAsync(request.AppUserId);
            if (values != null)
            {
                // Mevcut değerleri güncelle
                values.Name = request.Name;
                values.Surname = request.Surname;
                values.Email = request.Email;
                values.UserName = request.UserName;
                values.ImageUrl = request.ImageUrl;

                // Şifre boş gönderilmediyse güncelle (Önemli!)
                if (!string.IsNullOrEmpty(request.Password))
                {
                    values.Password = request.Password;
                }

                await repository.UpdateAsync(values);
            }
        }
    }
}