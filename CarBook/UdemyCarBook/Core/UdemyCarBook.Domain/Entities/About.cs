using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Domain.Entities
{
    public class About
    {
        public int AboutID { get; set; }
        public string  Title { get; set; }

        public string  Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
