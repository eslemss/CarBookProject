    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace UdemyCarBook.Application.Features.Mediator.Results.CarPricingResults
    {
        public class GetCarPricingWithTimePeriodQueryResult
        {
            public int CarID { get; set; }
            public string Model { get; set; }
            public decimal DailyAmount { get; set; }
            public decimal WeeklyAmount { get; set; }
            public decimal MonthlyAmount { get; set; }
           public string CoverImageUrl { get; set; }
           public string Brand { get; set; }
        }
    }
