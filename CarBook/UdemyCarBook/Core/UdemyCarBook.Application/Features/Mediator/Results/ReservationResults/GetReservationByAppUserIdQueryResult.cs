namespace UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;

public class GetReservationByAppUserIdQueryResult
{
    public int ReservationID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int CarID { get; set; }
    public string CarName { get; set; } // Eksik olan
    public int PickUpLocationID { get; set; }
    public string PickUpLocationName { get; set; } // Eksik olan
    public int DropOffLocationID { get; set; }
    public string DropOffLocationName { get; set; } // Eksik olan
    public int Age { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int AppUserId { get; set; } // Eksik olan

    public DateTime PickUpFull { get; set; }
    public DateTime DropOffFull { get; set; }
}