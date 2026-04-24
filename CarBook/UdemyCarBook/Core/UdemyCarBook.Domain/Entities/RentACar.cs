using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Domain.Entities
{
    public class RentACar//araçların hangi lokasyonlarda tutulduğunu gösterir
    {
        public int RentACarId { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
        public bool Available { get; set; }
    }
}
