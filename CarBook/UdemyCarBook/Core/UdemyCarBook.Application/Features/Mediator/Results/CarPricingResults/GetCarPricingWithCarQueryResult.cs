using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.Mediator.Results.CarPricingResults
{
    public class GetCarPricingWithCarQueryResult
    {
        public int CarId { get; set; }
        public int CarPricingId { get; set; } 
        public string Brand { get; set; }  
        public string Model { get; set; }   
        public decimal Amount { get; set; }   
        public string CoverImageUrl { get; set; }
        public bool Available { get; set; } // Bunu ekle

    }
}
