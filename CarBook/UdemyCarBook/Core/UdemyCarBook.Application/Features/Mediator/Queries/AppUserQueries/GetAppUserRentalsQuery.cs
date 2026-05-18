using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;
namespace UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries
{
    // Query dosyasında
    public class GetAppUserRentalsQuery : IRequest<List<GetAppUserRentalsQueryResult>>
    {
        public int Id { get; set; }
        public GetAppUserRentalsQuery(int id)
        {
            Id = id;
        }
    }
}
