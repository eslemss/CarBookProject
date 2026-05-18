using Newtonsoft.Json;
using System;

namespace UdemyCarBook.Dto.ReservationDtos
{
    public class ResultReservationDto
    {
        public int ReservationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int CarID { get; set; }
        public string CarName { get; set; } // API'de "CarName" olduğundan emin olun

        public int PickUpLocationID { get; set; }
        public string PickUpLocationName { get; set; }

        public int DropOffLocationID { get; set; }
        public string DropOffLocationName { get; set; }

        public int Age { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        // API'den gelen "PickUpDate" verisini "PickUpFull" içine doldurur
        [JsonProperty("PickUpDate")]
        public DateTime PickUpFull { get; set; }

        // API'den gelen "DropOffDate" verisini "DropOffFull" içine doldurur
        [JsonProperty("DropOffDate")]
        public DateTime DropOffFull { get; set; }

        public int? AppUserId { get; set; }
    }
}