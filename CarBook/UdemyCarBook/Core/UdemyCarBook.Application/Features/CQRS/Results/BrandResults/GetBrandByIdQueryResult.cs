using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.CQRS.Results.BrandResults
{
    public class GetBrandByIdQueryResult
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

    }
}
