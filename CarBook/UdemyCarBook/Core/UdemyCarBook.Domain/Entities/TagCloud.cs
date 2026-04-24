using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Domain.Entities
{
    public class TagCloud
    {
        public int TagCloudId { get; set; } 
        public string Title { get; set; } 
        public int BlogID { get; set; } 
        public Blog Blog { get; set; } 
    }
}
