using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
