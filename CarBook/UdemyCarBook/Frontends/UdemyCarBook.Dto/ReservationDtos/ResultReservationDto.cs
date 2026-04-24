using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.ReservationDtos
{
    public class ResultReservationDto
    {
        public int CarID { get; set; }
        public DateTime PickUpFull { get; set; }
        public DateTime DropOffFull { get; set; }
    }
}
