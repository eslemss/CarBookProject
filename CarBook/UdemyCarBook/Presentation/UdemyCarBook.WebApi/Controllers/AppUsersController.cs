using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AppUserCommands; // Bu using'i ekle
using UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            var value = await _mediator.Send(new GetAppUserQuery(id));
            return Ok(value);
        }

        [HttpGet("GetUserRentals/{id}")]
        public async Task<IActionResult> GetUserRentals(int id)
        {
            var values = await _mediator.Send(new GetReservationByAppUserIdQuery(id));
            return Ok(values);
        }

        // --- EKSİK OLAN GÜNCELLEME METODU BURASI ---
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı Bilgileri Başarıyla Güncellendi");
        }
    }
}