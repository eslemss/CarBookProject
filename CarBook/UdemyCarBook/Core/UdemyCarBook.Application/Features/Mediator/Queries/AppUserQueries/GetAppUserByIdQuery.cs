using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;

namespace UdemyCarBook.Application.Features.Mediator.Queries.AppUserQueries
{
    // Application/Features/AppUsers/Queries/GetAppUserByIdQuery.cs
    public class GetAppUserByIdQuery : IRequest<GetAppUserByIdQueryResult>
    {
        public int Id { get; set; }
        public GetAppUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
