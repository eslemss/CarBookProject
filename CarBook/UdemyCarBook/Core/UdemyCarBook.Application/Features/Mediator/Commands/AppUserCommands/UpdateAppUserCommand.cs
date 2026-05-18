using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.AppUserCommands
{
    public class UpdateAppUserCommand : IRequest
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; } // 'Username' değil 'UserName' olduğundan emin ol
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string? Password { get; set; } // Şifre alanı şart!
    }
}