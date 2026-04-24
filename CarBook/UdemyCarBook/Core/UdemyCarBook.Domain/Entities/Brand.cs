using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Domain.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
