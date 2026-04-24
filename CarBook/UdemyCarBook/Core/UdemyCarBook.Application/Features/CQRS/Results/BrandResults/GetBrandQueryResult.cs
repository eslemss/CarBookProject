using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.CQRS.Results.BrandResult
{
    public class GetBrandQueryResult
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

    }
}
