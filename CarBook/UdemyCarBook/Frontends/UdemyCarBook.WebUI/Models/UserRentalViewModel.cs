namespace UdemyCarBook.WebUI.Models
{
    public class UserRentalViewModel
    {
        public int ReservationID { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        // HATA VEREN EKSİK ALANLAR BURADA OLMALI:
        public string PickUpLocationName { get; set; }
        public string DropOffLocationName { get; set; }
    }
}
