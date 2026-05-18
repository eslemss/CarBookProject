using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;

namespace UdemyCarBook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ReservationList()
    {
        var values = await _mediator.Send(new GetReservationQuery());
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        var value = await _mediator.Send(new GetReservationByIdQuery(id));
        return Ok(value);
    }

    [HttpGet("CheckAvailability")]
    public async Task<IActionResult> CheckAvailability([FromQuery] int carId, [FromQuery] DateTime pickup, [FromQuery] DateTime dropoff)
    {
        try
        {
            await _mediator.Send(new CreateReservationCommand
            {
                CarID = carId,
                PickUpDate = pickup,
                DropOffDate = dropoff
                // Status = "Kontrol" satırını buradan sildik
            });

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Rezervasyon Başarıyla Oluşturuldu");
        }
        catch (Exception ex)
        {
            // Eğer kayıt sırasında bir çakışma olursa (Handler exception fırlatırsa)
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ReservationStatusChangeApproved/{id}")]
    public async Task<IActionResult> ReservationStatusChangeApproved(int id)
    {
        await _mediator.Send(new UpdateReservationApproveCommand(id));
        return Ok("Rezervasyon Onaylandı");
    }

    [HttpGet("ReservationStatusChangeCancelled/{id}")]
    public async Task<IActionResult> ReservationStatusChangeCancelled(int id)
    {
        await _mediator.Send(new UpdateReservationCancelCommand(id));
        return Ok("Rezervasyon İptal Edildi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReservation(UpdateReservationCommand command)
    {
        await _mediator.Send(command);
        return Ok("Rezervasyon Güncellendi");
    }
}