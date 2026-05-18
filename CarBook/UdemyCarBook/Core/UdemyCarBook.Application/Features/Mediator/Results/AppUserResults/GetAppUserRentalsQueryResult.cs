using System;

namespace UdemyCarBook.Application.Features.Mediator.Results.AppUserResults
{
    public class GetAppUserRentalsQueryResult
    {
        public int RentalId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}