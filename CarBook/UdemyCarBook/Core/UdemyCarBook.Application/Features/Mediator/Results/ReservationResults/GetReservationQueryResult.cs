namespace UdemyCarBook.Application.Features.Mediator.Results.ReservationResults
{
    public class GetReservationQueryResult
    {
        public int ReservationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CarID { get; set; }

        // --- BU ALANLARI EKLİYORUZ ---
        public string CarName { get; set; }
        public string PickUpLocationName { get; set; }
        public string DropOffLocationName { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        // ----------------------------

        public int PickUpLocationID { get; set; }
        public int DropOffLocationID { get; set; }
        public int Age { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}