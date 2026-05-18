using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;

public class UpdateReservationCommand : IRequest
{
    public int ReservationID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int CarID { get; set; }
    public int PickUpLocationID { get; set; }
    public int DropOffLocationID { get; set; }
    public int Age { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime PickUpFull { get; set; } // Bu alanı ekle
    public DateTime DropOffFull { get; set; } // Bu alanı ekle
    public int AppUserId { get; set; }
}