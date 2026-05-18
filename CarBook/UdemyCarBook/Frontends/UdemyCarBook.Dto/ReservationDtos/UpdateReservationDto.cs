using Newtonsoft.Json;

namespace UdemyCarBook.Dto.ReservationDtos;

public class UpdateReservationDto
{
    public int ReservationID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int CarID { get; set; }
    public int PickUpLocationID { get; set; }
    public int DropOffLocationID { get; set; }
    public int Age { get; set; }
    public int DriverLicenseYear { get; set; }
    public string DriverLicenseNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    [JsonProperty("pickUpDate")] // 'P' harfini küçük yapmayı dene
    public DateTime PickUpFull { get; set; }

    [JsonProperty("dropOffDate")] // 'D' harfini küçük yapmayı dene
    public DateTime DropOffFull { get; set; }

    public int AppUserId { get; set; }
    public DateTime PickUpDate { get; set; }
    public DateTime DropOffDate { get; set; }
}