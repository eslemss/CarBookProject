using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.ViewModels
{
    public class CarPricingViewModel
    {
        public CarPricingViewModel()
        {
            Amounts = new List<decimal>();
        }
        public string Model { get; set; }
        public List<Decimal> Amounts { get; set; }
        public string BrandName { get; set; } 
        public string CoverImageUrl { get; set; }
        public string Brand { get; set; }
        public bool Available { get; set; }
        public int CarID { get; set; }
    }
}


